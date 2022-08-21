import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Fee } from 'src/app/models/fee';
import { FeeService } from 'src/app/services/fee.service';

@Component({
  selector: 'app-payment-fee',
  templateUrl: './payment-fee.component.html',
  styleUrls: ['./payment-fee.component.css']
})
export class PaymentFeeComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private router: Router, private feeService: FeeService, private toastrService: ToastrService, private activatedRoute: ActivatedRoute) { }

  paymentForm: FormGroup;
  fee: Fee;
  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      if (params["feeId"]) {
        this.getFee(params["feeId"]);
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


  payFee() {
    if (this.paymentForm.valid) {
      let model = Object.assign({
        paidId: this.fee.id,
        customerName: this.paymentForm.value.customerName,
        cardNumber: this.paymentForm.value.cardNumber,
        cvc: this.paymentForm.value.cvc,
        expireMonth: this.paymentForm.value.expireMonth,
        expireYear: this.paymentForm.value.expireYear,
        amount: this.fee.amount
      });

      this.feeService.paymentFee(model).subscribe({
        next: response => {
          this.toastrService.success(response.message, "Success");
          this.router.navigate(['user/fees']);
        }, error: responseError => {
          this.toastrService.error(responseError.error.message, "Error");
        }
      })
    }
  }

  getFee(feeId: number) {
    this.feeService.getFee(feeId).subscribe({
      next: response => {
        this.fee = response.data;
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




