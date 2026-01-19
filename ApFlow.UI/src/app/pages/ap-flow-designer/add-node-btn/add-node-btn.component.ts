import { Component, inject, OnInit } from '@angular/core';
import { Node as XNode } from '@antv/x6';
import { X6FlowGraph } from '../services/x6-flow-graph';
import { NodeInfo, NodeType } from '../../models/node-description';
import { CustomShapeNames } from '../custom-shape-names';


// 节点类型定义
interface ApprovalNode {
  type: string;       // 节点类型标识
  name: string;       // 节点名称
  description: string; // 节点描述
  icon: string;       // 节点图标
  shape: string;      // 节点形状名称
}

@Component({
  selector: 'app-add-node-btn',
  templateUrl: './add-node-btn.component.html',
  styleUrls: ['./add-node-btn.component.css'],
  standalone: false
})
export class AddNodeBtnComponent implements OnInit {

  // 模态框显示状态
  isModalVisible = false;

  // 审批节点列表（从nodes目录获取的节点）
  approvalNodes: ApprovalNode[] = [];

  // 注入 X6FlowGraph 服务
  private readonly flowGraph = inject(X6FlowGraph);

  constructor() {

  }

  ngOnInit() {
    // 初始化审批节点列表
    this.initApprovalNodes();
  }

  /**
   * 初始化审批节点列表
   */
  initApprovalNodes(): void {
    // 从nodes目录获取所有可用的审批节点
    this.approvalNodes = [
      {
        type: 'parallel-approval',
        name: '并行审批',
        description: '多个审批人同时审批，全部通过后流程继续',
        icon: 'team',
        shape: CustomShapeNames.pApNode
      },
      {
        type: 'parallel-approval-merge',
        name: '合并审批',
        description: '合并多个并行审批流程分支',
        icon: 'merge-cells',
        shape: CustomShapeNames.pApMergeNode
      },
      {
        type: 'approve',
        name: '普通审批',
        description: '单个审批人审批',
        icon: 'check-circle',
        shape: CustomShapeNames.apNode
      }
    ];
  }

  /**
   * 处理添加节点按钮点击事件
   */
  onAddClick(): void {
    // 显示模态框
    this.isModalVisible = true;
  }

  /**
   * 处理节点点击事件，直接添加节点并关闭模态框
   */
  onNodeClick(node: ApprovalNode): void {
    // 获取当前操作节点
    const currentNode = this.flowGraph.currentNode;

    if (!currentNode) {
      console.error('当前操作节点不存在');
      this.isModalVisible = false;
      return;
    }

    console.log('当前节点 ID:', currentNode.id);
    console.log('当前节点位置:', currentNode.getPosition());

    // 判断是否是并行审批节点
    if (node.type === 'parallel-approval') {
      this.handleParallelApprovalNode(currentNode, node);
    } else {
      this.handleRegularApprovalNode(currentNode, node);
    }

    // 关闭模态框
    this.isModalVisible = false;
  }

