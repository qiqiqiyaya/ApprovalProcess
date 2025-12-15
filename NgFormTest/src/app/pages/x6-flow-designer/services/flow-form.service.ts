import { Injectable } from '@angular/core';
import { SFSchema } from '@delon/form';
import { BehaviorSubject, map, Observable, of } from 'rxjs';

@Injectable()
export class FlowFormService {

  private _formSchema = new BehaviorSubject<SFSchema>({});
  private _properties = new BehaviorSubject<PropertyName[]>([]);
  private _formJson: string = '{"properties":{"name":{"type":"string","title":"Name","ui":{"addOnAfter":"RMB","placeholder":"RMB结算"}},"mobile":{"type":"string","format":"date","title":"手机号"},"sfz":{"type":"string","format":"week","title":"身份证号"},"regex":{"type":"string","pattern":"^[abc]+$","title":"正则表达式","ui":{"placeholder":"^[abc]+$"}},"color":{"type":"string","format":"color","title":"颜色","ui":{"optionalHelp":{"text":"我是有背景颜色的喔","bgColor":"#f50"}}}},"required":["name"]}';

  constructor() {
    /* 测试代码 */
    this.loadFormData().subscribe(schema => {
      this._formSchema.next(schema);
    });
  }

  get formSchema(): Observable<SFSchema> { return this._formSchema; }
  /* 属性 */
  get properties(): Observable<PropertyName[]> { return this._properties; }

  loadFormData(): Observable<SFSchema> {
    const schema = JSON.parse(this._formJson);
    this._formSchema.next(schema);
    this.setProperties();
    return of(schema);
  }

  setProperty(set: (original: PropertyName[]) => PropertyName[]) {

  }

  private setProperties() {
    if (!this._formSchema.value.properties) {
      this._properties.next([]);
    }

    const schema = this._formSchema.value.properties;

    const names: PropertyName[] = [];
    for (const key in schema) {
      const obj = schema[key];
      if (!obj) continue;
      if (!obj.title) continue;

      names.push(new PropertyName(key, obj.title));
    }

    this._properties.next(names);
  }
}

export class PropertyName {
  id: string;
  name: string;
  readonly: boolean = true;
  editable: boolean = false;
  hidden: boolean = false;

  [key: string]: any;

  constructor(id: string, name: string) {
    this.id = id;
    this.name = name;
  }
}
