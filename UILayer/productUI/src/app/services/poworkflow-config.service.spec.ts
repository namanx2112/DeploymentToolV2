import { TestBed } from '@angular/core/testing';

import { POWorkflowConfigService } from './poworkflow-config.service';

describe('POWorkflowConfigService', () => {
  let service: POWorkflowConfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(POWorkflowConfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