  /**
   * 处理普通审批节点
   */
  private handleRegularApprovalNode(currentNode: XNode, node: ApprovalNode): void {
    // 创建普通审批节点
    const approvalNode = this.createApprovalNode(node, currentNode);

    if (!approvalNode) {
      console.error('创建审批节点失败');
      return;
    }

    console.log('审批节点 ID:', approvalNode.id);
    console.log('审批节点位置:', approvalNode.getPosition());

    // 创建新的 add 节点
    const newAddNode = this.createAddNode(approvalNode);

    if (!newAddNode) {
      console.error('创建 add 节点失败');
      return;
    }

    console.log('新 add 节点 ID:', newAddNode.id);
    console.log('新 add 节点位置:', newAddNode.getPosition());

    // 更新节点关系：当前 add → 审批节点 → 新 add → 原下游节点
    this.updateNodeRelationshipsWithAdd(currentNode, approvalNode, newAddNode);

    // 验证节点关系是否正确更新
    const currentInfo = currentNode.getData() as NodeInfo;
    const approvalInfo = approvalNode.getData() as NodeInfo;
    const addInfo = newAddNode.getData() as NodeInfo;

    console.log('=== 验证节点关系 ===');
    console.log('当前 add 节点 next 数量:', currentInfo.next?.length || 0);
    console.log('当前 add 节点 next IDs:', currentInfo.next?.map(n => n.id) || []);
    console.log('审批节点 prevs IDs:', approvalInfo.prevs?.map(n => n.id) || []);
    console.log('审批节点 next 数量:', approvalInfo.next?.length || 0);
    console.log('审批节点 next IDs:', approvalInfo.next?.map(n => n.id) || []);
    console.log('新 add 节点 prevs IDs:', addInfo.prevs?.map(n => n.id) || []);
    console.log('新 add 节点 next 数量:', addInfo.next?.length || 0);
    console.log('新 add 节点 next IDs:', addInfo.next?.map(n => n.id) || []);

    // 重建连接线
    this.flowGraph.establishFlowConnections(this.flowGraph.startNode);

    // 调整布局
    this.flowGraph.layoutFlowNodes(this.flowGraph.startNode);
  }

  /**
   * 处理并行审批节点
   * 创建结构：add → 并行审批节点 → add → 合并审批节点 → end
   *                              → add →
   */
  private handleParallelApprovalNode(currentNode: XNode, node: ApprovalNode): void {
    console.log('=== 开始处理并行审批节点 ===');

    // 创建并行审批节点
    const parallelNode = this.createApprovalNode(node, currentNode);

    if (!parallelNode) {
      console.error('创建并行审批节点失败');
      return;
    }

    console.log('并行审批节点 ID:', parallelNode.id);
    console.log('并行审批节点位置:', parallelNode.getPosition());

    // 获取并行审批节点的位置和尺寸
    const parallelPosition = parallelNode.getPosition();
    const parallelSize = parallelNode.getSize();

    // 创建第一个 add 节点（分支1，左侧）
    const addNode1 = this.createAddNodeWithOffset(parallelNode, -80);

    if (!addNode1) {
      console.error('创建第一个 add 节点失败');
      return;
    }

    console.log('第一个 add 节点 ID:', addNode1.id);

    // 创建第二个 add 节点（分支2，右侧）
    const addNode2 = this.createAddNodeWithOffset(parallelNode, 80);

    if (!addNode2) {
      console.error('创建第二个 add 节点失败');
      return;
    }

    console.log('第二个 add 节点 ID:', addNode2.id);

    // 创建合并审批节点（位于并行审批节点下方，居中）
    const mergeNode = this.createMergeNode(parallelNode);

    if (!mergeNode) {
      console.error('创建合并审批节点失败');
      return;
    }

    console.log('合并审批节点 ID:', mergeNode.id);
    console.log('合并审批节点位置:', mergeNode.getPosition());

    // 更新节点关系：当前 add → 并行审批节点 → add1/add2 → 合并审批节点 → 原下游节点
    this.updateParallelNodeRelationships(currentNode, parallelNode, addNode1, addNode2, mergeNode);

    // 验证节点关系是否正确更新
    const currentInfo = currentNode.getData() as NodeInfo;
    const parallelInfo = parallelNode.getData() as NodeInfo;
    const addInfo1 = addNode1.getData() as NodeInfo;
    const addInfo2 = addNode2.getData() as NodeInfo;
    const mergeInfo = mergeNode.getData() as NodeInfo;

    console.log('=== 验证并行审批节点关系 ===');
    console.log('当前 add 节点 next 数量:', currentInfo.next?.length || 0);
    console.log('当前 add 节点 next IDs:', currentInfo.next?.map(n => n.id) || []);
    console.log('并行审批节点 prevs IDs:', parallelInfo.prevs?.map(n => n.id) || []);
    console.log('并行审批节点 next 数量:', parallelInfo.next?.length || 0);
    console.log('并行审批节点 next IDs:', parallelInfo.next?.map(n => n.id) || []);
    console.log('第一个 add 节点 prevs IDs:', addInfo1.prevs?.map(n => n.id) || []);
    console.log('第一个 add 节点 next IDs:', addInfo1.next?.map(n => n.id) || []);
    console.log('第二个 add 节点 prevs IDs:', addInfo2.prevs?.map(n => n.id) || []);
    console.log('第二个 add 节点 next IDs:', addInfo2.next?.map(n => n.id) || []);
    console.log('合并审批节点 prevs 数量:', mergeInfo.prevs?.length || 0);
    console.log('合并审批节点 prevs IDs:', mergeInfo.prevs?.map(n => n.id) || []);
    console.log('合并审批节点 next 数量:', mergeInfo.next?.length || 0);
    console.log('合并审批节点 next IDs:', mergeInfo.next?.map(n => n.id) || []);

    // 重建连接线
    this.flowGraph.establishFlowConnections(this.flowGraph.startNode);

    // 调整布局
    this.flowGraph.layoutFlowNodes(this.flowGraph.startNode);

    console.log('=== 并行审批节点处理完成 ===');
  }

