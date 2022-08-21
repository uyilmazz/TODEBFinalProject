import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Bill } from 'src/app/models/bill';
import { BillService } from 'src/app/services/bill.service';

@Component({
  selector: 'app-payment-bill',
  templateUrl: './payment-bill.component.html',
  styleUrls: ['./payment-bill.component.css']
})
export class PaymentBillComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private router: Router, private billService: BillService, private toastrService: ToastrService, private activatedRoute: ActivatedRoute) { }

  paymentForm: FormGroup;
  bill: Bill;
  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      if (params["billId"]) {
        this.getBill(params["billId"]);
      }
    });
    this.createFormGroup();
  }

  createFormGroup() {
    this.paymentForm = this.formBuilder.group({
      customerName: ["", [Validators.required]],
      cardNumber: ["", [Validators.required, Validators.minLength(16), Validators.maxLength(16)]],
      cvc: ["", [Validators.required]],
      expireMonth: ["", [Validators.required, Validators.min(1), Validators.max(12)]],
      expireYear: ["", [Validators.required]]
    })
  }


  payBill() {
    if (this.paymentForm.valid) {
      let model = Object.assign({
        paidId: this.bill.id,
        customerName: this.paymentForm.value.customerName,
        cardNumber: this.paymentForm.value.cardNumber,
        cvc: this.paymentForm.value.cvc,
        expireMonth: this.paymentForm.value.expireMonth,
        expireYear: this.paymentForm.value.expireYear,
        amount: this.bill.amount
      });

      this.billService.paymentBill(model).subscribe({
        next: response => {
          this.toastrService.success(response.message, "Success");
          this.router.navigate(['user/fees']);
        }, error: responseError => {
          this.toastrService.error(responseError.error.message, "Error");
        }
      })
    }
  }

  getBill(billId: number) {
    this.billService.getBill(billId).subscribe({
      next: response => {
        this.bill = response.data;
      }, error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

}
