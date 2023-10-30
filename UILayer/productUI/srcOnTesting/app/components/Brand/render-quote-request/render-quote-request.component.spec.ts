import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenderQuoteRequestComponent } from './render-quote-request.component';

describe('RenderQuoteRequestComponent', () => {
  let component: RenderQuoteRequestComponent;
  let fixture: ComponentFixture<RenderQuoteRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RenderQuoteRequestComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RenderQuoteRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
