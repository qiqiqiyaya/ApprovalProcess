/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { NodeEventService } from './node-event.service';

describe('Service: NodeEvent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NodeEventService]
    });
  });

  it('should ...', inject([NodeEventService], (service: NodeEventService) => {
    expect(service).toBeTruthy();
  }));
});
