import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Fee } from 'src/app/models/fee';
import { FeeService } from 'src/app/services/fee.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-user-fee',
  templateUrl: './user-fee.component.html',
  styleUrls: ['./user-fee.component.css']
})
export class UserFeeComponent implements OnInit {

  constructor(private feeService: FeeService, private toastrService: ToastrService, private localStorageService: LocalStorageService) { }

  fees: Fee[] = [];

  ngOnInit(): void {
    this.getUserFees();
  }

  getUserFees() {
    var userId: number = this.localStorageService.decodeToken();

    this.feeService.getUserFees(userId).subscribe({
      next: response => {
        this.fees = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

}
