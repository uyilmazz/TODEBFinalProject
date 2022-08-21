import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { UserDetail } from '../models/userDetail';
import { AuthService } from '../services/auth.service';
import { LocalStorageService } from '../services/local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private toastrService: ToastrService,
    private router: Router,
    private localStorageService: LocalStorageService
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const expectedRole = route.data['expectedRole'];
    if (this.authService.isAuthenticated()) {
      if (this.localStorageService.getTypeFromToken() == expectedRole) {
        return true;
      } else {
        this.router.navigate(["user/fees"]);
        this.toastrService.warning("Bu sayfaya erişmek için yeterli yetkiniz yok", "Yetkiniz yok");
        return false;
      }
    } else {
      return false;
    }

  }
}
