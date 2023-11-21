import { TestBed } from '@angular/core/testing';

import { QuoteRequestWorkflowConfigService } from './quote-request-workflow-config.service';

describe('QuoteRequestWorkflowConfigService', () => {
  let service: QuoteRequestWorkflowConfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuoteRequestWorkflowConfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
