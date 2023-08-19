import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenderDateChangeTemplateComponent } from './render-date-change-template.component';

describe('RenderDateChangeTemplateComponent', () => {
  let component: RenderDateChangeTemplateComponent;
  let fixture: ComponentFixture<RenderDateChangeTemplateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RenderDateChangeTemplateComponent]
    });
    fixture = TestBed.createComponent(RenderDateChangeTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
