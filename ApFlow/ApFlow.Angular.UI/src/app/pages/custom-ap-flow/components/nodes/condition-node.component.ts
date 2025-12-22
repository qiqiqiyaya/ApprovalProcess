import { Component, Input, OnInit } from '@angular/core';
import { Node } from '@antv/x6';

@Component({
    selector: 'app-condition-node',
    template: `
    <div class="condition-node" [class.selected]="isSelected">
      <div class="node-header">
        <i nz-icon nzType="branches" nzTheme="outline"></i>
        <span class="node-title">条件分支</span>
      </div>
      <div class="node-content">
        <div class="node-info" *ngIf="nodeData">
          <div class="info-item">
            <span class="info-label">条件:</span>
            <span class="info-value">{{ nodeData.condition || '未设置' }}</span>
          </div>
          <div class="info-item">
            <span class="info-label">分支:</span>
            <span class="info-value">{{ nodeData.branches?.length || 2 }}个</span>
          </div>
        </div>
      </div>
      <div class="node-ports">
        <div class="port port-input" data-port="input"></div>
        <div class="port port-output-left" data-port="output-left"></div>
        <div class="port port-output-right" data-port="output-right"></div>
      </div>
    </div>
  `,
    styleUrls: ['./condition-node.component.css'],
    standalone:false
})
export class ConditionNodeComponent implements OnInit {
    @Input() node: Node | null = null;
    @Input() isSelected: boolean = false;

    nodeData: any = null;

    ngOnInit(): void {
        if (this.node) {
            this.nodeData = this.node.getData() || {};
        }
    }
}