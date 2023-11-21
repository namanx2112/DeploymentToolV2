import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectGlimpseComponent } from './project-glimpse.component';

describe('ProjectGlimpseComponent', () => {
  let component: ProjectGlimpseComponent;
  let fixture: ComponentFixture<ProjectGlimpseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectGlimpseComponent]
    });
    fixture = TestBed.createComponent(ProjectGlimpseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
