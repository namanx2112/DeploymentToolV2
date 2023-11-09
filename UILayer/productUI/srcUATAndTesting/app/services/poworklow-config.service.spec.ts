import { TestBed } from '@angular/core/testing';

import { POWorkflowConfigService } from './poworklow-config.service';

describe('POWorklowConfigService', () => {
  let service: POWorkflowConfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(POWorkflowConfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
