import { ComponentFixture, TestBed } from '@angular/core/testing';

import { POWorkflowTemplateComponent } from './poworkflow-template.component';

describe('POWorkflowTemplateComponent', () => {
  let component: POWorkflowTemplateComponent;
  let fixture: ComponentFixture<POWorkflowTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ POWorkflowTemplateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(POWorkflowTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
