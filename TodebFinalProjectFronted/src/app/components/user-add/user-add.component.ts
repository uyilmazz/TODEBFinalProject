import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.css']
})
export class UserAddComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private userService: UserService, private toastrService: ToastrService, private router: Router) { }

  userAddFormGroup: FormGroup;

  ngOnInit(): void {
    this.createFormGroup();
  }

  createFormGroup() {
    this.userAddFormGroup = this.formBuilder.group({
      firstName: ["", [Validators.required, Validators.minLength(3)]],
      lastName: ["", [Validators.required, Validators.minLength(3)]],
      tcNo: ["", [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      email: ["", [Validators.required, Validators.email]],
      plakaNo: [],
      phoneNumber: []
    })
  }

  addUser() {
    if (this.userAddFormGroup.valid) {
      let userModel = Object.assign({}, this.userAddFormGroup.value);
      this.userService.addUser(userModel).subscribe({
        next: response => {
          this.toastrService.success(response.data, "User Password");
          this.router.navigate(["/users"]);
        }, error: responseError => {
          this.toastrService.error(responseError.error.message, "Error");
        }
      })
    }
  }

}

