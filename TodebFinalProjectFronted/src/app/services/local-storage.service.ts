import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  getItem(key: string) {
    return localStorage.getItem(key);
  }

  setItem(key: string, value: any) {
    localStorage.setItem(key, value);
  }

  deleteItem(key: string) {
    localStorage.removeItem(key);
  }

  decodeToken() {
    let token = localStorage.getItem('token');
    if (token != null) {
      let decodedJWT = JSON.parse(window.atob(token.split('.')[1]));
      return decodedJWT["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    }
  }

  getTypeFromToken() {
    let token = localStorage.getItem('token');
    if (token != null) {
      let decodedJWT = JSON.parse(window.atob(token.split('.')[1]));
      return decodedJWT["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    }
  }

}
