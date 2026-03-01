import { Injector } from "@angular/core";
import { register } from "@antv/x6-angular-shape";
import { AddNodeBtnComponent } from "./add-node-btn/add-node-btn.component";
import { ApproveComponent } from "./approve/approve.component";
import { ParallelApprovalComponent } from "./parallel-approval/parallel-approval.component";
import { ParallelApprovalMergeComponent } from "./parallel-approval-merge/parallel-approval-merge.component";

export const NodeShape = {
    operation: "operation",
    approve: "approve",
    parallelApproval: "parallel-approval",
    parallelApprovalMerge: "parallel-approval-merge"
}

/**
 * 自定义图形注册器
 */
export class NodeRegister {
    /**
     * 注册自定图形
     * @param injector 注入器
     */
    static register(injector: Injector) {
        // 注册添加节点按钮
        register({
            shape: NodeShape.operation,
            content: AddNodeBtnComponent,
            injector: injector,
        });

        // 注册审批节点
        register({
            shape: NodeShape.approve,
            content: ApproveComponent,
            injector: injector,
        });

        // 注册并行审批节点
        register({
            shape: NodeShape.parallelApproval,
            content: ParallelApprovalComponent,
            injector: injector,
        });

        // 注册并行审批合并节点
        register({
            shape: NodeShape.parallelApprovalMerge,
            content: ParallelApprovalMergeComponent,
            injector: injector,
        });
    }
}
