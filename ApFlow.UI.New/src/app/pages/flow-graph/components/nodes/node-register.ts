import { Injector } from "@angular/core";
import { register } from "@antv/x6-angular-shape";
import { AddNodeBtnComponent } from "./add-node-btn/add-node-btn.component";
import { StartComponent } from "./start/start.component";
import { EndComponent } from "./end/end.component";
import { ApproveComponent } from "./approve/approve.component";
import { ParallelApprovalComponent } from "./parallel-approval/parallel-approval.component";
import { ParallelApprovalMergeComponent } from "./parallel-approval-merge/parallel-approval-merge.component";
import { OrComponent } from "./or/or.component";
import { OrMergeComponent } from "./or-merge/or-merge.component";
import { JointReviewComponent } from "./joint-review/joint-review.component";

export const NodeShape = {
    start: "start",
    end: "end",
    operation: "operation",
    approve: "approve",
    parallelApproval: "parallel-approval",
    parallelApprovalMerge: "parallel-approval-merge",
    or: "or",
    orMerge: "or-merge",
    jointReview: "joint-review"
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
        // 注册开始节点
        register({
            shape: NodeShape.start,
            content: StartComponent,
            injector: injector,
        });

        // 注册结束节点
        register({
            shape: NodeShape.end,
            content: EndComponent,
            injector: injector,
        });

        // 注册添加节点按钮
        register({
            shape: NodeShape.operation,
            content: AddNodeBtnComponent,
            injector: injector,
        });

        // 注册开始节点
        register({
            shape: NodeShape.start,
            content: StartComponent,
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

        // 注册或签节点
        register({
            shape: NodeShape.or,
            content: OrComponent,
            injector: injector,
        });

        // 注册或签合并节点
        register({
            shape: NodeShape.orMerge,
            content: OrMergeComponent,
            injector: injector,
        });

        // 注册会审节点
        register({
            shape: NodeShape.jointReview,
            content: JointReviewComponent,
            injector: injector,
        });
    }
}
