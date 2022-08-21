import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Message } from 'src/app/models/message';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.css']
})
export class UserMessagesComponent implements OnInit {

  constructor(private messageService: MessageService, private toastrService: ToastrService, private localStorageService: LocalStorageService) { }
  messages: Message[] = [];

  ngOnInit(): void {
    this.getMessages();
  }


  getMessages() {
    var userId = this.localStorageService.decodeToken();
    this.messageService.getMessagesBySenderId(userId).subscribe({
      next: response => {
        this.messages = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }
}