  /**
   * 创建审批节点
   * @param node 节点类型信息
   * @param prevNode 前置节点
   * @returns 新创建的节点
   */
  private createApprovalNode(node: ApprovalNode, prevNode: XNode): XNode | null {
    const prevPosition = prevNode.getPosition();
    const prevSize = prevNode.getSize();

    // 计算新节点位置（在前置节点下方）
    const newNodeY = prevPosition.y + prevSize.height + 60; // 60 是垂直间距

    const newNode = this.flowGraph.graph.addNode({
      shape: node.shape,
      x: prevPosition.x,
      y: newNodeY,
      width: 120,
      height: 60,
      label: node.name
    });

    // 设置节点数据
    const newNodeInfo: NodeInfo = {
      type: NodeType.Info,
      current: newNode,
      prevs: [prevNode],
      next: []
    };

    newNode.setData(newNodeInfo);

    return newNode;
  }

  /**
   * 更新节点关系
   * 将新节点插入到当前节点和它的下一个节点之间
   * @param currentNode 当前节点
   * @param newNode 新节点
   */
  private updateNodeRelationships(currentNode: XNode, newNode: XNode): void {
    const currentInfo = currentNode.getData() as NodeInfo;
    const newInfo = newNode.getData() as NodeInfo;

    console.log('=== 开始更新节点关系 ===');
    console.log('当前节点 ID:', currentNode.id);
    console.log('当前节点原来的 next 数量:', currentInfo.next?.length || 0);
    console.log('当前节点原来的 next IDs:', currentInfo.next?.map(n => n.id) || []);

    // 保存当前节点原来的 next 节点（如果有）
    const originalNextNodes = currentInfo.next || [];
    console.log('保存的原下游节点数量:', originalNextNodes.length);

    // 删除当前节点到原来下游节点的所有连接线
    if (originalNextNodes.length > 0) {
      console.log('删除当前节点到原下游节点的连接线');
      this.flowGraph.removeAllNextEdge(currentNode);
    }

    // 更新当前节点的 next 为新节点
    currentInfo.next = [newNode];
    currentNode.setData(currentInfo);
    console.log('更新后当前节点的 next 数量:', currentInfo.next.length);
    console.log('更新后当前节点的 next IDs:', currentInfo.next.map(n => n.id));

    // 更新新节点的 prevs 为当前节点
    newInfo.prevs = [currentNode];

    // 更新新节点的 next 为原来当前节点的 next
    newInfo.next = originalNextNodes;
    newNode.setData(newInfo);
    console.log('新节点的 prevs IDs:', newInfo.prevs?.map(n => n.id) || []);
    console.log('新节点的 next 数量:', newInfo.next?.length || 0);
    console.log('新节点的 next IDs:', newInfo.next?.map(n => n.id) || []);

    // 更新原来 next 节点的 prevs，将当前节点替换为新节点
    for (const nextNode of originalNextNodes) {
      const nextInfo = nextNode.getData() as NodeInfo;
      console.log('处理原下游节点:', nextNode.id);
      console.log('原下游节点当前的 prevs IDs:', nextInfo.prevs?.map(n => n.id) || []);

      if (nextInfo.prevs) {
        const index = nextInfo.prevs.indexOf(currentNode);
        if (index !== -1) {
          nextInfo.prevs[index] = newNode;
          nextNode.setData(nextInfo);
          console.log('更新后原下游节点的 prevs IDs:', nextInfo.prevs.map(n => n.id));
        }
      }
    }

    console.log('=== 节点关系更新完成 ===');
  }

