import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthModule } from './components/auth/auth.module';
import { OnlineUsersComponent } from './components/online-users/online-users.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PickerModule } from '@ctrl/ngx-emoji-mart';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { CreateGroupModalComponent } from './components/create-group-modal/create-group-modal.component';
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';
import { GroupUserModalComponent } from './components/group-user-modal/group-user-modal.component';
import { NotificationModalComponent } from './components/notification-modal/notification-modal.component';
import { MenuModule } from 'primeng/menu';
import { FileUploadModule } from 'primeng/fileupload';
import { AvatarModule } from 'primeng/avatar';
import { UserDetailComponent } from './components/user-detail/user-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    OnlineUsersComponent,
    CreateGroupModalComponent,
    GroupUserModalComponent,
    NotificationModalComponent,
    UserDetailComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AuthModule,
    FormsModule,
    ReactiveFormsModule,
    PickerModule,
    ToastModule,
    ModalModule,
    MenuModule,
    FileUploadModule,
    AvatarModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    BsModalService,
    MessageService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
