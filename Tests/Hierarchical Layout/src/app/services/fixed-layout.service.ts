import { Injectable } from '@angular/core';
import { Graph, Node, Edge } from '@antv/x6';

export interface LayoutNode {
  id: string;
  x: number;
  y: number;
  depth: number;
  column: number;  // 新增：列索引
  parentId?: string;
  children: string[];
  shape?: string;
}

@Injectable()
export class FixedLayoutService {
  private config = {
    nodeWidth: 120,
    nodeHeight: 60,
    horizontalGap: 160,  // 分支水平间距
    verticalGap: 80,     // 垂直间距
    centerX: 400,        // 中心线x坐标
    startY: 50,
  };

  constructor() {
  }

  /**
   * 重新设计的布局算法
   */
  layout(graph: Graph): void {
    const nodes = graph.getNodes();
    const edges = graph.getEdges();
    
    if (nodes.length === 0) return;
    
    // 1. 构建节点树
    const nodeTree = this.buildNodeTree(nodes, edges);
    
    // 2. 计算布局
    const layoutNodes = this.calculateLayout(nodeTree);
    
    // 3. 应用布局
    this.applyLayout(graph, layoutNodes);
    
    // 4. 调整边
    // this.adjustEdges(graph);
  }

  /**
   * 构建节点树结构
   */
  private buildNodeTree(nodes: Node[], edges: Edge[]): Map<string, LayoutNode> {
    const nodeMap = new Map<string, LayoutNode>();
    const adjacencyList = new Map<string, string[]>();
    const reverseAdjacencyList = new Map<string, string[]>();
    
    // 初始化节点
    nodes.forEach(node => {
      nodeMap.set(node.id, {
        id: node.id,
        x: 0,
        y: 0,
        depth: 0,
        column: 0,
        children: [],
        shape: node.shape
      });
      adjacencyList.set(node.id, []);
      reverseAdjacencyList.set(node.id, []);
    });
    
    // 构建连接关系
    edges.forEach(edge => {
      const sourceId = edge.getSourceCellId();
      const targetId = edge.getTargetCellId();
      
      if (sourceId && targetId) {
        adjacencyList.get(sourceId)!.push(targetId);
        reverseAdjacencyList.get(targetId)!.push(sourceId);
      }
    });
    
    // 构建树结构
    nodeMap.forEach((node, nodeId) => {
      const parentIds = reverseAdjacencyList.get(nodeId) || [];
      node.parentId = parentIds.length > 0 ? parentIds[0] : undefined;
      node.children = adjacencyList.get(nodeId) || [];
    });
    
    return nodeMap;
  }

  /**
   * 计算布局位置
   */
  private calculateLayout(nodeMap: Map<string, LayoutNode>): Map<string, LayoutNode> {
    // 1. 找到所有起始节点
    const startNodes: string[] = [];
    nodeMap.forEach((node, nodeId) => {
      if (!node.parentId) {
        startNodes.push(nodeId);
      }
    });
    
    // 2. 计算深度和列
    startNodes.forEach(startNodeId => {
      this.calculateDepthAndColumn(nodeMap, startNodeId, 0, 0);
    });
    
    // 3. 特殊处理：识别并行和合并节点
    this.identifyParallelAndMergeNodes(nodeMap);
    
    // 4. 调整列分配（确保并行分支左右分布）
    this.adjustColumnsForParallelBranches(nodeMap);
    
    // 5. 计算最终坐标
    this.calculateCoordinates(nodeMap);
    
    return nodeMap;
  }

