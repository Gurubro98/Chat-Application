import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  ElementRef,
  HostListener,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { MessageService } from 'primeng/api';
import { Messages } from 'src/app/interfaces/messages';
import { Register } from 'src/app/interfaces/register';
import { AuthService } from 'src/app/services/auth.service';
import { UserMessageService } from 'src/app/services/user-message.service';
import { UserStoreService } from 'src/app/services/user-store.service';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CreateGroupModalComponent } from '../create-group-modal/create-group-modal.component';
import { GroupService } from 'src/app/services/group.service';
import { Group } from 'src/app/interfaces/Group';
import { Subscription, forkJoin } from 'rxjs';
import { ChatService } from 'src/app/services/chat.service';
import { GroupUserModalComponent } from '../group-user-modal/group-user-modal.component';
import { Guid } from 'guid-typescript';
import { NotificationModalComponent } from '../notification-modal/notification-modal.component';
import { group } from '@angular/animations';
import { RequestService } from 'src/app/services/request.service';
import { Request } from 'src/app/interfaces/request';
import { Mutual } from 'src/app/interfaces/Mutual';
import { Form } from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-online-users',
  templateUrl: './online-users.component.html',
  styleUrls: ['./online-users.component.css'],
})
export class OnlineUsersComponent implements OnInit, AfterViewInit {
  allMessages: FormData = new FormData();
  loggedInUserId!: string;
  loggedInUserName!: string;
  loggedInUser!: Register;
  users: (Mutual | Group)[] = [];
  groups: Group[] = [];
  groupUsers: any[] = [];
  imgData: any;
  selectedFile: any;
  showGroupUser: boolean = false;
  chatUser!: Group | Mutual;
  notification: string[] = [];
  msgDate!: Date;
  messages: Messages[] = [];
  displayMessages: any[] = [];
  requests: Request[] = [];
  message: string = '';
  currentGroup!: Group | null;
  previewImage!: any;
  hubConnection!: HubConnection;
  token: any;
  showEmoji: boolean = true;
  isChatOpen: boolean = false;
  receiveDMSubscribed: boolean = false;
  connectedUsers: any[] = [];
  unReadMessages: any[] = [];
  unReadMessageCount!: number;
  isSmallScreen: boolean = false;
  isChatHeadVisible: boolean = true;
  modalRef!: BsModalRef;
  isAVailableForChat: boolean = false;
  IsGroupMessage: boolean = false;
  connectedUserSubscription!: Subscription;
  chatSubscription!: Subscription;
  groupSubscription!: Subscription;
  RemoveGroupUserSubscription!: Subscription;
  RemoveUserSubscription!: Subscription;
  notificationSubscription!: Subscription;
  requestSubscription!: Subscription;
  newUserSubscription!: Subscription;
  requestCount: number = 0;
  showProfile: boolean = true;
  messageLoadCount: number = 1;
  page = 1;
  items: any[] = [];
  profile: any[] = [];
  attachment: any[] = [];
  spinner: boolean = false;
  showAttachMent: boolean = false;
  hasMoreData = true;
  showMenu: boolean = false;
  @ViewChild('scrollContainer') scrollContainer!: ElementRef;
  @ViewChild('scrollerMe') scrollerMeRef: ElementRef = new ElementRef<any>(0);
  @ViewChild('choose') chooseInput!: ElementRef;
  constructor(
    private router: Router,
    private auth: AuthService,
    private userStore: UserStoreService,
    private messageService: MessageService,
    private userMessageService: UserMessageService,
    private modalService: BsModalService,
    private groupService: GroupService,
    private chatService: ChatService,
    private cdr: ChangeDetectorRef,
    private requestService: RequestService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    try {
      this.chatService.start();
      this.chatService.signalRMethods();

      // get current userId
      this.userStore.getIdFromToken().subscribe((val) => {
        let userIdFromToken = this.auth.getIdFromToken();
        this.loggedInUserId = val || userIdFromToken;
      });
      // get current user Name
      this.userStore.getNameFromToken().subscribe((val) => {
        let userNameFromToken = this.auth.getNameFromToken();
        this.loggedInUserName = val || userNameFromToken;
      });

      this.auth.getLoggedInUser(this.loggedInUserId).subscribe((res: any) => {
        this.loggedInUser = res.currentUser;

        this.loggedInUser.imageUrl = this.loggedInUser.imageUrl.replace(
          'C:\\Gagan\\Programming\\assignment\\ChatApplicationAPI\\ChatApplicationAPI\\ChatApplicationAPI\\wwwroot\\',
          'https://localhost:7033/'
        );
      });
      //show right menu
      this.items = [
        {
          label: 'Leave Group',
          icon: 'bx bx-trash',
          command: () => {
            if (this.currentGroup && this.currentGroup.groupId) {
              this.onLeaveGroup(this.currentGroup.groupId);
            }
          },
        },
      ];
      this.attachment = [
        {
          label: 'Images',
          icon: 'bx bx-image',
          command: () => {
            this.openFileDialog();
          },
        },
      ];
      // show profile
      this.profile = [
        {
          label: `${this.loggedInUserName}`,
          icon: 'bx bx-user',
        },
        {
          label: 'Create Group',
          icon: 'bx bx-group',
          command: () => {
            this.createGroupModal();
          },
        },
        {
          label: 'Logout',
          icon: 'bx bx-log-out',
          command: () => {
            this.onLogout();
          },
        },
      ];
      // setTimeout(() => , 500);
      // this.token = localStorage.getItem('token');

      const combinedObservable = forkJoin([
        this.userMessageService.getAllUnReadMessages(),
        this.auth.getAllMutualUsers(this.loggedInUserId),
        this.groupService.getAllGroups(),
      ]);

      // Subscribe to the combined observable
      combinedObservable.subscribe({
        next: (results: any) => {
          const unReadMessages = results[0];
          const usersResponse = results[1];
          const groupResponse = results[2];

          // Process unReadMessages
          this.unReadMessages = unReadMessages.unReadMessages;
          console.log('Unread Messages', this.unReadMessages);

          // Process usersResponse
          if (usersResponse.users) {
            this.users = usersResponse.users;
            // this.users = usersResponse.users.filter(
            //   (x: any) => (x.user.id !== this.loggedInUserId) && (x.mutualId.id !==this.loggedInUserId)
            // );
            this.users.forEach((item: any) => {
              item['count'] = this.unReadMessages.filter(
                (message) =>
                  (message.senderId == item.user.id ||
                    message.senderId == item.mutual.id) &&
                  !message.isRead &&
                  (message.receiverId == this.loggedInUserId ||
                    message.receiverId == this.loggedInUserId)
              ).length;
              item['isActive'] = false;
            });
            console.log('users', this.users);
            this.makeItOnline();
          }

          if (groupResponse.groups != null) {
            this.groups = groupResponse.groups;
            console.log(groupResponse);
            this.groups.forEach((item) => {
              (this.users as Group[]).push(item);
            });
          }
        },
        error: (err) => {
          console.log(err);
        },
      });

      // get  Notification data
      this.requestService
        .getNumberOfRequests(this.loggedInUserId)
        .subscribe((res: any) => {
          this.requestCount = res.count;
        });

      // get all connected Users
      this.connectedUserSubscription =
        this.chatService.connectedUsers$.subscribe((users) => {
          this.connectedUsers = users;
          (this.users as Mutual[]).forEach((item) => {
            item.isOnline = false;
          });
          console.log('connected users', this.connectedUsers);
          this.makeItOnline();
        });

      // get all Notification
      this.notificationSubscription = this.chatService.notification$.subscribe(
        (res: any) => {
          if (res.senderName && res.receiverId == this.loggedInUserId) {
            this.messageService.add({
              severity: 'success',
              summary: res.senderName,
              detail: res.notification,
            });
            this.notification.push(res);
            console.log(res);
          } else if (res.notification) {
            this.messageService.add({
              severity: 'success',
              summary: 'Notification',
              detail: res.notification,
            });
          }
        }
      );

      // get received Message
      this.chatSubscription = this.chatService.messages$.subscribe(
        (message) => {
          (this.users as Mutual[]).forEach((item) => {
            if (
              (item.user?.id == message.senderId ||
                item.mutual?.id == message.senderId) &&
              message.receiverId == this.loggedInUserId &&
              !item['isActive']
            ) {
              item['count'] = item.count + 1;
            }
            if (item['isActive']) {
              message.isRead = true;
              this.userMessageService
                .UpdateMessage(message)
                .subscribe((val) => {
                  console.log('Message read SuccessFully');
                });
            }
          });
          if (
            this.currentGroup &&
            message.groupId == this.currentGroup.groupId
          ) {
            this.messages.unshift(message);
            this.displayMessages = this.messages;
            this.scrollToBottom();
          } else if (
            message.receiverId == this.loggedInUserId &&
            !(this.chatUser as Group).groupId
          ) {
            this.messages.unshift(message);
            this.displayMessages = this.messages;
            this.scrollToBottom();
          }
        }
      );

      // new group
      this.groupSubscription = this.chatService.newGroup$.subscribe((group) => {
        this.users.push(group);
      });

      // remove user{
      this.RemoveGroupUserSubscription =
        this.chatService.removeGroupUser$.subscribe((val) => {
          if (
            this.currentGroup &&
            val.groupId == this.currentGroup.groupId &&
            val.userId == this.loggedInUserId
          ) {
            this.isAVailableForChat = false;
          }
        });

      // requests
      this.requestSubscription = this.chatService.request$.subscribe(
        (request: Request) => {
          console.log(request);
          let previousRequest: Request | undefined = this.requests.find(
            (x) =>
              x.senderId == request.senderId &&
              x.receiverId == request.receiverId
          );
          if (!previousRequest) {
            this.requestCount = this.requestCount + 1;
            this.requests.push(request);
          }
        }
      );
      if (!this.newUserSubscription) {
        this.newUserSubscription = this.chatService.newUser$.subscribe(
          (user) => {
            this.users.push(user);
            this.makeItOnline();
          }
        );
      }

      this.RemoveUserSubscription = this.chatService.removeUser$.subscribe(
        (user) => {
          let removerUser;
          (this.users as Mutual[]).forEach((item) => {
            if (item.mutualId == user.mutualId && item.userId == user.userId) {
              removerUser = item;
            }
          });
          if (removerUser) {
            let removeUserIndex = this.users.indexOf(removerUser);
            if (removeUserIndex) {
              this.users.splice(removeUserIndex, 1);
            }
          }

          this.isChatOpen = false;
        }
      );
    } catch (err) {
      console.error(err);
    }
  }

