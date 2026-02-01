import { Injectable, signal } from '@angular/core';
import { Graph } from '@antv/x6';

@Injectable({
  providedIn: 'root'
})
export class CanvasService {
  private static _graph: Graph;
  static setGraph(newGraph: Graph) {
    CanvasService._graph = newGraph;
  }
  constructor() { }

  private innerGraph = signal<Graph>(CanvasService._graph)

  /**
   * 图
   */
  get graph() {
    return this.innerGraph();
  }

}
