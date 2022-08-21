import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Bill } from 'src/app/models/bill';
import { BillService } from 'src/app/services/bill.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-user-bill',
  templateUrl: './user-bill.component.html',
  styleUrls: ['./user-bill.component.css']
})
export class UserBillComponent implements OnInit {

  constructor(private localStorageService: LocalStorageService, private billService: BillService, private toastrService: ToastrService) { }
  bills: Bill[] = [];

  ngOnInit(): void {
    this.getUserBills();
  }

  getUserBills() {
    var userId: number = this.localStorageService.decodeToken();

    this.billService.getUserFees(userId).subscribe({
      next: response => {
        this.bills = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

}
