/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FlowLayoutService } from './flow-layout.service';

describe('Service: FlowLayout', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FlowLayoutService]
    });
  });

  it('should ...', inject([FlowLayoutService], (service: FlowLayoutService) => {
    expect(service).toBeTruthy();
  }));
});
