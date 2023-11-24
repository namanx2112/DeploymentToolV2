import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExButtonComponent } from './ex-button.component';

describe('ExButtonComponent', () => {
  let component: ExButtonComponent;
  let fixture: ComponentFixture<ExButtonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExButtonComponent]
    });
    fixture = TestBed.createComponent(ExButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
