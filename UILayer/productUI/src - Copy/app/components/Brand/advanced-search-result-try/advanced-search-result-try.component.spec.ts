import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvancedSearchResultTryComponent } from './advanced-search-result-try.component';

describe('AdvancedSearchResultTryComponent', () => {
  let component: AdvancedSearchResultTryComponent;
  let fixture: ComponentFixture<AdvancedSearchResultTryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdvancedSearchResultTryComponent]
    });
    fixture = TestBed.createComponent(AdvancedSearchResultTryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
