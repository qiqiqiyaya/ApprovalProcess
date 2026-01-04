import { Component, inject, OnInit } from '@angular/core';
import { FlowFormService, PropertyName } from '../../x6-flow-designer/services/flow-form.service';
import { SFSchema } from '@delon/form';

@Component({
  selector: 'app-form-field-configuration',
  templateUrl: './form-field-configuration.component.html',
  styleUrls: ['./form-field-configuration.component.css'],
  standalone: false
})
export class FormFieldConfigurationComponent implements OnInit {

  form = inject(FlowFormService);

  radioValue = "A";
  constructor() { }

  ngOnInit() {

  }

  submit(data: {}) {

  }

  modelChange(property: PropertyName, name: string) {
    property.editable = false;
    property.hidden = false;
    property.readonly = false;
    property[name] = true;
  }
}