  /**
   * 计算深度和列（递归）
   */
  private calculateDepthAndColumn(
    nodeMap: Map<string, LayoutNode>, 
    nodeId: string, 
    depth: number, 
    column: number
  ): void {
    const node = nodeMap.get(nodeId);
    if (!node) return;
    
    node.depth = depth;
    node.column = column;
    
    // 根据节点类型决定子节点的列分配
    const children = node.children;
    if (children.length === 0) return;
    
    if (node.shape === 'parallel') {
      // 并行节点：子节点分配到左右两侧
      children.forEach((childId, index) => {
        // 左边分支：-1，右边分支：1
        const childColumn = index === 0 ? -1 : 1;
        this.calculateDepthAndColumn(nodeMap, childId, depth + 1, childColumn);
      });
    } else if (node.shape === 'merge') {
      // 合并节点：所有子节点继承同一列（中间）
      children.forEach(childId => {
        this.calculateDepthAndColumn(nodeMap, childId, depth + 1, 0);
      });
    } else {
      // 普通节点：子节点继承父节点列
      children.forEach(childId => {
        this.calculateDepthAndColumn(nodeMap, childId, depth + 1, column);
      });
    }
  }

  /**
   * 识别并行和合并节点
   */
  private identifyParallelAndMergeNodes(nodeMap: Map<string, LayoutNode>): void {
    nodeMap.forEach(node => {
      // 并行节点：有多个子节点
      if (node.children.length > 1) {
        // 标记为并行节点（如果还不是）
        if (node.shape !== 'parallel') {
          console.log(`标识为并行节点: ${node.id}`);
        }
      }
      
      // 合并节点：有多个父节点
      if (node.parentId) {
        const parentNode = nodeMap.get(node.parentId);
        // 如果有兄弟节点且父节点是并行节点，那么自己可能是分支节点
        if (parentNode && parentNode.children.length > 1) {
          // 分支节点保持自己的列设置
        }
      }
    });
  }

  /**
   * 调整列分配（关键修复）
   */
  private adjustColumnsForParallelBranches(nodeMap: Map<string, LayoutNode>): void {
    // 找到所有并行节点
    const parallelNodes: string[] = [];
    nodeMap.forEach((node, nodeId) => {
      if (node.shape === 'parallel' || node.children.length > 1) {
        parallelNodes.push(nodeId);
      }
    });
    
    parallelNodes.forEach(parallelNodeId => {
      const parallelNode = nodeMap.get(parallelNodeId);
      if (!parallelNode) return;
      
      // 为每个分支分配列偏移量
      const branchCount = parallelNode.children.length;
      
      parallelNode.children.forEach((childId, index) => {
        const childNode = nodeMap.get(childId);
        if (!childNode) return;
        
        // 计算列偏移：左分支负值，右分支正值
        let columnOffset = 0;
        if (branchCount === 2) {
          // 两个分支：左边-1，右边+1
          columnOffset = index === 0 ? -1 : 1;
        } else {
          // 多个分支：均匀分布
          columnOffset = index - Math.floor(branchCount / 2);
        }
        
        // 设置分支节点的列
        childNode.column = columnOffset;
        
        // 递归设置分支子节点的列
        this.setColumnForBranch(nodeMap, childId, columnOffset);
      });
    });
  }

  /**
   * 为分支设置列（递归）
   */
  private setColumnForBranch(
    nodeMap: Map<string, LayoutNode>, 
    nodeId: string, 
    baseColumn: number
  ): void {
    const node = nodeMap.get(nodeId);
    if (!node) return;
    
    node.column = baseColumn;
    
    // 递归设置子节点（除非遇到合并节点）
    if (node.shape !== 'merge') {
      node.children.forEach(childId => {
        this.setColumnForBranch(nodeMap, childId, baseColumn);
      });
    }
  }

