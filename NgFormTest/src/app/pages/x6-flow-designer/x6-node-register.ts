import { register } from "@antv/x6-angular-shape";
import { NodeOperationComponent } from "./node-operation/node-operation.component";
import { ParallelApprovalBtnComponent } from "./parallel-approval-btn/parallel-approval-btn.component";
import { Injector } from "@angular/core";
import { ApproveNodeComponent } from "./approve-node/approve-node.component";

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
            shape: 'parallel-approval-node',
            width: 120,
            height: 40,
            content: ParallelApprovalBtnComponent,
            injector: injector,
        });

        register({
            shape: 'approval-node',
            width: 120,
            height: 40,
            content: ApproveNodeComponent,
            injector: injector,
        })
    }
}
