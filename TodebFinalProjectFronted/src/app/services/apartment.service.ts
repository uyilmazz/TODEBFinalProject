import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Apartment } from '../models/apartment';
import { ApartmentBloc } from '../models/apartmentBloc';
import { ApartmentType } from '../models/apartmentType';
import { CreateApartmanModel } from '../models/createApartmentModel';
import { DataResponseModel } from '../models/dataResponseModel';
import { ResponseModel } from '../models/responseModel';
import { UpdateApartmentModel } from '../models/updateApartmentModel';

@Injectable({
  providedIn: 'root'
})
export class ApartmentService {

  apiUrl: string = "https://localhost:44355/api";

  constructor(private httpClient: HttpClient) { }

  getApartments(): Observable<DataResponseModel<Apartment[]>> {
    return this.httpClient.get<DataResponseModel<Apartment[]>>(this.apiUrl + "/Apartments");
  }

  getApartment(apartmentId: number): Observable<DataResponseModel<Apartment>> {
    return this.httpClient.get<DataResponseModel<Apartment>>(this.apiUrl + "/Apartments/id?id=" + apartmentId);
  }

  getApartmentTypes(): Observable<DataResponseModel<ApartmentType[]>> {
    return this.httpClient.get<DataResponseModel<ApartmentType[]>>(this.apiUrl + "/ApartmentTypes");
  }

  getApartmentBlocs(): Observable<DataResponseModel<ApartmentBloc[]>> {
    return this.httpClient.get<DataResponseModel<ApartmentBloc[]>>(this.apiUrl + "/ApartmentBlocs");
  }

  addApartment(newApartment: CreateApartmanModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.apiUrl + "/Apartments", newApartment);
  }

  updateApartment(updatedApartment: UpdateApartmentModel): Observable<ResponseModel> {
    return this.httpClient.patch<ResponseModel>(this.apiUrl + "/Apartments", updatedApartment);
  }

  deleteApartment(apartmentId: number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(this.apiUrl + "/Apartments?id=" + apartmentId);
  }
}

