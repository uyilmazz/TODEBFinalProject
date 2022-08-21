import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Bill } from 'src/app/models/bill';
import { BillService } from 'src/app/services/bill.service';

@Component({
  selector: 'app-bill',
  templateUrl: './bill.component.html',
  styleUrls: ['./bill.component.css']
})
export class BillComponent implements OnInit {

  constructor(private billService: BillService, private toastrService: ToastrService) { }
  bills: Bill[] = [];
  ngOnInit(): void {
    this.getBills();
  }


  getBills() {
    this.billService.getFees().subscribe({
      next: response => {
        this.bills = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

}
