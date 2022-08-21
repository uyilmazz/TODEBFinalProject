import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateUserModel } from '../models/createUserModel';
import { DataResponseModel } from '../models/dataResponseModel';
import { ResponseModel } from '../models/responseModel';
import { UpdateUserModel } from '../models/updateUserModel';
import { User } from '../models/user';
import { UserType } from '../models/userType';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  private apiUrl = "https://localhost:44355/api";

  getUsers(): Observable<DataResponseModel<User[]>> {
    return this.httpClient.get<DataResponseModel<User[]>>(this.apiUrl + "/Persons");
  }

  getUser(userId: number): Observable<DataResponseModel<User>> {
    return this.httpClient.get<DataResponseModel<User>>(this.apiUrl + "/Persons/id?id=" + userId);
  }

  getUserTypes(): Observable<DataResponseModel<UserType[]>> {
    return this.httpClient.get<DataResponseModel<UserType[]>>(this.apiUrl + "/PersonTypes");
  }

  addUser(user: CreateUserModel): Observable<DataResponseModel<string>> {
    return this.httpClient.post<DataResponseModel<string>>(this.apiUrl + "/Persons", user);
  }

  updateUser(updatedUser: UpdateUserModel): Observable<ResponseModel> {
    return this.httpClient.patch<ResponseModel>(this.apiUrl + "/Persons", updatedUser);
  }

  deleteUser(userId: number): Observable<ResponseModel> {
    return this.httpClient.delete<ResponseModel>(this.apiUrl + "/Persons?id=" + userId);
  }
}

