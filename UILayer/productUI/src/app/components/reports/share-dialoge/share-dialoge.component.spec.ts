import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShareDialogeComponent } from './share-dialoge.component';

describe('ShareDialogeComponent', () => {
  let component: ShareDialogeComponent;
  let fixture: ComponentFixture<ShareDialogeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShareDialogeComponent]
    });
    fixture = TestBed.createComponent(ShareDialogeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
