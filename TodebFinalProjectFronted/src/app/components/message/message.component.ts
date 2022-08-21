import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { DataResponseModel } from 'src/app/models/dataResponseModel';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit {

  constructor(private messageService: MessageService, private toastrService: ToastrService, private activatedRouter: ActivatedRoute) { }

  messages: Message[] = [];

  ngOnInit(): void {

    this.activatedRouter.params.subscribe((params) => {
      switch (params["messageType"]) {
        case 'all':
          this.getMessages(this.messageService.getMessages());
          break;
        case 'news':
          this.getMessages(this.messageService.newMessages());
          break;
        case 'readed':
          this.getMessages(this.messageService.readedMessages());
          break;
        case 'unReaded':
          this.getMessages(this.messageService.unReadedMessages());
          break;
        default:
          this.getMessages(this.messageService.getMessages());
          break;
      }
    });
  }

  getMessages(funct: Observable<DataResponseModel<Message[]>>) {
    funct.subscribe({
      next: response => {
        this.messages = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

  getReadedMessages() {
    this.messageService.readedMessages().subscribe({
      next: response => {
        this.messages = response.data;
      },
      error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
      }
    })
  }

}
