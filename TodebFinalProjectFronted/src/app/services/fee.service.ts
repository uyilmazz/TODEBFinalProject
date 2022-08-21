import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateFeeModel } from '../models/createFeeModel';
import { DataResponseModel } from '../models/dataResponseModel';
import { Fee } from '../models/fee';
import { Payment } from '../models/payment';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class FeeService {

  constructor(private httpClient: HttpClient) { }
  private apiUrl = "https://localhost:44355/api";

  getFees(): Observable<DataResponseModel<Fee[]>> {
    return this.httpClient.get<DataResponseModel<Fee[]>>(this.apiUrl + "/fees");
  }

  getUserFees(userId: number): Observable<DataResponseModel<Fee[]>> {
    return this.httpClient.get<DataResponseModel<Fee[]>>(this.apiUrl + "/fees/userId?userId=" + userId);
  }
  getFee(feeId: number): Observable<DataResponseModel<Fee>> {
    return this.httpClient.get<DataResponseModel<Fee>>(this.apiUrl + "/fees/id?id=" + feeId);
  }

  addSingleFee(feeModel: CreateFeeModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.apiUrl + "/fees/single", feeModel);
  }

  addBulkFee(feeModel: CreateFeeModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.apiUrl + "/Fees/bulkAdd", feeModel);
  }

  paymentFee(payment: Payment): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.apiUrl + "/fees/paymentFee", payment);
  }

}
