import { ChangeDetectionStrategy, Component, DestroyRef, inject, Injector, OnDestroy, OnInit } from '@angular/core';
import { Graph, Node } from '@antv/x6';
import { GraphConstant } from '../../models/graph-constant';
import { NodeInfo, NodeType } from '../../models/node-description';
import { X6FlowGraph } from '../services/x6-flow-graph';
import { CustomShapeRegister } from '../custom-shape-register';
import { CustomShapeNames } from '../custom-shape-names';
import { FlowGraph } from '../services/flow-graph';
import { newAddNode, newEndNode, newStartNode } from '../models/nodes-export';

@Component({
  selector: 'app-flow-graph-editor',
  templateUrl: './flow-graph-editor.component.html',
  styleUrls: ['./flow-graph-editor.component.css'],
  standalone: false,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FlowGraphEditorComponent implements OnInit, OnDestroy {
  private graph: Graph;
  private readonly flowGraph = inject(FlowGraph);
  private readonly destroyRef = inject(DestroyRef);

  constructor(injector: Injector) {
    CustomShapeRegister.register(injector);
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    const container = document.getElementById('container');
    if (!container) throw new Error(`容器元素 #'container' 未找到`);
    this.graph = new Graph({
      container,
      grid: true,
      panning: true,
      mousewheel: true,
    });
    this.flowGraph.init(this.graph);
    this.initFlow();
  }

  /**
  * 创建示例流程 - 优化初始位置
  */
  private initFlow(): void {
    // 清空画布
    this.graph.clearCells();
    // 初始创建节点、连接、布局
    debugger;
    const startNode = newStartNode(this.flowGraph);
    const addNode = newAddNode(this.flowGraph);
    const endNode = newEndNode(this.flowGraph);

    startNode.inEdge(addNode.inEdge(endNode));
    this.flowGraph.layout();
  }


  ngOnDestroy(): void {
    this.graph?.dispose();
  }
}
