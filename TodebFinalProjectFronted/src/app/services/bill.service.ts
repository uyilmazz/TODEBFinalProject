import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bill } from '../models/bill';
import { CreateBillModel } from '../models/createBillModel';
import { DataResponseModel } from '../models/dataResponseModel';
import { Payment } from '../models/payment';
import { ResponseModel } from '../models/responseModel';

@Injectable({
  providedIn: 'root'
})
export class BillService {

  constructor(private httpClient: HttpClient) { }
  private apiUrl = "https://localhost:44355/api";

  getFees(): Observable<DataResponseModel<Bill[]>> {
    return this.httpClient.get<DataResponseModel<Bill[]>>(this.apiUrl + "/bills");
  }

  getUserFees(userId: number): Observable<DataResponseModel<Bill[]>> {
    return this.httpClient.get<DataResponseModel<Bill[]>>(this.apiUrl + "/bills/userId?userId=" + userId);
  }

  addSingleFee(billModel: CreateBillModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.apiUrl + "/bills/single", billModel);
  }

  addBulkFee(billModel: CreateBillModel): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.apiUrl + "/bills/bulkAdd", billModel);
  }

  paymentBill(payment: Payment): Observable<ResponseModel> {
    return this.httpClient.post<ResponseModel>(this.apiUrl + "/bills/paymentBill", payment);
  }

  getBill(billId: number): Observable<DataResponseModel<Bill>> {
    return this.httpClient.get<DataResponseModel<Bill>>(this.apiUrl + "/bills/id?id=" + billId);
  }

}
