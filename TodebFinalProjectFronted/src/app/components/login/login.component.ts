import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private router: Router, private toastrService: ToastrService, private authService: AuthService, private localStorageService: LocalStorageService) { }

  ngOnInit(): void {
    this.craeteFormGroup();
  }

  loginForm: FormGroup;


  craeteFormGroup() {
    this.loginForm = this.formBuilder.group({
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required]]
    });
  }

  login() {
    if (this.loginForm.valid) {
      let loginModel = Object.assign({}, this.loginForm.value);
      this.authService.login(loginModel).subscribe({
        next: response => {
          this.toastrService.success("You have successfully logged in.");
          this.localStorageService.setItem("token", response.data.token);
          var userId = this.localStorageService.decodeToken();
          this.authService.getUser(userId).subscribe({
            next: response => {
              this.authService.user = response.data;
              this.router.navigate([""]);
              window.location.reload();
            },
            error: resError => {
              this.toastrService.error(resError.error.message, "Error");
            }
          })
        },
        error: responseError => {
          this.toastrService.error(responseError.error.message, "Error");
        }
      })
    }
  }



}
