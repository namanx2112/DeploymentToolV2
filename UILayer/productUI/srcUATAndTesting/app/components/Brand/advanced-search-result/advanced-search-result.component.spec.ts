import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvancedSearchResultComponent } from './advanced-search-result.component';

describe('AdvancedSearchResultComponent', () => {
  let component: AdvancedSearchResultComponent;
  let fixture: ComponentFixture<AdvancedSearchResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdvancedSearchResultComponent]
    });
    fixture = TestBed.createComponent(AdvancedSearchResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
