import { TestBed } from '@angular/core/testing';

import { SonicService } from './sonic.service';

describe('SonicService', () => {
  let service: SonicService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SonicService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
