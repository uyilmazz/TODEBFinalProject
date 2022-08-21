import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddBillComponent } from './components/add-bill/add-bill.component';
import { AddFeeComponent } from './components/add-fee/add-fee.component';
import { AddMessageComponent } from './components/add-message/add-message.component';
import { ApartmentAddComponent } from './components/apartment-add/apartment-add.component';
import { ApartmentUpdateComponent } from './components/apartment-update/apartment-update.component';
import { ApartmentComponent } from './components/apartment/apartment.component';
import { BillComponent } from './components/bill/bill.component';
import { FeeComponent } from './components/fee/fee.component';
import { LoginComponent } from './components/login/login.component';
import { MessageDetailComponent } from './components/message-detail/message-detail.component';
import { MessageComponent } from './components/message/message.component';
import { PaymentBillComponent } from './components/payment-bill/payment-bill.component';
import { PaymentFeeComponent } from './components/payment-fee/payment-fee.component';
import { UserAddComponent } from './components/user-add/user-add.component';
import { UserBillComponent } from './components/user-bill/user-bill.component';
import { UserFeeComponent } from './components/user-fee/user-fee.component';
import { UserMessagesComponent } from './components/user-messages/user-messages.component';
import { UserUpdateComponent } from './components/user-update/user-update.component';
import { UserComponent } from './components/user/user.component';
import { AuthorizeGuard } from './guards/authorize.guard';
import { LoginGuard } from './guards/login.guard';


const routes: Routes = [
  { path: "", redirectTo: "users", component: UserComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "users", component: UserComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "apartments", component: ApartmentComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "users/userAdd", component: UserAddComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "apartments/apartmentAdd", component: ApartmentAddComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "apartments/edit/:apartmentId", component: ApartmentUpdateComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "users/edit/:userId", component: UserUpdateComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "login", component: LoginComponent, canActivate: [LoginGuard] },
  { path: "messages", component: MessageComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "messages/detail/:messageId", component: MessageDetailComponent, pathMatch: 'full' },
  { path: "messages/type/:messageType", component: MessageComponent, pathMatch: 'full' },
  { path: "fees", component: FeeComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "fees/addFee", component: AddFeeComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "user/fees", component: UserFeeComponent, pathMatch: "full", canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'User' } },
  { path: "bills", component: BillComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "bills/addBill", component: AddBillComponent, canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'Admin' } },
  { path: "user/bills", component: UserBillComponent, pathMatch: "full", canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'User' } },
  { path: "user/messages", component: UserMessagesComponent, pathMatch: "full", canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'User' } },
  { path: "user/messages/new", component: AddMessageComponent, pathMatch: "full", canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'User' } },
  { path: "user/fees/payment/:feeId", component: PaymentFeeComponent, pathMatch: "full", canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'User' } },
  { path: "user/bills/payment/:billId", component: PaymentBillComponent, pathMatch: "full", canActivate: [LoginGuard, AuthorizeGuard], data: { expectedRole: 'User' } }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
