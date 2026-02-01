import { Component, inject, OnInit } from '@angular/core';
import { NgZorroAntdCommonModule } from '../../../../shared/ng-zorro-antd-common.module';

@Component({
  selector: 'app-add-node-btn',
  templateUrl: './add-node-btn.component.html',
  styleUrls: ['./add-node-btn.component.css'],
  imports:[NgZorroAntdCommonModule]
})
export class AddNodeBtnComponent implements OnInit {

  // 模态框显示状态
  isModalVisible = false;

  // 注入 X6FlowGraph 服务
  // private readonly flowGraph = inject(X6FlowGraph);

  constructor() {

  }

  ngOnInit() {

  }


  onCardClick(){
    
  }
}
