import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Apartment } from 'src/app/models/apartment';
import { ApartmentService } from 'src/app/services/apartment.service';

@Component({
  selector: 'app-apartment',
  templateUrl: './apartment.component.html',
  styleUrls: ['./apartment.component.css']
})
export class ApartmentComponent implements OnInit {

  constructor(private apartmentService: ApartmentService, private toastrService: ToastrService) { }

  apartments: Apartment[] = [];
  ngOnInit(): void {
    this.getApartments();
  }

  getApartments() {
    this.apartmentService.getApartments().subscribe({
      next: response => {
        this.apartments = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.message, "Error");
      }
    })
  }

  deleteApartment(apartmentId: number) {
    this.apartmentService.deleteApartment(apartmentId).subscribe({
      next: response => {
        this.toastrService.success(response.message, "Success");
        this.getApartments();
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

}
