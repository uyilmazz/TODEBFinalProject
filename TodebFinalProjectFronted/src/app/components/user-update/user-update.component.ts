import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UpdateUserModel } from 'src/app/models/updateUserModel';
import { User } from 'src/app/models/user';
import { UserType } from 'src/app/models/userType';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {

  constructor(private userService: UserService, private formBuilder: FormBuilder, private toastrService: ToastrService, private activatedRoute: ActivatedRoute, private router: Router) { }

  currentUser: User;
  userTypes: UserType[];
  userFormGroup: FormGroup;

  ngOnInit(): void {
    this.getUserTypes();
    this.activatedRoute.params.subscribe((params) => {
      if (params["userId"]) {
        this.getUser(params["userId"]);
      }
    });
    this.createFormGroup();
  }

  createFormGroup() {
    this.userFormGroup = this.formBuilder.group({
      id: new FormControl({ value: '', disabled: true }, Validators.required),
      firstName: ["", [Validators.required, Validators.minLength(3)]],
      lastName: ["", [Validators.required, Validators.minLength(3)]],
      tcNo: ["", [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      email: ["", [Validators.required, Validators.email]],
      plakaNo: [],
      phoneNumber: []
    })
  }

  getUser(userId: number) {
    this.userService.getUser(userId).subscribe({
      next: response => {
        this.currentUser = response.data;
        this.userFormGroup.controls["id"].setValue(this.currentUser.id);
        this.userFormGroup.controls['firstName'].setValue(this.currentUser.firstName);
        this.userFormGroup.controls['lastName'].setValue(this.currentUser.lastName);
        this.userFormGroup.controls['tcNo'].setValue(this.currentUser.tcNo);
        this.userFormGroup.controls['email'].setValue(this.currentUser.email);
        this.userFormGroup.controls['plakaNo'].setValue(this.currentUser.plakaNo);
        this.userFormGroup.controls['phoneNumber'].setValue(this.currentUser.phoneNumber);
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

  getUserTypes() {
    this.userService.getUserTypes().subscribe({
      next: response => {
        this.userTypes = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

  updateUser() {
    if (this.userFormGroup.valid) {
      let userModel: UpdateUserModel = Object.assign({}, this.userFormGroup.value);
      userModel.id = this.currentUser.id;
      userModel.typeId = this.userTypes.find(userType => userType.type == this.currentUser.typeName)?.id ?? 2;
      this.userService.updateUser(userModel).subscribe({
        next: response => {
          this.toastrService.success(response.message, "Success");
          this.router.navigate(["/users"]);
        },
        error: responseError => {
          this.toastrService.error(responseError.error.message, "Error");
        }
      })
    }
  }

}
