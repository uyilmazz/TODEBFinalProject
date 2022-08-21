import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApartmentBloc } from 'src/app/models/apartmentBloc';
import { ApartmentType } from 'src/app/models/apartmentType';
import { User } from 'src/app/models/user';
import { ApartmentService } from 'src/app/services/apartment.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-apartment-add',
  templateUrl: './apartment-add.component.html',
  styleUrls: ['./apartment-add.component.css']
})
export class ApartmentAddComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private apartmentService: ApartmentService, private toastrService: ToastrService, private userService: UserService, private router: Router) { }

  apartmentFormGroup: FormGroup;
  apartmentBlocs: ApartmentBloc[] = [];
  apartmentTypes: ApartmentType[] = [];
  persons: User[] = [];

  ngOnInit(): void {
    this.createFormGroup();
    this.getBlocs();
    this.getTypes();
    this.getPersons();
  }

  createFormGroup() {
    this.apartmentFormGroup = this.formBuilder.group({
      blocId: ["", [Validators.required, Validators.min(0)]],
      typeId: ["", [Validators.required, Validators.min(0)]],
      floor: ["", [Validators.required]],
      personId: [],
      apartmentNumber: ["", [Validators.required, Validators.min(0)]],
      isEmpty: ["", [Validators.required]]
    });
  }

  addApartment() {
    if (this.apartmentFormGroup.valid) {
      let createApartmentModel = Object.assign({}, this.apartmentFormGroup.value);
      this.apartmentService.addApartment(createApartmentModel).subscribe({
        next: response => {
          this.toastrService.success(response.message, "Success");
          this.router.navigate(["/apartments"])
        },
        error: responseError => {
          this.toastrService.error(responseError.error.message, "Error");
        }
      })
    }
  }

  getTypes() {
    this.apartmentService.getApartmentTypes().subscribe({
      next: response => {
        this.apartmentTypes = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

  getBlocs() {
    this.apartmentService.getApartmentBlocs().subscribe({
      next: response => {
        this.apartmentBlocs = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.message, "Error");
      }
    })
  }

  getPersons() {
    this.userService.getUsers().subscribe({
      next: response => {
        this.persons = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.message, "Error");
      }
    })
  }

}
