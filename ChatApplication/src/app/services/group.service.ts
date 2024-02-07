import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  constructor(private http: HttpClient, private router: Router) {}
  private baseUrl: string = 'https://localhost:7033/api/group/';

  joinGroup(groupObj: any) {
    return this.http.post(`${this.baseUrl}JoinGroup`, groupObj);
  }

  getAllGroups() {
    return this.http.get(`${this.baseUrl}GetAllGroups`);
  }

  getAllGroupUsers(groupId: Guid) {
    return this.http.get(`${this.baseUrl}GetGroupUsers/` + groupId);
  }

  getAllGroupMessages(groupId: Guid) {
    return this.http.get(
      `${this.baseUrl}GetAllGroupMessage?groupId=` + groupId
    );
  }

  findGroupUsers(groupId: Guid, userId: string) {
    return this.http.get(
      `${this.baseUrl}FindGroupUsers?groupId=` + groupId + '&userId=' + userId
    );
  }

  leaveGroup(groupId: Guid, userId: string) {
    return this.http.delete(
      `${this.baseUrl}LeaveGroup/` + groupId + '?userId=' + userId
    );
  }
}
