import { TestBed } from '@angular/core/testing';

import { RolloutProjectsService } from './rollout-projects.service';

describe('RolloutProjectsService', () => {
  let service: RolloutProjectsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RolloutProjectsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
