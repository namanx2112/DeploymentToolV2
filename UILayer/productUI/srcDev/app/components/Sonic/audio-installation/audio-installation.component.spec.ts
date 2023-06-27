import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AudioInstallationComponent } from './audio-installation.component';

describe('AudioInstallationComponent', () => {
  let component: AudioInstallationComponent;
  let fixture: ComponentFixture<AudioInstallationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AudioInstallationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AudioInstallationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
