import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectPortfolioCellComponent } from './project-portfolio-cell.component';

describe('ProjectPortfolioCellComponent', () => {
  let component: ProjectPortfolioCellComponent;
  let fixture: ComponentFixture<ProjectPortfolioCellComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectPortfolioCellComponent]
    });
    fixture = TestBed.createComponent(ProjectPortfolioCellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
