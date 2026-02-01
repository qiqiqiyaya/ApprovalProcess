import { Injectable } from '@angular/core';
export interface FlowNode {
  id: string;
  type: 'start' | 'end' | 'approver' | 'addIcon';
  next: string | null;  // 指向下一个节点的ID
  prev: string | null;  // 指向前一个节点的ID
}

@Injectable({
  providedIn: 'root'
})
export class ApprovalNodeService {

  constructor() { }

  private nodes: Map<string, FlowNode> = new Map();
  private head: string | null = null;
  private tail: string | null = null;

  addNodeAfter(nodeId: string, newNode: FlowNode) {
    const currentNode = this.nodes.get(nodeId);
    if (!currentNode) return;

    const nextNodeId = currentNode.next;

    // 更新链表关系
    currentNode.next = newNode.id;
    newNode.prev = nodeId;
    newNode.next = nextNodeId;

    if (nextNodeId) {
      const nextNode = this.nodes.get(nextNodeId);
      if (nextNode) nextNode.prev = newNode.id;
    }

    this.nodes.set(newNode.id, newNode);

    // 如果是添加到末尾，更新tail
    if (nodeId === this.tail) {
      this.tail = newNode.id;
    }
  }
}
