import { TestBed } from '@angular/core/testing';

import { ExStoreService } from './ex-store.service';

describe('ExStoreService', () => {
  let service: ExStoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExStoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
