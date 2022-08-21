import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataResponseModel } from '../models/dataResponseModel';
import { LoginModel } from '../models/loginModel';
import { TokenModel } from '../models/tokenModel';
import { UserDetail } from '../models/userDetail';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient, private localStorageService: LocalStorageService) { }
  private apiUrl = "https://localhost:44355/api";
  user: UserDetail;

  login(loginModel: LoginModel): Observable<DataResponseModel<TokenModel>> {
    let newUrl = this.apiUrl + "/auth/login";
    return this.httpClient.post<DataResponseModel<TokenModel>>(newUrl, loginModel);
  }

  getUser(userId: number): Observable<DataResponseModel<UserDetail>> {
    let newUrl = this.apiUrl + "/Persons/id?id=" + userId;
    return this.httpClient.get<DataResponseModel<UserDetail>>(newUrl);
  }


  isAuthenticated() {
    if (this.localStorageService.getItem("token")) {
      return true;
    } else {
      return false;
    }
  }

  logOut() {
    this.localStorageService.deleteItem("token");
  }

}
