import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserFeeComponent } from './user-fee.component';

describe('UserFeeComponent', () => {
  let component: UserFeeComponent;
  let fixture: ComponentFixture<UserFeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserFeeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserFeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
