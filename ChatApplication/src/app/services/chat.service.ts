import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Request } from '../interfaces/request';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  token = localStorage.getItem('token');
  private userPayload: any;
  hubConnection: HubConnection = new HubConnectionBuilder()
    .withUrl(`https://localhost:7033/chat?access_token=${this.token}`)
    .configureLogging(signalR.LogLevel.Information)
    .build();

  public messages$ = new BehaviorSubject<any>({});
  public newGroup$ = new BehaviorSubject<any>({});
  public removeGroupUser$ = new BehaviorSubject<any>({});
  public notification$ = new BehaviorSubject<any>({});
  public request$ = new BehaviorSubject<any>({});
  public newUser$ = new BehaviorSubject<any>({});
  public removeUser$ = new BehaviorSubject<any>({});
  public connectedUsers$ = new BehaviorSubject<string[]>([]);

  // public connectedGroups$ = new BehaviorSubject<string[]>([]);

  constructor(private auth: AuthService, private router: Router) {
    this.userPayload = this.auth.decodeToken();
    console.log('user Payload');
  }

  //start connection
  public async start() {
    try {
      if (
        this.hubConnection.state === signalR.HubConnectionState.Disconnected
      ) {
        return this.hubConnection.start();
      } else {
        return Promise.resolve(); // Already connected or connecting
      }
    } catch (error) {
      console.log(error);
    }
  }

  signalRMethods() {
    this.hubConnection.on('BroadcastUserOnConnect', (Usrs) => {
      this.connectedUsers$.next(Usrs);
    });

    this.hubConnection.on('BroadcastUserOnDisconnect', (Usrs) => {
      this.connectedUsers$.next(Usrs);
    });

    this.hubConnection.on(
      'ReceiveNotification',
      (senderName, notification, receiverId) => {
        debugger;
        this.notification$.next({ senderName, notification, receiverId });
      }
    );

    this.hubConnection.on('ReceiveDM', (connectionId, message) => {
      console.log('received message', message);
      message.type = 'recieved';
      this.messages$.next(message);
    });

    this.hubConnection.on('NewGroup', (group) => {
      this.newGroup$.next(group);
    });

    this.hubConnection.on('ReceiveMessage', (message) => {
      message.type = 'recieved';
      this.messages$.next(message);
    });

    this.hubConnection.on('RemoveFromGroup', (groupId, userId) => {
      this.removeGroupUser$.next({ groupId, userId });
    });

    this.hubConnection.on('ReceiveRequest', (request) => {
      this.request$.next(request);
    });

    this.hubConnection.on('FriendUser', (User) => {
      debugger;
      this.newUser$.next(User);
    });
    this.hubConnection.on('RemoveUser', (User) => {
      this.removeUser$.next(User);
    });
  }

  public async SendDirectMessage(msg: any) {
    debugger;
    // const formDataObject: { [key: string]: string | Blob } = {};
    // msg.forEach((value, key) => {
    //   formDataObject[key] = value;
    // });

    // console.log(formDataObject);
    await this.hubConnection
      .invoke('SendMessageToUser', msg)
      .then(() => console.log('Message to user Sent Successfully'))
      .catch((err) => console.info(err.message));
  }

  public async CreateGroup(group: any) {
    await this.hubConnection
      .invoke('CreateGroup', group)
      .then(() => console.log('Group Created Successfully'))
      .catch((err) => console.error(err));
  }

  public async RemoveUserFromGroup(groupId: Guid, userId: string) {
    await this.hubConnection.invoke('UserRemoveFromGroup', groupId, userId);
  }

  public async RemoveUserFromList(
    loggedInUserId: string,
    removerUserId: string
  ) {
    await this.hubConnection.invoke(
      'RemoveUserFromList',
      loggedInUserId,
      removerUserId
    );
  }

  public async joinGroup(group: any) {
    await this.hubConnection.invoke('JoinGroup', group);
  }

  public async sendRequest(request: any) {
    await this.hubConnection
      .invoke('SendRequest', request)
      .then(() => console.log('No error'))
      .catch((err) => console.log(err));
  }

  public async TakeActionOnRequest(request: any) {
    await this.hubConnection.invoke('TakeActionOnRequest', request);
  }

  // public async SendMessageToUsers(msg: any) {
  //   await this.hubConnection
  //     .invoke('SendMessageToUsers', msg)
  //     .then(() => console.log('Message to user Sent Successfully'))
  //     .catch((err) => console.error(err));
  // }

  DisconnectUser() {
    this.hubConnection
      .invoke('RemoveOnlineUser', this.userPayload.Id)
      .then(() => {
        console.log('user disconnected');
      })
      .catch((err) => console.error(err));
    localStorage.removeItem('token');
    this.router.navigate(['login']);
  }
}
