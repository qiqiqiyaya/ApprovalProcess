import { Component, ElementRef, Injector, OnInit, viewChild } from '@angular/core';
import { Graph, Snapline } from '@antv/x6';
import { DagreLayout } from '@antv/layout';
import { NodeRegister, NodeShape } from '../nodes/node-register';
import { FlowGraph } from '../../models/flow-graph';
import { BranchGroupManager } from '../../models/branch-group-manager';
import { BranchGroupRenderer } from '../../models/branch-group-renderer';
import { EditorService } from '../../services/editor.service';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css'],
  standalone: false
})
export class EditorComponent implements OnInit {

  private graph: Graph;
  private contianer = viewChild<ElementRef>('graphContainer');
  constructor(injector: Injector, private editorService: EditorService) {
    NodeRegister.register(injector);
  }

  ngOnInit() {
    this.graph = new Graph({
      container: this.contianer()?.nativeElement,
      grid: true,
      panning: true,
      mousewheel: true,
    });
    this.graph.use(new Snapline({ enabled: true }));
    this.editorService.setGraph(this.graph);
    this.editorService.setflowGraph(FlowGraph.new());
    this.editorService.renderGraph();
  }
}
