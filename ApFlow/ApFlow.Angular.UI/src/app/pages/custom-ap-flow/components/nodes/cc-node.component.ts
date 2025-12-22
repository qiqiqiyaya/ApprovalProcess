import { Component, Input, OnInit } from '@angular/core';
import { Node } from '@antv/x6';

@Component({
    selector: 'app-cc-node',
    template: `
    <div class="cc-node" [class.selected]="isSelected">
      <div class="node-header">
        <i nz-icon nzType="mail" nzTheme="outline"></i>
        <span class="node-title">抄送人</span>
      </div>
      <div class="node-content">
        <div class="node-info" *ngIf="nodeData">
          <div class="info-item">
            <span class="info-label">抄送人:</span>
            <span class="info-value">{{ nodeData.ccUsers?.length || 0 }}人</span>
          </div>
          <div class="info-item">
            <span class="info-label">方式:</span>
            <span class="info-value">{{ getCcTypeText(nodeData.ccType) }}</span>
          </div>
        </div>
      </div>
      <div class="node-ports">
        <div class="port port-input" data-port="input"></div>
        <div class="port port-output" data-port="output"></div>
      </div>
    </div>
  `,
    styleUrls: ['./cc-node.component.css'],
    standalone:false
})
export class CcNodeComponent implements OnInit {
    @Input() node: Node | null = null;
    @Input() isSelected: boolean = false;

    nodeData: any = null;

    ngOnInit(): void {
        if (this.node) {
            this.nodeData = this.node.getData() || {};
        }
    }

    /**
     * 获取抄送类型文本
     */
    getCcTypeText(type: string): string {
        switch (type) {
            case 'before':
                return '审批前';
            case 'after':
                return '审批后';
            case 'always':
                return '始终';
            default:
                return '审批后';
        }
    }
}