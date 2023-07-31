import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SonicHomePageComponent } from './sonic-home-page.component';

describe('SonicHomePageComponent', () => {
  let component: SonicHomePageComponent;
  let fixture: ComponentFixture<SonicHomePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SonicHomePageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SonicHomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
