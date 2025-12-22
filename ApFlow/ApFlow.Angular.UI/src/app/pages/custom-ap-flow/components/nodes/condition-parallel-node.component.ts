import { Component, Input, OnInit } from '@angular/core';
import { Node } from '@antv/x6';

@Component({
    selector: 'app-condition-parallel-node',
    template: `
    <div class="condition-parallel-node" [class.selected]="isSelected">
      <div class="node-header">
        <i nz-icon nzType="apartment" nzTheme="outline"></i>
        <span class="node-title">条件并行</span>
      </div>
      <div class="node-content">
        <div class="node-info" *ngIf="nodeData">
          <div class="info-item">
            <span class="info-label">条件:</span>
            <span class="info-value">{{ nodeData.condition || '未设置' }}</span>
          </div>
          <div class="info-item">
            <span class="info-label">并行数:</span>
            <span class="info-value">{{ nodeData.parallelCount || 2 }}个</span>
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
    styleUrls: ['./condition-parallel-node.component.css'],
    standalone:false
})
export class ConditionParallelNodeComponent implements OnInit {
    @Input() node: Node | null = null;
    @Input() isSelected: boolean = false;

    nodeData: any = null;

    ngOnInit(): void {
        if (this.node) {
            this.nodeData = this.node.getData() || {};
        }
    }
}