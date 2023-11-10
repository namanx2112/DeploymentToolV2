import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageDropdownsComponent } from './manage-dropdowns.component';

describe('ManageDropdownsComponent', () => {
  let component: ManageDropdownsComponent;
  let fixture: ComponentFixture<ManageDropdownsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageDropdownsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageDropdownsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
