import { Component, Input, OnInit } from '@angular/core';
import { Node } from '@antv/x6';

@Component({
    selector: 'app-dynamic-condition-node',
    template: `
    <div class="dynamic-condition-node" [class.selected]="isSelected">
      <div class="node-header">
        <i nz-icon nzType="code" nzTheme="outline"></i>
        <span class="node-title">动态条件</span>
      </div>
      <div class="node-content">
        <div class="node-info" *ngIf="nodeData">
          <div class="info-item">
            <span class="info-label">脚本:</span>
            <span class="info-value">{{ nodeData.script ? '已设置' : '未设置' }}</span>
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
    styleUrls: ['./dynamic-condition-node.component.css'],
    standalone:false
})
export class DynamicConditionNodeComponent implements OnInit {
    @Input() node: Node | null = null;
    @Input() isSelected: boolean = false;

    nodeData: any = null;

    ngOnInit(): void {
        if (this.node) {
            this.nodeData = this.node.getData() || {};
        }
    }
}