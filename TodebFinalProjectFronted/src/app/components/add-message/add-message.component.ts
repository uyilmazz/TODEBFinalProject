import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CreateMessage } from 'src/app/models/createMessage';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-add-message',
  templateUrl: './add-message.component.html',
  styleUrls: ['./add-message.component.css']
})
export class AddMessageComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private localStorageService: LocalStorageService, private messageService: MessageService, private toastrService: ToastrService, private router: Router) { }

  messageForm: FormGroup;

  ngOnInit(): void {
    this.createFormGroup();
  }

  createFormGroup() {
    this.messageForm = this.formBuilder.group({
      content: ["", [Validators.required, Validators.minLength(10)]],
      subject: ["", [Validators.required, Validators.minLength(3)]]
    });
  }

  sendMessage() {
    if (this.messageForm.valid) {
      let message: CreateMessage = Object.assign({
        subject: this.messageForm.value.subject,
        content: this.messageForm.value.content,
        senderId: this.localStorageService.decodeToken()
      });
      this.messageService.sendMessage(message).subscribe({
        next: response => {
          this.toastrService.success(response.message, "Success");
          this.router.navigate(['user/messages']);
        }, error: errorResponse => {
          this.toastrService.error(errorResponse.error.message, "Error");
        }
      })
    }
  }

}
