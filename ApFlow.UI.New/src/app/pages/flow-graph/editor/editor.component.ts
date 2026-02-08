import { Component, ElementRef, Injector, OnInit, viewChild } from '@angular/core';
import { Graph } from '@antv/x6';
import { DagreLayout } from '@antv/layout';
import { NodeRegister, NodeShape } from '../nodes/node-register';
import { FlowGraph } from '../../models/flow-graph';
import { BranchGroupManager } from '../../services/branch-group-manager';
import { BranchGroupRenderer } from '../../services/branch-group-renderer';
import { EditorService } from '../services/editor.service';

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
    this.editorService.setGraph(this.graph);
    this.graphGenerated(this.graph);
    this.editorService.setFlowGraph(FlowGraph.testGrap);
  }

  graphGenerated(graph: Graph) {



    // 创建管理器实例
    const branchManager = new BranchGroupManager();
    // 1. 创建分支组
    const result = branchManager.createBranchGroup('start-node-1', { description: '测试分支组' });

    // 2. 添加节点到分支
    branchManager.addNodeToBranch(result.group!.id, 0, 'task-node-1');
    branchManager.addNodeToBranch(result.group!.id, 0, 'task-node-2');
    branchManager.addNodeToBranch(result.group!.id, 1, 'task-node-3');

    // 3. 设置合并节点
    debugger;
    branchManager.setMergeNode(result.group!.id, 'merge-node-1');

    var groups = branchManager.getAllGroups();
    console.log('所有分支组:', groups);

    groups.forEach(res => BranchGroupRenderer.branchGroupToNodes(res));
    groups.forEach(res => BranchGroupRenderer.branchGroupToEdges(res));

    // 3. 监听节点点击事件
    graph.on('node:click', (e) => {
      debugger;

      const s = e.node;
    });

    // 4. 查询统计信息
    // const stats = branchManager.getBranchStats(result.group!.id);
    // console.log('分支统计:', stats);

    // 1. 使用 DagreLayout 计算节点位置
    const dagreLayout = new DagreLayout({
      type: 'dagre',
      ranksep: 15,
      nodesep: 35,
    });
    const layoutedData = dagreLayout.layout(FlowGraph.testGrap);
    // 3. 使用计算好的数据渲染图
    graph.fromJSON(layoutedData);
    graph.centerContent();
  }
}