  /**
   * 创建 add 节点
   * @param prevNode 前置节点
   * @returns 新创建的 add 节点
   */
  private createAddNode(prevNode: XNode): XNode | null {
    const prevPosition = prevNode.getPosition();
    const prevSize = prevNode.getSize();

    // 计算新节点位置（在前置节点下方）
    const newNodeY = prevPosition.y + prevSize.height + 60; // 60 是垂直间距

    const newNode = this.flowGraph.graph.addNode({
      shape: CustomShapeNames.addNodeBtn,
      x: prevPosition.x,
      y: newNodeY,
      width: 120,
      height: 40
    });

    // 设置节点数据
    const newNodeInfo: NodeInfo = {
      type: NodeType.OperationNode,
      current: newNode,
      prevs: [prevNode],
      next: []
    };

    newNode.setData(newNodeInfo);

    return newNode;
  }

  /**
   * 创建带有水平偏移的 add 节点
   * @param prevNode 前置节点
   * @param xOffset 水平偏移量（负数向左，正数向右）
   * @returns 新创建的 add 节点
   */
  private createAddNodeWithOffset(prevNode: XNode, xOffset: number): XNode | null {
    const prevPosition = prevNode.getPosition();
    const prevSize = prevNode.getSize();

    // 计算新节点位置（在前置节点下方，带有水平偏移）
    const newNodeY = prevPosition.y + prevSize.height + 60; // 60 是垂直间距
    const newNodeX = prevPosition.x + xOffset;

    const newNode = this.flowGraph.graph.addNode({
      shape: CustomShapeNames.addNodeBtn,
      x: newNodeX,
      y: newNodeY,
      width: 120,
      height: 40
    });

    // 设置节点数据
    const newNodeInfo: NodeInfo = {
      type: NodeType.OperationNode,
      current: newNode,
      prevs: [prevNode],
      next: []
    };

    newNode.setData(newNodeInfo);

    return newNode;
  }

  /**
   * 创建合并审批节点
   * @param prevNode 前置节点
   * @returns 新创建的合并审批节点
   */
  private createMergeNode(prevNode: XNode): XNode | null {
    const prevPosition = prevNode.getPosition();
    const prevSize = prevNode.getSize();

    // 计算新节点位置（在前置节点下方）
    const newNodeY = prevPosition.y + prevSize.height + 60; // 60 是垂直间距

    const newNode = this.flowGraph.graph.addNode({
      shape: CustomShapeNames.pApMergeNode,
      x: prevPosition.x,
      y: newNodeY,
      width: 120,
      height: 60,
      label: '合并审批'
    });

    // 设置节点数据
    const newNodeInfo: NodeInfo = {
      type: NodeType.Info,
      current: newNode,
      prevs: [],
      next: []
    };

    newNode.setData(newNodeInfo);

    return newNode;
  }

