import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import ValidateForm, {
  ConfirmedValidator,
} from 'src/app/helpers/validate-form';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  signupForm!: FormGroup;
  imageUrl: any;
  imgData: any;
  userData = new FormData();
  postImageChange = false;
  previousImage!: string;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.signupForm = this.fb.group(
      {
        name: [
          '',
          [
            Validators.required,
            Validators.minLength(3),
            Validators.maxLength(20),
            Validators.pattern('^(([A-za-z]+[\\s]{1}[A-za-z]+)|([A-Za-z]+))$'),
          ],
        ],
        email: [
          '',
          [
            Validators.required,
            Validators.pattern(
              '^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$'
            ),
          ],
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(7),
            Validators.pattern(
              '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{6,}'
            ),
          ],
        ],
        confirmPassword: ['', Validators.required],
        profilePicture : [],
      },
      { validator: ConfirmedValidator('password', 'confirmPassword') }
    );
  }

  // get all filed
  get f() {
    return this.signupForm.controls;
  }

  onCloseImage() {
    this.imageUrl = null;
  }

  onUpload(data: any) {
    console.log(data);
    this.imgData = <Form>data.files[0];
    console.log(this.imgData);
    let reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
      this.postImageChange = true;
      console.log('url', btoa(this.imageUrl));
    };
    reader.readAsDataURL(this.imgData);
    console.log(this.imgData);
  }

  onSignUp() {
    if (this.signupForm.valid) {
      if (this.imgData) {
        this.userData.append('ProfilePic', this.imgData, this.imgData.name);
      }
      this.userData.append('Name', this.signupForm.value.name);
      this.userData.append('Email', this.signupForm.value.email);
      this.userData.append('Password', this.signupForm.value.password);
      this.userData.append(
        'ConfirmPassword',
        this.signupForm.value.confirmPassword
      );
      this.auth.signUp(this.userData).subscribe({
        next: (res: any) => {
          this.signupForm.reset();
          this.router.navigate(['login']);
        },
        error: (err: any) => {
          console.log(err?.error.message);
        },
      });
    } else {
      console.log(' Form is not valid');
      ValidateForm.validateAllFormFields(this.signupForm);
    }
  }
}
