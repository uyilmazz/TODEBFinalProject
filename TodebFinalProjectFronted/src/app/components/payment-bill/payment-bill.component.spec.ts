import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentBillComponent } from './payment-bill.component';

describe('PaymentBillComponent', () => {
  let component: PaymentBillComponent;
  let fixture: ComponentFixture<PaymentBillComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentBillComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentBillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
