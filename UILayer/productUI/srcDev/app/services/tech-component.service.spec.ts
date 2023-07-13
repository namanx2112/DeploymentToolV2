import { TestBed } from '@angular/core/testing';

import { TechComponentService } from './tech-component.service';

describe('TechComponentService', () => {
  let service: TechComponentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TechComponentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
