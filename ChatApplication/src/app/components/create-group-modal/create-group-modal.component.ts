import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import ValidateForm from 'src/app/helpers/validate-form';
import { AuthService } from 'src/app/services/auth.service';
import { ChatService } from 'src/app/services/chat.service';
import { GroupService } from 'src/app/services/group.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-create-group-modal',
  templateUrl: './create-group-modal.component.html',
  styleUrls: ['./create-group-modal.component.css'],
})
export class CreateGroupModalComponent implements OnInit {
  groupForm!: FormGroup;
  hubConnection!: HubConnection;
  token: any;
  loggedInUserId!: string;
  @Output() event = new EventEmitter<string>();
  constructor(
    private modalRef: BsModalRef,
    private fb: FormBuilder,
    private auth: AuthService,
    private userStore: UserStoreService,
    private groupService: GroupService,
    private messageService: MessageService,
    private chatService: ChatService
  ) {}
  ngOnInit(): void {
    this.token = localStorage.getItem('token');
    // get current userId
    this.userStore.getIdFromToken().subscribe((val) => {
      let userIdFromToken = this.auth.getIdFromToken();
      this.loggedInUserId = val || userIdFromToken;
    });
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`https://localhost:7033/chat?access_token=${this.token}`)
      .configureLogging(signalR.LogLevel.Information)
      .build();
    this.groupForm = this.fb.group({
      groupName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(15),
        ],
      ],
      userId: [this.loggedInUserId],
      // userId: [this.userId],
    });
  }

  get f() {
    return this.groupForm.controls;
  }

  onClose() {
    this.modalRef.hide();
  }

  createGroup() {
    debugger;
    if (this.groupForm.valid) {
      console.log(this.groupForm.value);
      this.chatService
        .CreateGroup(this.groupForm.value)
        .then(() => {
          this.groupForm.reset();
          this.modalRef.hide();
          this.event.emit('ok');
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Group Created successfully',
          });
        })
        .catch((err) => {
          console.log(err);
        });
    } else {
      ValidateForm.validateAllFormFields(this.groupForm);
    }
  }
}
