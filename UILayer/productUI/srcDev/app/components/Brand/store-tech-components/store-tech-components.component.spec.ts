import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StoreTechComponentsComponent } from './store-tech-components.component';

describe('StoreTechComponentsComponent', () => {
  let component: StoreTechComponentsComponent;
  let fixture: ComponentFixture<StoreTechComponentsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StoreTechComponentsComponent]
    });
    fixture = TestBed.createComponent(StoreTechComponentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
