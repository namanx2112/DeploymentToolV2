import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewEditObjectComponent } from './new-edit-object.component';

describe('NewEditObjectComponent', () => {
  let component: NewEditObjectComponent;
  let fixture: ComponentFixture<NewEditObjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewEditObjectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewEditObjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
