import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Message } from 'src/app/models/message';
import { AuthService } from 'src/app/services/auth.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { MessageService } from 'src/app/services/message.service';
import { DatePipe } from '@angular/common'

@Component({
  selector: 'app-message-detail',
  templateUrl: './message-detail.component.html',
  styleUrls: ['./message-detail.component.css']
})
export class MessageDetailComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private datepipe: DatePipe, private messageService: MessageService, private toastrService: ToastrService, private localStorageService: LocalStorageService, private authService: AuthService, private activatedRoute: ActivatedRoute, private router: Router) { }

  messageForm: FormGroup
  message: Message;
  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      if (params["messageId"]) {
        this.getMessage(params["messageId"]);
      }
    });
    this.createFormGroup();
  }

  createFormGroup() {
    this.messageForm = this.formBuilder.group({
      userName: [],
      subject: [],
      content: [],
      createdDate: []
    })
  }

  getMessage(messageId: number) {
    this.messageService.getMessage(messageId).subscribe({
      next: response => {

        this.message = response.data;
        this.messageForm.controls["userName"].setValue(this.message.userName);
        this.messageForm.controls['createdDate'].setValue(this.datepipe.transform(this.message.createdDate, 'yyyy-MM-dd hh:mm'));
        this.messageForm.controls['subject'].setValue(this.message.subject);
        this.messageForm.controls['content'].setValue(this.message.content);

      }, error: responseError => {
        this.toastrService.error(responseError.error.message, "Error");
        return this.router.navigate(["messages"]);
      }
    })
  }

}
