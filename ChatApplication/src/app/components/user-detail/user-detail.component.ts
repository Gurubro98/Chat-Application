import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Group } from 'src/app/interfaces/Group';
import { Register } from 'src/app/interfaces/register';
import { ChatService } from 'src/app/services/chat.service';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css'],
})
export class UserDetailComponent implements OnInit {
  @Input() currentGroup!: Group | null;
  @Input() users!: any;
  @Input() loggedInUserId!: string;
  @Output() event = new EventEmitter<boolean>();

  constructor(private groupService: GroupService, private chatService : ChatService, private messageService : MessageService) {}

  ngOnInit(): void {
  }

  getGroupUsers() {
    if (this.currentGroup && this.currentGroup.groupId) {
      this.groupService.getAllGroupUsers(this.currentGroup.groupId).subscribe({
        next: (res: any) => {
          this.users = res.groupUsers;
          console.log(res);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  onBack(){
    this.event.emit(false);
  }

  onRemoveGroup(userId: string) {
    if(this.currentGroup && this.currentGroup.groupId){
    this.chatService.RemoveUserFromGroup(this.currentGroup.groupId, userId);
    this.messageService.add({
      severity: 'success',
      summary: 'Remove From Group',
      detail: 'User Remove Group successfully',
    });
    setTimeout(() => this.getGroupUsers(), 100);
  }
  }

}