  /**
   * 更新三个节点的关系
   * 当前 add → 审批节点 → 新 add → 原下游节点
   * @param currentAdd 当前 add 节点
   * @param approvalNode 审批节点
   * @param newAddNode 新的 add 节点
   */
  private updateNodeRelationshipsWithAdd(currentAdd: XNode, approvalNode: XNode, newAddNode: XNode): void {
    const currentAddInfo = currentAdd.getData() as NodeInfo;
    const approvalInfo = approvalNode.getData() as NodeInfo;
    const newAddInfo = newAddNode.getData() as NodeInfo;

    console.log('=== 开始更新三个节点关系 ===');
    console.log('当前 add 节点 ID:', currentAdd.id);
    console.log('审批节点 ID:', approvalNode.id);
    console.log('新 add 节点 ID:', newAddNode.id);

    // 保存当前 add 节点原来的 next 节点
    const originalNextNodes = currentAddInfo.next || [];
    console.log('当前 add 节点原来的 next 数量:', originalNextNodes.length);
    console.log('当前 add 节点原来的 next IDs:', originalNextNodes.map(n => n.id));

    // 删除当前 add 节点到原来下游节点的所有连接线
    if (originalNextNodes.length > 0) {
      console.log('删除当前 add 节点到原下游节点的连接线');
      this.flowGraph.removeAllNextEdge(currentAdd);
    }

    // 更新当前 add 节点的 next 为审批节点
    currentAddInfo.next = [approvalNode];
    currentAdd.setData(currentAddInfo);
    console.log('更新后当前 add 节点的 next IDs:', currentAddInfo.next.map(n => n.id));

    // 更新审批节点的 prevs 为当前 add 节点
    approvalInfo.prevs = [currentAdd];

    // 更新审批节点的 next 为新 add 节点
    approvalInfo.next = [newAddNode];
    approvalNode.setData(approvalInfo);
    console.log('更新后审批节点的 prevs IDs:', approvalInfo.prevs.map(n => n.id));
    console.log('更新后审批节点的 next IDs:', approvalInfo.next.map(n => n.id));

    // 更新新 add 节点的 prevs 为审批节点
    newAddInfo.prevs = [approvalNode];

    // 更新新 add 节点的 next 为原下游节点
    newAddInfo.next = originalNextNodes;
    newAddNode.setData(newAddInfo);
    console.log('更新后新 add 节点的 prevs IDs:', newAddInfo.prevs.map(n => n.id));
    console.log('更新后新 add 节点的 next IDs:', newAddInfo.next.map(n => n.id));

    // 更新原下游节点的 prevs，将当前 add 节点替换为新 add 节点
    for (const nextNode of originalNextNodes) {
      const nextInfo = nextNode.getData() as NodeInfo;
      console.log('处理原下游节点:', nextNode.id);
      console.log('原下游节点当前的 prevs IDs:', nextInfo.prevs?.map(n => n.id) || []);

      if (nextInfo.prevs) {
        const index = nextInfo.prevs.indexOf(currentAdd);
        if (index !== -1) {
          nextInfo.prevs[index] = newAddNode;
          nextNode.setData(nextInfo);
          console.log('更新后原下游节点的 prevs IDs:', nextInfo.prevs.map(n => n.id));
        }
      }
    }

    console.log('=== 三个节点关系更新完成 ===');
  }

