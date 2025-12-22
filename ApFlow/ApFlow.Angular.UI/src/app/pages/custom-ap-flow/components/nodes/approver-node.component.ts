import { Component, Input, OnInit } from '@angular/core';
import { Node } from '@antv/x6';

@Component({
    selector: 'app-approver-node',
    template: `
    <div class="approver-node" [class.selected]="isSelected">
      <div class="node-header">
        <i nz-icon nzType="user" nzTheme="outline"></i>
        <span class="node-title">审批人</span>
      </div>
      <div class="node-content">
        <div class="node-info" *ngIf="nodeData">
          <div class="info-item">
            <span class="info-label">审批人:</span>
            <span class="info-value">{{ nodeData.approver || '未设置' }}</span>
          </div>
          <div class="info-item">
            <span class="info-label">方式:</span>
            <span class="info-value">{{ getApprovalTypeText(nodeData.approvalType) }}</span>
          </div>
        </div>
      </div>
      <div class="node-ports">
        <div class="port port-input" data-port="input"></div>
        <div class="port port-output" data-port="output"></div>
      </div>
    </div>
  `,
    styleUrls: ['./approver-node.component.css'],
    standalone:false
})
export class ApproverNodeComponent implements OnInit {
    @Input() node: Node | null = null;
    @Input() isSelected: boolean = false;

    nodeData: any = null;

    ngOnInit(): void {
        if (this.node) {
            this.nodeData = this.node.getData() || {};
        }
    }

    /**
     * 获取审批类型文本
     */
    getApprovalTypeText(type: string): string {
        switch (type) {
            case 'single':
                return '单人审批';
            case 'multi':
                return '多人审批';
            case 'any':
                return '任意一人审批';
            case 'all':
                return '所有人审批';
            default:
                return '单人审批';
        }
    }
}