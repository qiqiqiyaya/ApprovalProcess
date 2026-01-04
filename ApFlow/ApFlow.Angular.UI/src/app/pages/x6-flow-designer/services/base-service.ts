import { X6FlowGraph } from "../../ap-flow-designer/services/x6-flow-graph";

export abstract class BaseService {
  protected _flowGraph: X6FlowGraph;
  protected get graph() {
    return this._flowGraph.graph;
  }

  /* 当前操作的节点 */
  protected get currentNode() { return this._flowGraph.currentNode; }
  /**
   * 当前操作节点的信息
   */
  protected get currentNodeInfo() {
    return this.currentNode.getNodeInfo();
  }

  constructor(flowGraph: X6FlowGraph) {
    this._flowGraph = flowGraph;
  }
}