  /**
   * 更新并行审批节点的关系
   * 当前 add → 并行审批节点 → add1/add2 → 合并审批节点 → 原下游节点
   * @param currentAdd 当前 add 节点
   * @param parallelNode 并行审批节点
   * @param addNode1 第一个 add 节点
   * @param addNode2 第二个 add 节点
   * @param mergeNode 合并审批节点
   */
  private updateParallelNodeRelationships(
    currentAdd: XNode,
    parallelNode: XNode,
    addNode1: XNode,
    addNode2: XNode,
    mergeNode: XNode
  ): void {
    const currentAddInfo = currentAdd.getData() as NodeInfo;
    const parallelInfo = parallelNode.getData() as NodeInfo;
    const addInfo1 = addNode1.getData() as NodeInfo;
    const addInfo2 = addNode2.getData() as NodeInfo;
    const mergeInfo = mergeNode.getData() as NodeInfo;

    console.log('=== 开始更新并行审批节点关系 ===');
    console.log('当前 add 节点 ID:', currentAdd.id);
    console.log('并行审批节点 ID:', parallelNode.id);
    console.log('第一个 add 节点 ID:', addNode1.id);
    console.log('第二个 add 节点 ID:', addNode2.id);
    console.log('合并审批节点 ID:', mergeNode.id);

    // 保存当前 add 节点原来的 next 节点
    const originalNextNodes = currentAddInfo.next || [];
    console.log('当前 add 节点原来的 next 数量:', originalNextNodes.length);
    console.log('当前 add 节点原来的 next IDs:', originalNextNodes.map(n => n.id));

    // 删除当前 add 节点到原来下游节点的所有连接线
    if (originalNextNodes.length > 0) {
      console.log('删除当前 add 节点到原下游节点的连接线');
      this.flowGraph.removeAllNextEdge(currentAdd);
    }

    // 1. 更新当前 add 节点的 next 为并行审批节点
    currentAddInfo.next = [parallelNode];
    currentAdd.setData(currentAddInfo);
    console.log('更新后当前 add 节点的 next IDs:', currentAddInfo.next.map(n => n.id));

    // 2. 更新并行审批节点的 prevs 为当前 add 节点
    parallelInfo.prevs = [currentAdd];

    // 3. 更新并行审批节点的 next 为两个 add 节点
    parallelInfo.next = [addNode1, addNode2];
    parallelNode.setData(parallelInfo);
    console.log('更新后并行审批节点的 prevs IDs:', parallelInfo.prevs.map(n => n.id));
    console.log('更新后并行审批节点的 next IDs:', parallelInfo.next.map(n => n.id));

    // 4. 更新第一个 add 节点的 prevs 为并行审批节点
    addInfo1.prevs = [parallelNode];

    // 5. 更新第一个 add 节点的 next 为合并审批节点
    addInfo1.next = [mergeNode];
    addNode1.setData(addInfo1);
    console.log('更新后第一个 add 节点的 prevs IDs:', addInfo1.prevs.map(n => n.id));
    console.log('更新后第一个 add 节点的 next IDs:', addInfo1.next.map(n => n.id));

    // 6. 更新第二个 add 节点的 prevs 为并行审批节点
    addInfo2.prevs = [parallelNode];

    // 7. 更新第二个 add 节点的 next 为合并审批节点
    addInfo2.next = [mergeNode];
    addNode2.setData(addInfo2);
    console.log('更新后第二个 add 节点的 prevs IDs:', addInfo2.prevs.map(n => n.id));
    console.log('更新后第二个 add 节点的 next IDs:', addInfo2.next.map(n => n.id));

    // 8. 更新合并审批节点的 prevs 为两个 add 节点
    mergeInfo.prevs = [addNode1, addNode2];

    // 9. 更新合并审批节点的 next 为原下游节点
    mergeInfo.next = originalNextNodes;
    mergeNode.setData(mergeInfo);
    console.log('更新后合并审批节点的 prevs IDs:', mergeInfo.prevs.map(n => n.id));
    console.log('更新后合并审批节点的 next IDs:', mergeInfo.next.map(n => n.id));

    // 10. 更新原下游节点的 prevs，将当前 add 节点替换为合并审批节点
    for (const nextNode of originalNextNodes) {
      const nextInfo = nextNode.getData() as NodeInfo;
      console.log('处理原下游节点:', nextNode.id);
      console.log('原下游节点当前的 prevs IDs:', nextInfo.prevs?.map(n => n.id) || []);

      if (nextInfo.prevs) {
        const index = nextInfo.prevs.indexOf(currentAdd);
        if (index !== -1) {
          nextInfo.prevs[index] = mergeNode;
          nextNode.setData(nextInfo);
          console.log('更新后原下游节点的 prevs IDs:', nextInfo.prevs.map(n => n.id));
        }
      }
    }

    console.log('=== 并行审批节点关系更新完成 ===');
  }

  /**
   * 处理模态框关闭事件
   */
  handleModalCancel(): void {
    this.isModalVisible = false;
  }

  /**
   * ngFor trackBy 函数，提升列表渲染性能
   */
  trackByNodeType(index: number, node: ApprovalNode): string {
    return node.type;
  }

}