  ngAfterViewInit() {
    this.scrollToBottom();
  }

  ngOnDestroy() {
    // Unsubscribe to avoid memory leaks
    if (this.chatSubscription) {
      this.chatSubscription.unsubscribe();
    }
    this.connectedUserSubscription.unsubscribe();
    this.groupSubscription.unsubscribe();
    this.RemoveGroupUserSubscription.unsubscribe();
    this.notificationSubscription.unsubscribe();
    this.requestSubscription.unsubscribe();
    if (this.newUserSubscription) {
      this.newUserSubscription.unsubscribe();
    }

    this.RemoveUserSubscription.unsubscribe();
  }

  onShowEmoji() {
    this.showEmoji = !this.showEmoji;
  }

  addEmoji(event: any) {
    if (this.message == '') {
      this.message = event.emoji.native;
    } else {
      this.message = this.message + event.emoji.native;
    }
  }

  onShowAttachMent() {
    this.showAttachMent = !this.showAttachMent;
    this.previewImage = null;
  }

  onShowMenu() {
    this.showMenu = !this.showMenu;
  }

  onShowProfile() {
    this.showProfile = !this.showProfile;
  }

  isMutual(chatUser: Group | Mutual): chatUser is Mutual {
    return (chatUser as Mutual).mutualId !== null;
  }

