import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { SideBarComponent } from './components/side-bar/side-bar.component';
import { UserComponent } from './components/user/user.component';
import { ApartmentComponent } from './components/apartment/apartment.component';
import { UserAddComponent } from './components/user-add/user-add.component';
import { ApartmentAddComponent } from './components/apartment-add/apartment-add.component';
import { ApartmentUpdateComponent } from './components/apartment-update/apartment-update.component';
import { UserUpdateComponent } from './components/user-update/user-update.component';
import { MessageComponent } from './components/message/message.component';
import { LoginComponent } from './components/login/login.component';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { MessageDetailComponent } from './components/message-detail/message-detail.component';
import { BillComponent } from './components/bill/bill.component';
import { FeeComponent } from './components/fee/fee.component';
import { AddFeeComponent } from './components/add-fee/add-fee.component';
import { AddBillComponent } from './components/add-bill/add-bill.component';
import { UserFeeComponent } from './components/user-fee/user-fee.component';
import { UserBillComponent } from './components/user-bill/user-bill.component';
import { UserMessagesComponent } from './components/user-messages/user-messages.component';
import { AddMessageComponent } from './components/add-message/add-message.component';
import { PaymentFeeComponent } from './components/payment-fee/payment-fee.component';
import { PaymentBillComponent } from './components/payment-bill/payment-bill.component';
import { DatePipe } from '@angular/common';


@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    SideBarComponent,
    UserComponent,
    ApartmentComponent,
    UserAddComponent,
    ApartmentAddComponent,
    ApartmentUpdateComponent,
    UserUpdateComponent,
    MessageComponent,
    LoginComponent,
    MessageDetailComponent,
    BillComponent,
    FeeComponent,
    AddFeeComponent,
    AddBillComponent,
    UserFeeComponent,
    UserBillComponent,
    UserMessagesComponent,
    AddMessageComponent,
    PaymentFeeComponent,
    PaymentBillComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: "toast-bottom-right"
    }),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true },
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
