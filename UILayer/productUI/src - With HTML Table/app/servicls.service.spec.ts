import { TestBed } from '@angular/core/testing';

import { ServiclsService } from './servicls.service';

describe('ServiclsService', () => {
  let service: ServiclsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiclsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
