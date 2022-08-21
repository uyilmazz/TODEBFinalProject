import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { LocalStorageService } from './services/local-storage.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TodebFinalProject';


  constructor(private authService: AuthService) { };

  isLogin: boolean;

  ngOnInit(): void {
    this.isAuthenticated();
  }

  isAuthenticated() {
    this.isLogin = this.authService.isAuthenticated();
  }

}


