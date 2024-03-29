import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchFieldsComponent } from './search-fields.component';

describe('SearchFieldsComponent', () => {
  let component: SearchFieldsComponent;
  let fixture: ComponentFixture<SearchFieldsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SearchFieldsComponent]
    });
    fixture = TestBed.createComponent(SearchFieldsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
