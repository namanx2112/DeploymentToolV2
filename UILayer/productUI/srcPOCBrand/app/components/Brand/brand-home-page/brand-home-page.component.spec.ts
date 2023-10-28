import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BrandHomePageComponent } from './brand-home-page.component';

describe('BrandHomePageComponent', () => {
  let component: BrandHomePageComponent;
  let fixture: ComponentFixture<BrandHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BrandHomePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BrandHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
