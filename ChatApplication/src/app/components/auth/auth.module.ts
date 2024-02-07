import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { MessageService } from 'primeng/api';
import { CardModule } from 'primeng/card';
import { InputMaskModule } from 'primeng/inputmask';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { LoginComponent } from './login/login.component';
import { FileUploadModule } from 'primeng/fileupload';



@NgModule({
  declarations: [
    RegisterComponent,
    LoginComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    InputMaskModule,
    ProgressSpinnerModule,
    PasswordModule,
    ButtonModule,
    CalendarModule,
    ModalModule,
    FileUploadModule

  ],
  providers: [
    BsModalService,
    MessageService
  ]
})
export class AuthModule { }
