import { Component, inject, OnInit } from '@angular/core';
import { DelonFormModule, SFSchema, SFStringWidgetSchema, SFValueChange } from '@delon/form';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
  imports: [DelonFormModule]
})
export class FormComponent implements OnInit {

  schema: SFSchema;
  formJson = '{"properties":{"name":{"type":"string","title":"Name","ui":{"addOnAfter":"RMB","placeholder":"RMB结算"}},"mobile":{"type":"string","format":"date","title":"手机号"},"sfz":{"type":"string","format":"week","title":"身份证号"},"regex":{"type":"string","pattern":"^[abc]+$","title":"正则表达式","ui":{"placeholder":"^[abc]+$"}},"color":{"type":"string","format":"color","title":"颜色","ui":{"optionalHelp":{"text":"我是有背景颜色的喔","bgColor":"#f50"}}}},"required":["name"]}';

  ngOnInit(): void {
    const form = JSON.parse(this.formJson);
    this.schema = form;
  }

  private readonly msg = inject(NzMessageService);

  submit(value: {}): void {
    this.msg.success(JSON.stringify(value));
  }

  valueChange(res: SFValueChange): void {
    this.msg.info(JSON.stringify(res));
  }

}