  isGroup(chatUser: Group | Mutual): chatUser is Group {
    return (chatUser as Group).groupId !== null;
  }

  getChatUserImage(item: Mutual) {
    if (item.userId == this.loggedInUserId) {
      const replacedImageUrl = item.mutual?.imageUrl?.replace(
        'C:\\Gagan\\Programming\\assignment\\ChatApplicationAPI\\ChatApplicationAPI\\ChatApplicationAPI\\wwwroot\\',
        'https://localhost:7033/'
      );
      if (replacedImageUrl) {
        return replacedImageUrl;
      }
      return null;
    } else {
      const replacedImageUrl = item.user?.imageUrl?.replace(
        'C:\\Gagan\\Programming\\assignment\\ChatApplicationAPI\\ChatApplicationAPI\\ChatApplicationAPI\\wwwroot\\',
        'https://localhost:7033/'
      );
      if (replacedImageUrl) {
        return replacedImageUrl;
      }
      return null;
    }
  }

  getGroupUsers() {
    if (this.currentGroup && this.currentGroup.groupId) {
      this.groupService.getAllGroupUsers(this.currentGroup.groupId).subscribe({
        next: (res: any) => {
          this.groupUsers = res.groupUsers;
          console.log(res);
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  openChat(user: any) {
    this.page = 1;
    this.hasMoreData = true;
    this.messageLoadCount = 1;
    this.isChatHeadVisible = false;
    this.isChatOpen = true;
    this.displayMessages = [];
    // for groupChat
    if (user.groupName) {
      this.chatService
        .joinGroup(user)
        .then(() => 'Joined group successfuly')
        .catch((err) => console.log(err));
      this.IsGroupMessage = true;
      this.currentGroup = user;
      this.getGroupUsers();
      (this.users as Mutual[]).forEach((item) => {
        item['isActive'] = false;
      });
      user['isActive'] = true;
      this.chatUser = user;

      // user can access the group or not
      this.groupService
        .findGroupUsers(user.groupId, this.loggedInUserId)
        .subscribe((val: any) => {
          if (val.isUserAddedInGroup) {
            this.isAVailableForChat = true;
          } else {
            this.isAVailableForChat = false;
          }
          this.spinner = true;
          if (this.currentGroup && this.currentGroup.groupId) {
            this.userMessageService
              // get all user messages for specific chat
              .getAllGroupMessages(this.currentGroup.groupId, this.page)
              .subscribe({
                next: (res: any) => {
                  this.messages = res.messages;
                  this.messages.forEach((x) => {
                    x.type =
                      x.senderId === this.loggedInUserId ? 'sent' : 'recieved';
                  });
                  this.displayMessages = this.messages;

                  this.scrollToBottom();
                  this.cdr.detectChanges();
                  console.log(this.displayMessages);
                },
                error: (err) => {
                  console.log(err);
                },
                complete: () => {
                  this.spinner = false;
                },
              });
          }
        });
    } else {
      this.IsGroupMessage = false;
      this.currentGroup = null;
      // for single user Chat
      this.isAVailableForChat = true;
      (this.users as Mutual[]).forEach((item) => {
        item['isActive'] = false;
      });
      user.count = 0;
      user['isActive'] = true;
      this.chatUser = user;
      let receiverId =
        this.loggedInUserId == (this.chatUser as Mutual).userId
          ? (this.chatUser as Mutual).mutualId
          : (this.chatUser as Mutual).userId;
      if (receiverId) {
        this.userMessageService
          // get all user messages for specific chat
          .getAllUserMessages(this.loggedInUserId, receiverId, this.page)
          .subscribe({
            next: (res: any) => {
              this.messages = res.messages;
              this.messages.forEach((x) => {
                x.type =
                  x.receiverId === this.loggedInUserId ? 'recieved' : 'sent';
              });
              this.displayMessages = this.messages;
              this.scrollToBottom();
              this.cdr.detectChanges();
              console.log(this.displayMessages);
            },
            error: (err) => {
              console.log(err);
            },
          });
      }
    }
  }

  makeItOnline() {
    if (this.connectedUsers && this.users) {
      this.connectedUsers.forEach((item) => {
        let user = (this.users as Mutual[]).find((x) =>
          x.userId !== this.loggedInUserId
            ? x.userId == item.userId
            : x.mutualId == item.userId
        );
        if (user) {
          user.isOnline = true;
        }
      });
    }
  }

  scrollToBottom() {
    if (this.scrollContainer && this.scrollContainer.nativeElement) {
      setTimeout(() => {
        this.scrollContainer.nativeElement.scrollTop =
          this.scrollContainer.nativeElement.scrollHeight;
      });
    }
  }

  SendDirectMessage() {
    this.previewImage = null;
    this.msgDate = new Date();
    this.msgDate = this.convertUtcToLocalDate(this.msgDate);
    if ((this.message != '' && this.message.trim() != '') || this.imgData) {
      // let guid = Guid.create();
      if (
        this.currentGroup &&
        this.currentGroup.groupId &&
        this.IsGroupMessage
      ) {
        if (this.imgData) {
          this.allMessages.append(
            'AttachmentFile',
            this.imgData,
            this.imgData.name
          );
          this.allMessages.append('CreatedTime', this.msgDate.toISOString());
        }
        if (this.message == '') {
          this.userMessageService
            .addAttachment(this.allMessages)
            .subscribe((val: any) => {
              console.log(val);
              this.selectedFile = val.attachment;
              var msg: any = {
                id: null,
                senderId: this.loggedInUserId,
                groupId: this.currentGroup?.groupId,
                timeStamp: this.msgDate,
                type: 'sent',
                attachmentId: this.selectedFile.attachmentId,
                attachment: this.selectedFile,
              };
              this.selectedFile = null;
              this.messages.unshift(msg);
              this.displayMessages = this.messages;
              this.scrollToBottom();

              this.chatService
                .SendDirectMessage(msg)
                .then(() => {
                  this.message = '';
                })
                .catch((err) => {
                  console.log(err);
                });
            });
        } else {
          var msg: any = {
            id: null,
            senderId: this.loggedInUserId,
            groupId: this.currentGroup.groupId,
            timeStamp: this.msgDate,
            type: 'sent',
            content: this.message,
          };
          this.messages.unshift(msg);
          this.displayMessages = this.messages;
          this.scrollToBottom();

          this.chatService
            .SendDirectMessage(msg)
            .then(() => {
              this.message = '';
            })
            .catch((err) => {
              console.log(err);
            });
        }
      } else {
        debugger;
        let receiverId =
          this.loggedInUserId == (this.chatUser as Mutual).userId
            ? (this.chatUser as Mutual).mutualId
            : (this.chatUser as Mutual).userId;

        if (this.imgData) {
          this.allMessages.append(
            'AttachmentFile',
            this.imgData,
            this.imgData.name
          );
          this.allMessages.append('CreatedTime', this.msgDate.toISOString());
        }
        if (this.message == '') {
          this.userMessageService
            .addAttachment(this.allMessages)
            .subscribe((val: any) => {
              console.log(val);
              this.selectedFile = val.attachment;
              var msg: any = {
                id: null,
                senderId: this.loggedInUserId,
                receiverId: receiverId,
                timeStamp: this.msgDate,
                type: 'sent',
                attachmentId: this.selectedFile.attachmentId,
                attachment: this.selectedFile,
              };
              this.selectedFile = null;
              debugger;
              this.messages.unshift(msg);
              this.displayMessages = this.messages;
              this.scrollToBottom();

              this.chatService
                .SendDirectMessage(msg)
                .then(() => {
                  this.message = '';
                })
                .catch((err) => {
                  console.log(err);
                });
            });
        } else {
          var msg: any = {
            id: null,
            senderId: this.loggedInUserId,
            receiverId: receiverId,
            timeStamp: this.msgDate,
            type: 'sent',
            content: this.message,
          };
          this.messages.unshift(msg);
          this.displayMessages = this.messages;
          this.scrollToBottom();

          this.chatService
            .SendDirectMessage(msg)
            .then(() => {
              this.message = '';
            })
            .catch((err) => {
              console.log(err);
            });
        }
      }
    }
  }

  onLogout() {
    this.chatService.DisconnectUser();
  }

  // convert date to localDate
  convertUtcToLocalDate(val: Date): Date {
    var date = new Date(val);
    var localOffset = date.getTimezoneOffset() * 60000;
    var localTime = date.getTime() - localOffset;

    date.setTime(localTime);
    return date;
  }

  // convert utc to localTime

  convertUtcToLocalTime(utcTime: string): string {
    let date = new Date();
    let localTimeOffset = date.getTimezoneOffset();
    const utcMoment = moment.utc(utcTime);
    const localMoment = utcMoment.subtract(localTimeOffset);

    const formattedLocalTime = localMoment.format('hh:mm a');
    return formattedLocalTime;
  }

  getSendImage(image: string) {
    const replacedImageUrl = image.replace(
      'C:\\Gagan\\Programming\\assignment\\ChatApplicationAPI\\ChatApplicationAPI\\ChatApplicationAPI\\wwwroot\\',
      'https://localhost:7033/'
    );
    // let imagePreviewUrl =
    //   this.sanitizer.bypassSecurityTrustResourceUrl(replacedImageUrl);
    return replacedImageUrl;
  }

  onShowGroupUser() {
    this.showGroupUser = true;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event): void {
    this.isSmallScreen = window.innerWidth < 767;
  }

  onBack() {
    this.isChatHeadVisible = !this.isChatHeadVisible;
  }

  createGroupModal() {
    this.modalRef = this.modalService.show(CreateGroupModalComponent);
  }

  onJoinGroup() {
    var joinGroup = {
      userId: this.loggedInUserId,
      groupId: this.currentGroup?.groupId,
    };
    this.groupService.joinGroup(joinGroup).subscribe({
      next: (res) => {
        this.isAVailableForChat = true;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  showUsers() {
    const initialState = {
      loggedInUserId: this.loggedInUserId,
    };
    this.modalRef = this.modalService.show(GroupUserModalComponent, {
      initialState,
    });
  }

  onBackGroupDetail(event: boolean) {
    this.showGroupUser = event;
  }

  showNotification() {
    this.requestService.getAllRequests(this.loggedInUserId).subscribe({
      next: (res: any) => {
        this.requests = res.requests;
        const initialState = {
          loggedInUserId: this.loggedInUserId,
          requests: this.requests,
        };
        this.modalRef = this.modalService.show(NotificationModalComponent, {
          initialState,
        });
        this.modalRef.setClass('modal-lg');
      },
    });
  }

  onLeaveGroup(groupId: Guid) {
    this.groupService
      .leaveGroup(groupId, this.loggedInUserId)
      .subscribe((res) => {
        this.isAVailableForChat = false;
        this.messageService.add({
          severity: 'success',
          summary: 'Leave Group',
          detail: 'User Leave Group successfully',
        });
      });
  }

  removeUserFromList(removeUserId: string) {
    this.chatService.RemoveUserFromList(this.loggedInUserId, removeUserId);
  }

  openFileDialog() {
    this.chooseInput.nativeElement.click();
  }

  handleFileSelected(event: any) {
    this.imgData = event.target.files[0];
    let reader = new FileReader();
    reader.onload = (event: any) => {
      this.previewImage = event.target.result;
      this.showAttachMent = false;
    };
    reader.readAsDataURL(this.imgData);
    console.log(this.imgData);
  }

  loadUserMessagesDuringPagination(): void {
    this.spinner = true;
    let receiverId =
      this.loggedInUserId == (this.chatUser as Mutual).userId
        ? (this.chatUser as Mutual).mutualId
        : (this.chatUser as Mutual).userId;
    if (receiverId) {
      this.userMessageService
        .getAllUserMessages(this.loggedInUserId, receiverId, this.page)
        .subscribe({
          next: (res: any) => {
            console.log(res.messages);
            this.messageLoadCount++;
            if (res.messages.length == 0) {
              this.hasMoreData = false;
            } else {
              this.messages.push(...res.messages);
              this.messages.forEach((x) => {
                x.type =
                  x.senderId === this.loggedInUserId ? 'sent' : 'recieved';
              });
              this.displayMessages = this.messages;
              console.log('display messages', this.displayMessages);
              this.cdr.detectChanges();
            }
          },
          error: (err) => {
            console.log(err?.error.message);
          },
          complete: () => {
            this.spinner = false;
          },
        });
    }
  }

  loadGroupMessagesDuringPagination(): void {
    this.spinner = true;
    if (this.currentGroup && this.currentGroup.groupId) {
      this.userMessageService
        .getAllGroupMessages(this.currentGroup.groupId, this.page)
        .subscribe({
          next: (res: any) => {
            console.log(res.messages);
            this.messageLoadCount++;
            if (res.messages.length == 0) {
              this.hasMoreData = false;
            } else {
              this.messages.push(...res.messages);
              this.messages.forEach((x) => {
                x.type =
                  x.senderId === this.loggedInUserId ? 'sent' : 'recieved';
              });
              this.displayMessages = this.messages;
              console.log('display messages', this.displayMessages);
              this.cdr.detectChanges();
            }
          },
          error: (err) => {
            console.log(err?.error.message);
          },
          complete: () => {
            this.spinner = false;
          },
        });
    }
  }

  onScroll(event: any): void {
    let scrollTop = event.target.scrollTop;
    if (scrollTop <= 1) {
      if (this.hasMoreData && this.messageLoadCount === this.page) {
        this.page++;
        if (this.currentGroup && this.currentGroup.groupId) {
          this.loadGroupMessagesDuringPagination();
          event.target.scrollTop = 150;
        } else {
          this.loadUserMessagesDuringPagination();
          event.target.scrollTop = 150;
        }
      }
    }
  }
}

// 380
