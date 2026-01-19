/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { GenerateNodeService } from './generate-node.service';

describe('Service: GenerateNode', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GenerateNodeService]
    });
  });

  it('should ...', inject([GenerateNodeService], (service: GenerateNodeService) => {
    expect(service).toBeTruthy();
  }));
});
