import { Component, inject, Injector, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { Graph, Node as XNode } from '@antv/x6';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { NodeOperationService } from './node-operation.service';
import { GraphConstant } from './graph-constant';
import { NodeInfo, NodeType } from './node-description';
import { X6FlowGraph } from './services/x6-flow-graph';
import { X6NodeRegister } from './x6-node-register';
import { AddNodeComponent } from '../ap-flow-node-components/add-node/add-node.component';
import './global-extension';
import { NzTabChangeEvent, NzTabComponent } from 'ng-zorro-antd/tabs';

@Component({
  selector: 'app-x6-flow-designer',
  templateUrl: './x6-flow-designer.component.html',
  styleUrls: ['./x6-flow-designer.component.css'],
  standalone: false,
})
export class X6FlowDesignerComponent implements OnInit {

  flowOperationModel: NzModalRef;
  @ViewChild('template') template: TemplateRef<{}>

  operation = inject(NodeOperationService);
  modal = inject(NzModalService);
  ref = inject(ViewContainerRef);

  startNode: XNode;
  endNode: XNode;
  /* 当前操作的节点 */
  currentOpNode: XNode;

  selectedIndex: number=0;
  tabs: boolean[] = [true, false, false];

  constructor(injector: Injector,
    private flowGraph: X6FlowGraph) {
    X6NodeRegister.register(injector);
  }

  ngOnInit() {
  }

  ngAfterViewInit() {

  }

  nodeCreate() {
    this.flowOperationModel = this.modal.create({
      nzTitle: "添加",
      nzContent: AddNodeComponent,
      nzFooter: null
    });
  }

  openDrawer() {
    this.operation.drawerShow = true;
    this.flowOperationModel.close();
  }

  closeDrawer() {
    this.operation.drawerShow = false;
  }

  tabSelectChange(tab: NzTabChangeEvent) {
    this.tabs[tab.index!] = true;
  }

  save(){
    
  }
}
