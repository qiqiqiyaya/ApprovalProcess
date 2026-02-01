import { Component, ElementRef, ViewChild } from '@angular/core';
import { DagreLayout } from '@antv/layout';
import { Graph } from '@antv/x6';

@Component({
  selector: 'app-welcome',
  imports: [],
  templateUrl: './welcome.html',
  styleUrl: './welcome.css'
})
export class Welcome {

  graph: Graph;
  @ViewChild('graphContainer', { static: true }) container: ElementRef;
  ngAfterViewInit(): void {
    const graphData = {
      nodes: [
        { id: 'start', shape: 'circle', x: 50, y: 100, width: 60, height: 60 },
        { id: 'add_1', shape: 'circle', x: 200, y: 100, width: 30, height: 30, label: '+' },
        { id: 'end', shape: 'circle', x: 350, y: 100, width: 60, height: 60 }
      ],
      edges: [
        { source: 'start', target: 'add_1' },
        { source: 'add_1', target: 'end' }
      ]
    };

    // 1. 使用 DagreLayout 计算节点位置
    const dagreLayout = new DagreLayout({
      type: 'dagre',
      rankdir: 'TB',
      align: 'DL',
      ranksep: 30,
      nodesep: 25,
    });

    const layoutedData = dagreLayout.layout(graphData);

    // 2. 创建 X6 图实例
    this.graph = new Graph({
      container: this.container?.nativeElement,
      // 开启网格背景
      grid: true,
      // 允许平移和缩放
      panning: true,
      mousewheel: true,
    });

    // 3. 使用计算好的数据渲染图
    this.graph.fromJSON(layoutedData);
    this.graph.centerContent();

    // const jsonData = this.graph.toJSON();
    // console.log(JSON.stringify(jsonData));
  }
}
