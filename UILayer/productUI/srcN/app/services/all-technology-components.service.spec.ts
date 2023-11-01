import { TestBed } from '@angular/core/testing';

import { AllTechnologyComponentsService } from './all-technology-components.service';

describe('AllTechnologyComponentsService', () => {
  let service: AllTechnologyComponentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AllTechnologyComponentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
