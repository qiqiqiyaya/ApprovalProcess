import { Cell, Graph, Node as XNode } from '@antv/x6';
import { NodeInfo } from './node-description';
// Extend the Node class from @antv/x6 to include getNodeInfo
declare module '@antv/x6' {
    interface Node {
        getNodeInfo(): NodeInfo;
    }
}

XNode.prototype.getNodeInfo = function(): NodeInfo {
    return this.getData() as NodeInfo;
};
