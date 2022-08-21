import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor(private userService: UserService, private toastrService: ToastrService) { }

  users: User[] = [];

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers().subscribe({
      next: response => {
        this.users = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.message, "Error");
      }
    })
  }

  deleteUser(userId: number) {
    this.userService.deleteUser(userId).subscribe({
      next: response => {
        this.toastrService.success(response.message, "Success");
        this.getUsers();
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

}
