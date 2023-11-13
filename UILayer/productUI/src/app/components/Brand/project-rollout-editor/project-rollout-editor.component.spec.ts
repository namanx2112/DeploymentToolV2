import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectRolloutEditorComponent } from './project-rollout-editor.component';

describe('ProjectRolloutEditorComponent', () => {
  let component: ProjectRolloutEditorComponent;
  let fixture: ComponentFixture<ProjectRolloutEditorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectRolloutEditorComponent]
    });
    fixture = TestBed.createComponent(ProjectRolloutEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
