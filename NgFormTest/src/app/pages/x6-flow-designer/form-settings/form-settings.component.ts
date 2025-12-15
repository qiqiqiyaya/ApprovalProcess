import { Component, inject, OnInit } from '@angular/core';
import { FlowFormService } from '../services/flow-form.service';
import { SFSchema } from '@delon/form';

@Component({
  selector: 'app-form-settings',
  templateUrl: './form-settings.component.html',
  styleUrls: ['./form-settings.component.css'],
  standalone: false
})
export class FormSettingsComponent implements OnInit {

  form = inject(FlowFormService);
  schema: SFSchema = {};

  constructor() { }

  ngOnInit() {
    this.form.loadFormData();
    this.form.formSchema.subscribe(res => {
      this.schema = res;
    });
  }

  submit(value: {}): void {

  }
}
