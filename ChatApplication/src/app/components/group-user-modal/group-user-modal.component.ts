import { isNgTemplate } from '@angular/compiler';
import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { Guid } from 'guid-typescript';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { RequestAction } from 'src/app/helpers/RequestAction.enum';
import { Group } from 'src/app/interfaces/Group';
import { Register } from 'src/app/interfaces/register';
import { Request } from 'src/app/interfaces/request';
import { UserGroup } from 'src/app/interfaces/user-group';
import { AuthService } from 'src/app/services/auth.service';
import { ChatService } from 'src/app/services/chat.service';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-group-user-modal',
  templateUrl: './group-user-modal.component.html',
  styleUrls: ['./group-user-modal.component.css'],
})
export class GroupUserModalComponent implements OnInit {
  loggedInUserId!: string;
  users: Register[] = [];
  @Output() event = new EventEmitter<string>();
  constructor(
    private modalRef: BsModalRef,
    private messageService: MessageService,
    private chatService: ChatService,
    private auth: AuthService
  ) {}

  ngOnInit(): void {
      this.auth.getAllUsers().subscribe({
        next: (res: any) => {
          this.users = res.users.filter((x:any) => x.id !== this.loggedInUserId);
         console.log("users", this.users);
        },
      });
    }
  



  onClose() {
    this.modalRef.hide();
  }

 

  sendRequest(user: any) {
    user.isSendRequest = true;
    let request: any = {
      status: RequestAction.Pending,
      sender: null,
      receiver: null,
      senderId: this.loggedInUserId,
      receiverId: user.id,
      isTakeAction: false,
    };
    this.chatService.sendRequest(request);
  }


}
