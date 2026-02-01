/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ApprovalNodeService } from './approval-node.service';

describe('Service: ApprovalNode', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ApprovalNodeService]
    });
  });

  it('should ...', inject([ApprovalNodeService], (service: ApprovalNodeService) => {
    expect(service).toBeTruthy();
  }));
});
