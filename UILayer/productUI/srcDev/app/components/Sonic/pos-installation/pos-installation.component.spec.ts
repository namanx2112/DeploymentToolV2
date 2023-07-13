import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PosInstallationComponent } from './pos-installation.component';

describe('PosInstallationComponent', () => {
  let component: PosInstallationComponent;
  let fixture: ComponentFixture<PosInstallationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PosInstallationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PosInstallationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
