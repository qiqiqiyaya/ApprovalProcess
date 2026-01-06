import { AfterViewInit, ChangeDetectionStrategy, Component, ElementRef, inject, Injector, OnInit, ViewChild } from '@angular/core';
import { Graph } from '@antv/x6';
import { GraphConstant } from '../../models/graph-constant';
import { NodeInfo, NodeType } from '../../models/node-description';
import { X6FlowGraph } from '../services/x6-flow-graph';
import { CustomShapeNames } from '../custom-shape-names';
import { CustomShapeRegister } from '../custom-shape-register';

@Component({
  selector: 'app-flow-graph-editor',
  templateUrl: './flow-graph-editor.component.html',
  styleUrls: ['./flow-graph-editor.component.css'],
  standalone: false,
  // 使用OnPush策略优化性能
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FlowGraphEditorComponent implements OnInit, AfterViewInit {
  // 获取容器元素引用
  @ViewChild('container', { static: false }) containerRef!: ElementRef<HTMLDivElement>;

  // 注入服务
  flowGraph = inject(X6FlowGraph);

  constructor(injector: Injector) {
    // 注册自定义形状
    CustomShapeRegister.register(injector);
  }

  ngOnInit() {
    // 初始化逻辑可在此扩展
  }

  ngAfterViewInit() {
    // 确保容器存在
    if (!this.containerRef?.nativeElement) {
      console.error('Flow graph container not found');
      return;
    }

    // 初始化流程图
    this.initGraph();
  }

  /**
   * 初始化流程图实例和节点
   */
  private initGraph(): void {
    // 创建X6 Graph实例
    const graph = new Graph({
      container: this.containerRef.nativeElement,
      grid: true,
      panning: true,
      mousewheel: true,
      // 添加更多默认配置以提高可维护性
      connecting: {
        snap: true,
        allowBlank: false,
        allowLoop: false,
        allowMulti: true,
        connector: {
          name: 'rounded',
          args: {
            radius: 8,
          },
        },
      },
    });

    // 初始化基础流程节点
    this.initBaseNodes(graph);
  }

  /**
   * 初始化基础流程节点（开始、操作、结束）
   */
  private initBaseNodes(graph: Graph): void {
    // 创建开始节点
    const startNode = graph.addNode({
      id: 'start',
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: '发起人'
    });

    const startInfo: NodeInfo = {
      type: NodeType.Start,
      current: startNode,
      next: []
    };

    // 创建操作节点
    const operationNode = graph.addNode({
      id: 'operation',
      shape: CustomShapeNames.addNodeBtn,
      width: GraphConstant.nodeWidth,
      height: 40
    });

    const operationNodeInfo: NodeInfo = {
      type: NodeType.OperationNode,
      current: operationNode,
      prevs: [startNode],
      next: []
    };

    // 创建结束节点
    const endNode = graph.addNode({
      id: 'end',
      width: GraphConstant.nodeWidth,
      height: GraphConstant.nodeHeight,
      label: '结束'
    });

    const endNodeInfo: NodeInfo = {
      type: NodeType.End,
      current: endNode,
      prevs: [operationNode]
    };

    // 设置节点间连接关系
    startInfo.next?.push(operationNode);
    operationNodeInfo.next?.push(endNode);

    // 保存节点信息
    startNode.setData(startInfo);
    operationNode.setData(operationNodeInfo);
    endNode.setData(endNodeInfo);

    // 初始化流程图服务
    this.flowGraph.init(graph, startNode, endNode);

    // 自动连接和布局
    this.flowGraph.autoConnect(startNode);
    this.flowGraph.rePositionForNext(startNode);
  }
}
