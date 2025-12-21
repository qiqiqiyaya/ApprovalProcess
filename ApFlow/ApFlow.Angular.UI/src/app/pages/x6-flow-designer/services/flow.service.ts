import { inject, Injectable } from '@angular/core';
import { X6FlowGraph } from './x6-flow-graph';
import { FlowFormService } from './flow-form.service';

@Injectable()
export class FlowService {

  graph = inject(X6FlowGraph);
  form = inject(FlowFormService);

  constructor() { }

}
