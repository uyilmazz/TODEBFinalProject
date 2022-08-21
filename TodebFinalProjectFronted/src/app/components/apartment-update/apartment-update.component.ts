import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router, TitleStrategy } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Apartment } from 'src/app/models/apartment';
import { ApartmentBloc } from 'src/app/models/apartmentBloc';
import { ApartmentType } from 'src/app/models/apartmentType';
import { User } from 'src/app/models/user';
import { ApartmentService } from 'src/app/services/apartment.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-apartment-update',
  templateUrl: './apartment-update.component.html',
  styleUrls: ['./apartment-update.component.css']
})
export class ApartmentUpdateComponent implements OnInit {

  constructor(private apartmentService: ApartmentService, private userService: UserService, private formBuilder: FormBuilder, private toastrService: ToastrService, private activatedRouter: ActivatedRoute, private router: Router) { }

  currentApartment: Apartment;
  apartmentTypes: ApartmentType[];
  apartmentBlocs: ApartmentBloc[];
  persons: User[];
  apartmentFormGroup: FormGroup;

  ngOnInit(): void {

    this.activatedRouter.params.subscribe((params) => {
      if (params["apartmentId"]) {
        this.getBlocs();
        this.getPersons();
        this.getTypes();
        this.getApartment(params["apartmentId"]);
      }
    });
    this.createFormGroup();
  }

  createFormGroup() {
    this.apartmentFormGroup = this.formBuilder.group({
      id: new FormControl({ value: '', disabled: true }, Validators.required),
      blocId: ["", [Validators.required, Validators.min(0)]],
      typeId: ["", [Validators.required, Validators.min(0)]],
      floor: ["", [Validators.required]],
      personId: [],
      apartmentNumber: ["", [Validators.required, Validators.min(0)]],
      isEmpty: ["", [Validators.required]]
    })
  }


  getApartment(apartmentId: number) {
    this.apartmentService.getApartment(apartmentId).subscribe({
      next: response => {
        this.currentApartment = response.data;
        this.apartmentFormGroup.controls["id"].setValue(this.currentApartment.id);
        this.apartmentFormGroup.controls['blocId'].setValue(this.apartmentBlocs.find(a => a.name == this.currentApartment.blocName)?.id);
        this.apartmentFormGroup.controls['typeId'].setValue(this.apartmentTypes.find(a => a.name == this.currentApartment.typeName)?.id);
        this.apartmentFormGroup.controls['floor'].setValue(this.currentApartment.floor);
        this.apartmentFormGroup.controls['personId'].setValue(this.persons.find(p => p.firstName + " " + p.lastName == this.currentApartment.userName)?.id);
        this.apartmentFormGroup.controls['apartmentNumber'].setValue(this.currentApartment.apartmentNumber);
        this.apartmentFormGroup.controls['isEmpty'].setValue(this.currentApartment.isEmpty);
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
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

  updateApartment() {
    if (this.apartmentFormGroup.valid) {
      let updatedApartment = Object.assign({}, this.apartmentFormGroup.value);
      updatedApartment.id = this.currentApartment.id;
      this.apartmentService.updateApartment(updatedApartment).subscribe({
        next: response => {
          this.toastrService.success(response.message, "Success");
          this.router.navigate(["/apartments"]);
        },
        error: responseError => {
          this.toastrService.error(responseError.error.message, "Error");
        }
      })
    }
  }

}
