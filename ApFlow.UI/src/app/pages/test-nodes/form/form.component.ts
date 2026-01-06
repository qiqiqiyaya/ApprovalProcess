import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { DelonFormModule, SFComponent, SFSchema, SFStringWidgetSchema, SFValueChange } from '@delon/form';
import { NzMessageService } from 'ng-zorro-antd/message';
import { FlowFormService } from '../../ap-flow-designer/services/flow-form.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
  imports: [DelonFormModule]
})
export class FormComponent implements OnInit {

  @ViewChild('sf', { static: false }) sf: SFComponent;
  msg = inject(NzMessageService);

  ngOnInit(): void {
    // this.form.loadFormData();
  }

  submit(value: {}): void {
    debugger;
    // this.form.propertyNames();
  }

  valueChange(res: SFValueChange): void {
    this.msg.info(JSON.stringify(res));
  }
}
