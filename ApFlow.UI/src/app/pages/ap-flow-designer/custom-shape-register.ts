import { Injector } from "@angular/core";
import { register } from "@antv/x6-angular-shape";
import { AddNodeBtnComponent } from "./add-node-btn/add-node-btn.component";
import { CustomShapeNames } from "./custom-shape-names";
import { ParallelApprovalMergeNodeComponent } from "./nodes/parallel-approval-merge-node/parallel-approval-Merge-node.component";
import { ParallelApprovalNodeComponent } from "./nodes/parallel-approval-node/parallel-approval-node.component";
import { ApproveNodeComponent } from "./nodes/approve-node/approve-node.component";

/**
 * 自定义图形注册器
 */
export class CustomShapeRegister {
    /**
     * 注册自定图形
     * @param injector 注入器
     */
    static register(injector: Injector) {
        // 注册添加节点按钮
        register({
            shape: CustomShapeNames.addNodeBtn,
            width: 120,
            height: 20,
            content: AddNodeBtnComponent,
            injector: injector,
        });

        // 注册审批节点
        register({
            shape: CustomShapeNames.apNode,
            width: 120,
            height: 40,
            content: ApproveNodeComponent,
            injector: injector,
        });

        // 注册并行审批节点
        register({
            shape: CustomShapeNames.pApNode,
            width: 120,
            height: 40,
            content: ParallelApprovalNodeComponent,
            injector: injector,
        });

        // 注册并行审批合并节点
        register({
            shape: CustomShapeNames.pApMergeNode,
            width: 120,
            height: 40,
            content: ParallelApprovalMergeNodeComponent,
            injector: injector,
        });
    }
}
