import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Apartment } from 'src/app/models/apartment';
import { ApartmentService } from 'src/app/services/apartment.service';
import { FeeService } from 'src/app/services/fee.service';

@Component({
  selector: 'app-add-fee',
  templateUrl: './add-fee.component.html',
  styleUrls: ['./add-fee.component.css']
})
export class AddFeeComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private apartmentService: ApartmentService, private feeService: FeeService, private toastrService: ToastrService, private router: Router) { }

  feeGroup: FormGroup;
  isSingle: boolean = true;
  dateYear: number = new Date().getFullYear();
  apartments: Apartment[] = [];

  ngOnInit(): void {
    this.createFormGroup();
    this.getApartments();
  }

  createFormGroup() {
    this.feeGroup = this.formBuilder.group({
      amount: ["", [Validators.required, Validators.min(1)]],
      year: ["", [Validators.required, Validators.min(1990), Validators.max(this.dateYear)]],
      month: ["", [Validators.required, Validators.min(1), Validators.max(12)]],
      apartmentId: []
    });
  }

  getSingleClass(): String {
    if (this.isSingle) {
      return "btn btn-success mb-2 mt-4";
    }
    return "btn btn-outline-success mb-2 mt-4";
  }

  getBulkClass(): String {
    if (!this.isSingle) {
      return "btn btn-success mb-2 mt-4";
    }
    return "btn btn-outline-success mb-2 mt-4";
  }

  setSingle() {
    if (!this.isSingle) {
      this.isSingle = true;
    }
  }

  setBulk() {
    if (this.isSingle) {
      this.isSingle = false;
    }
  }

  getApartments() {
    this.apartmentService.getApartments().subscribe({
      next: response => {
        this.apartments = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    });
  }

  addFee() {
    let feeModel = Object.assign({
      amount: this.feeGroup.value.amount,
      apartmentId: this.feeGroup.value.apartmentId ?? 0,
      createdDate: new Date(this.feeGroup.value.year, this.feeGroup.value.month)
    });



    if (this.isSingle && this.feeGroup.value.apartmentId > 0) {
      if (this.feeGroup.valid) {
        this.feeService.addSingleFee(feeModel).subscribe({
          next: response => {
            this.toastrService.success(response.message, "Success");
          },
          error: responseError => {
            this.toastrService.error(responseError.error.message, "Error");
          }
        })
      }
    } else if (this.isSingle && (this.feeGroup.value.apartmentId == null || this.feeGroup.value.apartmentId <= 0)) {
      this.toastrService.error("Apartment is required", "Error");
    } else {
      console.log(this.feeGroup.value);
      this.feeService.addBulkFee(feeModel).subscribe({
        next: response => {
          this.toastrService.success(response.message, "Success");
        },
        error: responseError => {
          this.toastrService.error(responseError.error.message, "Error");
        }
      });
    }
  }


}
