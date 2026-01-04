import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DynamicFormComponent } from './dynamic-form.component';
import { FormFieldConfigurationComponent } from './form-field-configuration/form-field-configuration.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    DynamicFormComponent,
    FormFieldConfigurationComponent
  ]
})
export class DynamicFormModule { }
