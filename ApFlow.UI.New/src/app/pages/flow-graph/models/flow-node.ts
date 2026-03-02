import { NodeShape } from "../components/nodes/node-register";
import { FlowNodeHelper } from "../helper/flow-node-helper";


export class FlowNode {
    id: string = crypto.randomUUID();
    shape: string = 'rect';
    // 分支相关属性
    branchGroup?: string; // 分支组标识
    branchIndex?: number; // 在分支中的索引

    width?: number;
    height?: number;
    label?: string;

    data?: any;

    x?: number;
    y?: number;

    constructor()
    constructor(shape: string)
    constructor(shape: string, data: any)
    constructor(shape?: string, data?: any) {
        if (shape) this.shape = shape;
        if (data) this.data = data;
        FlowNodeHelper.setSize(this);
    }

}

/**
 * 矩形节点
 */
export class RectNode extends FlowNode {
    constructor(label: string) {
        super('rect');
        this.label = label;
    }
}