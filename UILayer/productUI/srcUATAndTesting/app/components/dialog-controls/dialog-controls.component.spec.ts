import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogControlsComponent } from './dialog-controls.component';

describe('DialogControlsComponent', () => {
  let component: DialogControlsComponent;
  let fixture: ComponentFixture<DialogControlsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogControlsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DialogControlsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
