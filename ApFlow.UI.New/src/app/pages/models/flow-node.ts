export class FlowNode {
    id: string;
    shape: string = 'rect';
    // 分支相关属性
    branchGroup?: string; // 分支组标识
    branchIndex?: number; // 在分支中的索引

    width?: number = 80;
    height?: number = 40;
    label?: string;

    constructor(id: string) {
        this.id = id;
        this.label = id;
    }
}
