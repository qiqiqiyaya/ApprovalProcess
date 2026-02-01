import { Component, ElementRef, Injector, OnDestroy, OnInit, effect, output, viewChild } from '@angular/core';
import { Graph } from '@antv/x6';
import { Group } from '@antv/x6/lib/model/port';
import { CanvasService } from './canvas.service';
import { NodeRegister } from '../nodes/node-register';

@Component({
  selector: 'app-canvas',
  templateUrl: './canvas.component.html',
  styleUrls: ['./canvas.component.css']
})
export class CanvasComponent implements OnInit, OnDestroy {

  private contianer = viewChild<ElementRef>('graphContainer');
  private graph: Graph;
  graphGenerated = output<Graph>()

  constructor(injector: Injector) {
    NodeRegister.register(injector);
  }

  ngOnDestroy(): void {

  }

  ngOnInit() {


    this.graph = new Graph({
      container: this.contianer()?.nativeElement,
      grid: true,
      panning: true,
      mousewheel: true,
    });

    CanvasService.setGraph(this.graph);
    this.graphGenerated.emit(this.graph);
  }

}
