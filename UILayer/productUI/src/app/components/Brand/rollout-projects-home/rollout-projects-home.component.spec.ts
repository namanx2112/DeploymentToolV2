import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RolloutProjectsHomeComponent } from './rollout-projects-home.component';

describe('RolloutProjectsHomeComponent', () => {
  let component: RolloutProjectsHomeComponent;
  let fixture: ComponentFixture<RolloutProjectsHomeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RolloutProjectsHomeComponent]
    });
    fixture = TestBed.createComponent(RolloutProjectsHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
