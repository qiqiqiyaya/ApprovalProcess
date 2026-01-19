import { inject, Injectable } from '@angular/core';
import { LayoutService } from './layout.service';
import { FlowGraph } from './flow-graph';

@Injectable()
export class GenerateNodeService {
  private nodeCounter = 0;
  layoutService = inject(LayoutService);
  flowGraph = inject(FlowGraph);

  constructor() { }

  /**
   * 添加节点（支持延迟布局）
   */
  addNode(type: string, label: string, autoLayout: boolean = true): any {
    const id = `node_${type}_${this.nodeCounter++}`;
    const node = this.flowGraph.graph.addNode({
      id,
      shape: type,
      label: label,
    });
    // if (autoLayout) {
    //   // 延迟执行布局
    //   this.layoutService.autoLayout(this.graph);
    // }

    return node;
  }
}
