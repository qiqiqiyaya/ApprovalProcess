import { Component, ElementRef, Injector, OnInit, viewChild, OnDestroy } from '@angular/core';
import { Graph, Snapline } from '@antv/x6';
import { NodeRegister } from '../nodes/node-register';
import { FlowGraph } from '../../models/flow-graph';
import { EditorService } from '../../services/editor.service';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css'],
  standalone: false
})
export class EditorComponent implements OnInit, OnDestroy {

  private graph: Graph;
  private contianer = viewChild<ElementRef>('graphContainer');

  constructor(
    injector: Injector,
    private editorService: EditorService,
  ) {
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

  ngOnDestroy(): void {
    
  }
}
