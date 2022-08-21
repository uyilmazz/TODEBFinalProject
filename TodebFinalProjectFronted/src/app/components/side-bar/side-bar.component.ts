import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {

  constructor(private authService: AuthService, private localStorageService: LocalStorageService) { }
  userType: string;
  ngOnInit(): void {
    this.getUserType();
  }

  getUserType() {
    this.authService.getUser(this.localStorageService.decodeToken()).subscribe({
      next: response => {
        this.userType = response.data.typeName;;
      }, error: responseError => {
        console.log("error : " + responseError.error.message);
      }
    })
  }

  isAdmin() {
    return this.userType == "Admin";
  }
}
