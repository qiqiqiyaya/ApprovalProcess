/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GraphManagerService } from './graph-manager.service';

describe('Service: GraphManager', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GraphManagerService]
    });
  });

  it('should ...', inject([GraphManagerService], (service: GraphManagerService) => {
    expect(service).toBeTruthy();
  }));
});
