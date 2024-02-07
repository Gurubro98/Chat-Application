import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import ValidateForm from 'src/app/helpers/validate-form';
import { AuthService } from 'src/app/services/auth.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  spinner : boolean = false;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private userStore: UserStoreService,
    private messageService: MessageService
  ) {}
  ngOnInit(): void {
    this.loginForm = this.fb.group({
      Email: ['', Validators.required],
      Password: ['', Validators.required],
    });
  }
  onLogin() {
    if (this.loginForm.valid) {
      this.spinner = true;
      this.auth.login(this.loginForm.value).subscribe({
        next: (res: any) => {
          this.auth.storeToken(res.token);
          const tokenPayload = this.auth.decodeToken();
          this.userStore.setNameForStore(tokenPayload.Name);
          this.userStore.setRoleForStore(tokenPayload.Role);
          this.userStore.setIdForStore(tokenPayload.Id);
          debugger;
          this.loginForm.reset();
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'User login successfully',
          });
          this.spinner = false;
          this.router.navigate(['online-users']);
        },
        error: (err) => {
          console.log(err);
          this.spinner = false;
        },
      });
    } else {
      console.log(' Form is not valid');
      ValidateForm.validateAllFormFields(this.loginForm);
    }
  }
}
