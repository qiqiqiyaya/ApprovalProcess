import { Injectable } from '@angular/core';
import { Graph, Node, Edge } from '@antv/x6';
import { ParallelVerticalLayout } from '../utils/layout.utils';
import { FixedLayoutService } from './fixed-layout.service';

@Injectable({
  providedIn: 'root'
})
export class FlowLayoutService {
  private layout: FixedLayoutService;
  
  constructor() {
    this.layout = new FixedLayoutService();
  }

  /**
   * 执行自动布局
   */
  autoLayout(graph: Graph): void {
    this.layout.layout(graph);
  }

  /**
   * 对齐并行和合并节点
   */
  alignParallelAndMerge(graph: Graph): void {
    const nodes = graph.getNodes();
    const parallelNodes = nodes.filter(node => node.shape === 'parallel');
    
    parallelNodes.forEach(parallelNode => {
      const mergeNode = this.findCorrespondingMerge(graph, parallelNode);
      if (mergeNode) {
        // 对齐x坐标
        const parallelPos = parallelNode.position();
        mergeNode.position(parallelPos.x, mergeNode.position().y);
        
        // 调整连接边
        this.adjustEdgesBetween(graph, parallelNode, mergeNode);
      }
    });
  }

  /**
   * 垂直对齐选中的节点
   */
  alignNodesVertical(graph: Graph, nodeIds: string[]): void {
    if (nodeIds.length < 2) return;
    
    const nodes = nodeIds.map(id => graph.getCellById(id)).filter(Boolean) as Node[];
    const firstNode = nodes[0];
    const firstPos = firstNode.position();
    
    // 将所有节点对齐到第一个节点的x坐标
    nodes.forEach(node => {
      node.position(firstPos.x, node.position().y);
    });
    
    // 重新调整边
    this.layout.layout(graph);
  }

  /**
   * 查找对应的合并节点
   */
  private findCorrespondingMerge(graph: Graph, parallelNode: Node): Node | null {
    // 获取并行节点的所有出边
    const outgoingEdges = graph.getOutgoingEdges(parallelNode);
    
    // 收集所有下游节点
    const visited = new Set<string>();
    const stack: string[] = [];
    
    outgoingEdges.forEach(edge => {
      const targetId = edge.getTargetCellId();
      if (targetId) stack.push(targetId);
    });
    
    while (stack.length > 0) {
      const nodeId = stack.pop()!;
      if (visited.has(nodeId)) continue;
      visited.add(nodeId);
      
      const node = graph.getCellById(nodeId);
      if (!node || !node.isNode()) continue;
      
      // 如果是合并节点，返回
      if (node.shape === 'merge') {
        return node as Node;
      }
      
      // 否则继续搜索
      const edges = graph.getOutgoingEdges(node as Node);
      edges.forEach(edge => {
        const targetId = edge.getTargetCellId();
        if (targetId) stack.push(targetId);
      });
    }
    
    return null;
  }

  /**
   * 调整两个节点之间的边
   */
  private adjustEdgesBetween(graph: Graph, sourceNode: Node, targetNode: Node): void {
    const edges = graph.getEdges().filter(edge => {
      const source = edge.getSourceNode();
      const target = edge.getTargetNode();
      return source === sourceNode && target === targetNode;
    });
    
    edges.forEach(edge => {
      // 如果源和目标在同一垂直线上，使用直线
      const sourcePos = sourceNode.position();
      const targetPos = targetNode.position();
      
      if (Math.abs(sourcePos.x - targetPos.x) < 10) {
        edge.setVertices([]);
      } else {
        // 否则添加顶点使其垂直连接
        const vertices = [
          { x: sourcePos.x, y: (sourcePos.y + targetPos.y) / 2 },
          { x: targetPos.x, y: (sourcePos.y + targetPos.y) / 2 }
        ];
        edge.setVertices(vertices);
      }
    });
  }

  /**
   * 获取布局信息
   */
  getLayoutInfo(graph: Graph): any {
    return this.layout.getLayoutData(graph);
  }
}