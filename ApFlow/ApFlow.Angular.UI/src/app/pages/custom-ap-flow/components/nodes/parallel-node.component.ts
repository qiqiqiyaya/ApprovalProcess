import { Component, Input, OnInit } from '@angular/core';
import { Node } from '@antv/x6';

@Component({
    selector: 'app-parallel-node',
    template: `
    <div class="parallel-node" [class.selected]="isSelected">
      <div class="node-header">
        <i nz-icon nzType="fork" nzTheme="outline"></i>
        <span class="node-title">并行审批</span>
      </div>
      <div class="node-content">
        <div class="node-info" *ngIf="nodeData">
          <div class="info-item">
            <span class="info-label">审批人:</span>
            <span class="info-value">{{ nodeData.approvers?.length || 0 }}人</span>
          </div>
          <div class="info-item">
            <span class="info-label">方式:</span>
            <span class="info-value">{{ getParallelTypeText(nodeData.parallelType) }}</span>
          </div>
        </div>
      </div>
      <div class="node-ports">
        <div class="port port-input" data-port="input"></div>
        <div class="port port-output" data-port="output"></div>
      </div>
    </div>
  `,
    styleUrls: ['./parallel-node.component.css'],
    standalone:false
})
export class ParallelNodeComponent implements OnInit {
    @Input() node: Node | null = null;
    @Input() isSelected: boolean = false;

    nodeData: any = null;

    ngOnInit(): void {
        if (this.node) {
            this.nodeData = this.node.getData() || {};
        }
    }

    /**
     * 获取并行审批类型文本
     */
    getParallelTypeText(type: string): string {
        switch (type) {
            case 'any':
                return '任意一人';
            case 'all':
                return '所有人';
            case 'percentage':
                return `按比例(${this.nodeData?.percentage || 50}%)`;
            case 'count':
                return `按人数(${this.nodeData?.requiredCount || 1}人)`;
            default:
                return '任意一人';
        }
    }
}