import { register } from "@antv/x6-angular-shape";
import { NodeOperationComponent } from "./node-operation/node-operation.component";
import { Injector } from "@angular/core";
import { ApproveNodeComponent } from "./approve-node/approve-node.component";
import { ParallelApprovalNodeComponent } from "./parallel-approval-node/parallel-approval-node.component";
import { ParallelApprovalMergeNodeComponent } from "./parallel-approval-Merge-node/parallel-approval-Merge-node.component";

export class X6NodeRegister {
    static register(injector: Injector) {
        register({
            shape: 'operation-node',
            width: 120,
            height: 20,
            content: NodeOperationComponent,
            injector: injector,
        });

        register({
            shape: 'approval-node',
            width: 120,
            height: 40,
            content: ApproveNodeComponent,
            injector: injector,
        })

        register({
            shape: 'parallel-approval-node',
            width: 120,
            height: 40,
            content: ParallelApprovalNodeComponent,
            injector: injector,
        });

        register({
            shape: 'parallel-approval-Merge-node',
            width: 120,
            height: 40,
            content: ParallelApprovalMergeNodeComponent,
            injector: injector,
        })
    }
}
