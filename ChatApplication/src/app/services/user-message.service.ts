import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root',
})
export class UserMessageService {
  constructor(private http: HttpClient, private router: Router) {}
  private baseUrl: string = 'https://localhost:7033/api/message/';

  getAllUserMessages(senderId: string, receiverId: string, page: number) {
    return this.http.get(
      `${this.baseUrl}GetAllMessages?senderId=` +
        senderId +
        '&receiverId=' +
        receiverId +
        '&page=' +
        page
    );
  }
  getAllUnReadMessages() {
    return this.http.get(`${this.baseUrl}GetAllUnReadMessages`);
  }
  getAllGroupMessages(groupId: Guid, page: number) {
    return this.http.get(
      `${this.baseUrl}GetAllGroupMessages?groupId=` + groupId + '&page=' + page
    );
  }

  addAttachment(file: FormData) {
    return this.http.post(`${this.baseUrl}AddAttachment`, file);
  }

  UpdateMessage(message: any) {
    return this.http.put(`${this.baseUrl}MessageSeen`, message);
  }
}
