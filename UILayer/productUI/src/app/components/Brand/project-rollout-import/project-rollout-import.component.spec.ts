import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectRolloutImportComponent } from './project-rollout-import.component';

describe('ProjectRolloutImportComponent', () => {
  let component: ProjectRolloutImportComponent;
  let fixture: ComponentFixture<ProjectRolloutImportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectRolloutImportComponent]
    });
    fixture = TestBed.createComponent(ProjectRolloutImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
