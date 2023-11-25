import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RolloutOwnProjectsComponent } from './rollout-own-projects.component';

describe('RolloutOwnProjectsComponent', () => {
  let component: RolloutOwnProjectsComponent;
  let fixture: ComponentFixture<RolloutOwnProjectsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RolloutOwnProjectsComponent]
    });
    fixture = TestBed.createComponent(RolloutOwnProjectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
