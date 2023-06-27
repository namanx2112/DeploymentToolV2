import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentTerminalInstallationComponent } from './payment-terminal-installation.component';

describe('PaymentTerminalInstallationComponent', () => {
  let component: PaymentTerminalInstallationComponent;
  let fixture: ComponentFixture<PaymentTerminalInstallationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentTerminalInstallationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaymentTerminalInstallationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
