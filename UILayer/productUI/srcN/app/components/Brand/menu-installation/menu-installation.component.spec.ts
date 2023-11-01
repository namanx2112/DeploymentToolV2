import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuInstallationComponent } from './menu-installation.component';

describe('MenuInstallationComponent', () => {
  let component: MenuInstallationComponent;
  let fixture: ComponentFixture<MenuInstallationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MenuInstallationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MenuInstallationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
