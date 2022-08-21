import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Apartment } from 'src/app/models/apartment';
import { ApartmentService } from 'src/app/services/apartment.service';
import { BillService } from 'src/app/services/bill.service';

@Component({
  selector: 'app-add-bill',
  templateUrl: './add-bill.component.html',
  styleUrls: ['./add-bill.component.css']
})
export class AddBillComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private apartmentService: ApartmentService, private billService: BillService, private toastrService: ToastrService, private router: Router) { }

  billGroup: FormGroup;
  isSingle: boolean = true;
  dateYear: number = new Date().getFullYear();
  apartments: Apartment[] = [];

  ngOnInit(): void {
    this.createFormGroup();
    this.getApartments();
  }

  createFormGroup() {
    this.billGroup = this.formBuilder.group({
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

  addBill() {
    let billModel = Object.assign({
      amount: this.billGroup.value.amount,
      apartmentId: this.billGroup.value.apartmentId ?? 0,
      createDate: new Date(this.billGroup.value.year, this.billGroup.value.month)
    });



    if (this.isSingle && this.billGroup.value.apartmentId > 0) {
      if (this.billGroup.valid) {
        this.billService.addSingleFee(billModel).subscribe({
          next: response => {
            this.toastrService.success(response.message, "Success");
          },
          error: responseError => {
            this.toastrService.error(responseError.error.message, "Error");
          }
        })
      }
    } else if (this.isSingle && (this.billGroup.value.apartmentId == null || this.billGroup.value.apartmentId <= 0)) {
      this.toastrService.error("Apartment is required", "Error");
    } else {
      console.log(this.billGroup.value);
      this.billService.addBulkFee(billModel).subscribe({
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
