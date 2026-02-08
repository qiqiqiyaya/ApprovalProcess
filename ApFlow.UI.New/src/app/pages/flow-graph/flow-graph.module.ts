import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditorComponent } from './editor/editor.component';
import { EditorService } from './services/editor.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    EditorComponent,
  ],
  providers: [
    EditorService,
  ],
  exports: [
    EditorComponent,
  ]
})
export class FlowGraphModule { }
