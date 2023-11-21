import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RolloutProjectTableComponent } from './rollout-project-table.component';

describe('RolloutProjectTableComponent', () => {
  let component: RolloutProjectTableComponent;
  let fixture: ComponentFixture<RolloutProjectTableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RolloutProjectTableComponent]
    });
    fixture = TestBed.createComponent(RolloutProjectTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
