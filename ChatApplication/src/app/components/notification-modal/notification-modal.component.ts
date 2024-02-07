import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { RequestAction } from 'src/app/helpers/RequestAction.enum';
import { Request } from 'src/app/interfaces/request';
import { ChatService } from 'src/app/services/chat.service';

@Component({
  selector: 'app-notification-modal',
  templateUrl: './notification-modal.component.html',
  styleUrls: ['./notification-modal.component.css'],
})
export class NotificationModalComponent implements OnInit {
  requests: Request[] = [];
  loggedInUserId!: string;
  action!: RequestAction;
  constructor(
    private modalRef: BsModalRef,
    // private messageService: MessageService,
    // private groupService: GroupService,
    private chatService: ChatService
  ) {}

  ngOnInit(): void {
    console.log(this.requests);
  }

  onClose() {
    this.modalRef.hide();
  }

  getActionValue(value: number) {
    return RequestAction[value];
  }

  getActionColor(val: number): string {
    if (val == 0) {
      return 'btn-warning';
    } else if (val == 1) {
      return 'btn-success';
    } else {
      return 'btn-danger';
    }
  }

  onApproved(request: Request) {
    request.isTakeAction = true;
    request.status = RequestAction.Approved;
    this.chatService.TakeActionOnRequest(request);
  }

  onReject(request: Request) {
    debugger;
    request.isTakeAction = true;
    request.status = RequestAction.Rejected;
    this.chatService.TakeActionOnRequest(request);
  }
}