  /**
   * 计算最终坐标
   */
  private calculateCoordinates(nodeMap: Map<string, LayoutNode>): void {
    // 首先找到最大深度
    let maxDepth = 0;
    nodeMap.forEach(node => {
      maxDepth = Math.max(maxDepth, node.depth);
    });
    
    // 按深度分组
    const nodesByDepth = new Map<number, LayoutNode[]>();
    nodeMap.forEach(node => {
      const depth = node.depth;
      if (!nodesByDepth.has(depth)) {
        nodesByDepth.set(depth, []);
      }
      nodesByDepth.get(depth)!.push(node);
    });
    
    // 计算每个深度的y坐标，以及节点在每层中的x坐标
    for (let depth = 0; depth <= maxDepth; depth++) {
      const nodesAtDepth = nodesByDepth.get(depth) || [];
      const y = this.config.startY + depth * this.config.verticalGap;
      
      // 按列排序
      nodesAtDepth.sort((a, b) => a.column - b.column);
      
      nodesAtDepth.forEach(node => {
        // 根据列计算x坐标
        let x: number;
        
        if (node.shape === 'parallel') {
          // 并行节点：居中
          x = this.config.centerX;
        } else if (node.shape === 'merge') {
          // 合并节点：与对应的并行节点对齐
          const parallelNodeId = this.findParallelNodeForMerge(nodeMap, node.id);
          if (parallelNodeId) {
            const parallelNode = nodeMap.get(parallelNodeId);
            x = parallelNode ? parallelNode.x : this.config.centerX;
          } else {
            x = this.config.centerX;
          }
        } else {
          // 普通节点和分支节点：根据列偏移计算
          const columnMultiplier = node.column;
          x = this.config.centerX + columnMultiplier * this.config.horizontalGap;
        }
        
        node.x = x;
        node.y = y;
      });
    }
  }

  /**
   * 查找合并节点对应的并行节点
   */
  private findParallelNodeForMerge(
    nodeMap: Map<string, LayoutNode>, 
    mergeNodeId: string
  ): string | null {
    const mergeNode = nodeMap.get(mergeNodeId);
    if (!mergeNode) return null;
    
    // 向上查找，找到深度小2的并行节点
    let currentNode: LayoutNode | undefined = mergeNode;
    
    // 最多向上查找10层
    for (let i = 0; i < 10; i++) {
      if (!currentNode.parentId) break;
      
      const parentNode = nodeMap.get(currentNode.parentId);
      if (!parentNode) break;
      
      // 检查是否是并行节点
      if (parentNode.shape === 'parallel' || parentNode.children.length > 1) {
        return parentNode.id;
      }
      
      currentNode = parentNode;
    }
    
    return null;
  }

  /**
   * 应用布局到图
   */
  private applyLayout(graph: Graph, layoutNodes: Map<string, LayoutNode>): void {
    layoutNodes.forEach(layoutNode => {
      const graphNode = graph.getCellById(layoutNode.id);
      if (graphNode && graphNode.isNode()) {
        // 使用动画移动
        graphNode.position(layoutNode.x, layoutNode.y);
      }
    });
  }

  /**
   * 调整边
   */
  private adjustEdges(graph: Graph): void {
    const edges = graph.getEdges();
    
    edges.forEach(edge => {
      const sourceNode = edge.getSourceNode();
      const targetNode = edge.getTargetNode();
      
      if (sourceNode && targetNode) {
        const sourcePos = sourceNode.position();
        const targetPos = targetNode.position();
        
        // 如果节点在同一垂直线上，使用直线
        const isVerticalAlign = Math.abs(sourcePos.x - targetPos.x) < 10;
        
        if (isVerticalAlign) {
          // 垂直连接，使用直线
          edge.setVertices([]);
        } else {
          // 水平偏移连接，使用直角线
          const midX = (sourcePos.x + targetPos.x) / 2;
          edge.setVertices([
            { x: midX, y: sourcePos.y + this.config.nodeHeight / 2 },
            { x: midX, y: targetPos.y - this.config.nodeHeight / 2 }
          ]);
        }
      }
    });
  }

  /**
   * 获取布局数据（用于调试）
   */
  getLayoutData(graph: Graph): any {
    const nodes = graph.getNodes();
    const edges = graph.getEdges();
    const nodeMap = this.buildNodeTree(nodes, edges);
    const layoutNodes = this.calculateLayout(nodeMap);
    
    const result: any = {};
    layoutNodes.forEach((node, id) => {
      result[id] = {
        x: node.x,
        y: node.y,
        depth: node.depth,
        column: node.column,
        shape: node.shape,
        children: node.children,
        parentId: node.parentId
      };
    });
    
    return result;
  }
}