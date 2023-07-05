import { TestBed } from '@angular/core/testing';

import { POWorklowConfigService } from './poworklow-config.service';

describe('POWorklowConfigService', () => {
  let service: POWorklowConfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(POWorklowConfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
