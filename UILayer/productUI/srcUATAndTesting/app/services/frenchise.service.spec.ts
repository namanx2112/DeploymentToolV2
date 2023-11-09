import { TestBed } from '@angular/core/testing';

import { FrenchiseService } from './frenchise.service';

describe('FrenchiseService', () => {
  let service: FrenchiseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FrenchiseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
