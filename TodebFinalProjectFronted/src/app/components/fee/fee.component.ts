import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Fee } from 'src/app/models/fee';
import { FeeService } from 'src/app/services/fee.service';

@Component({
  selector: 'app-fee',
  templateUrl: './fee.component.html',
  styleUrls: ['./fee.component.css']
})
export class FeeComponent implements OnInit {

  constructor(private feeService: FeeService, private toastrService: ToastrService) { }
  fees: Fee[] = [];

  ngOnInit(): void {
    this.getFees();
  }


  getFees() {
    this.feeService.getFees().subscribe({
      next: response => {
        this.fees = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

}
