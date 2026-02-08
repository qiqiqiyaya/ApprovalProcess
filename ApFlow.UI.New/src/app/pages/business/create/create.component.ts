import { Component, OnInit } from '@angular/core';
import { FlowGraphModule } from '../../flow-graph/flow-graph.module';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
  imports:[FlowGraphModule]
})
export class CreateComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
