import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserDetail } from 'src/app/models/userDetail';
import { AuthService } from 'src/app/services/auth.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private authService: AuthService, private localStorage: LocalStorageService, private router: Router, private toastrService: ToastrService) { }
  userType: string;
  user: UserDetail;
  ngOnInit(): void {
    this.getUserType();
  }

  getUserType() {
    this.authService.getUser(this.localStorage.decodeToken()).subscribe(response => {
      this.user = response.data;
    })
  }

  logOut() {
    this.authService.logOut();
    this.router.navigate([""])
    this.toastrService.success("Hesabınızdan çıkış yapıldı", "Çıkış yapıldı");
    window.location.reload();
  }
}
