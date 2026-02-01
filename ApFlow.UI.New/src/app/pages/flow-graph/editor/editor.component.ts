import { Component, OnInit } from '@angular/core';
import { CanvasComponent } from "../canvas/canvas.component";
import { Graph } from '@antv/x6';
import { DagreLayout } from '@antv/layout';
import { NodeShape } from '../nodes/node-register';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css'],
  imports: [CanvasComponent]
})
export class EditorComponent implements OnInit {

  constructor() { }

  ngOnInit() {

  }

  graphGenerated(graph: Graph) {
    const graphData = {
      nodes: [
        { id: 'start', label: '开始', width: 80, height: 40  },
        { id: "operation", label: 'operation', width: 80, height: 40,shape:NodeShape.operation },
        { id: 'process', label: '处理数据', width: 80, height: 40 },
        { id: 'decision', label: '判断？', width: 80, height: 40 },
        { id: 'end', label: '结束', width: 80, height: 40 },

        { id: 'ces1', label: 'ces1', width: 80, height: 40 },
        { id: 'ces2', label: 'ces2', width: 80, height: 40 },
      ],
      edges: [
        {
          source: 'start', target: 'operation',
          // 设置边的样式
          attrs: {
            line: {
              // 将 .running-line class 添加到边的 <path> 元素上
              class: 'running-line',
              stroke: '#5f95ff', // 边的颜色
              strokeWidth: 2,
              // 设置目标箭头
              targetMarker: 'classic',
            },
          },
          // 使用正交路由，使路径更规整
          router: { name: 'orth', },
        },
        { source: 'operation', target: 'process', router: { name: 'orth', } },
        { source: 'process', target: 'decision', router: { name: 'orth', } },
        { source: 'decision', target: 'end', label: '是', router: { name: 'orth', } },

        { source: 'process', target: 'ces1', router: { name: 'orth', } },
        { source: 'process', target: 'ces2', router: { name: 'orth', } },
      ]
    };

    // 1. 使用 DagreLayout 计算节点位置
    const dagreLayout = new DagreLayout({
      type: 'dagre',
      rankdir: 'TB',
      align: 'DL',
      ranksep: 15,
      nodesep: 25,
    });

    const layoutedData = dagreLayout.layout(graphData);

    // 3. 使用计算好的数据渲染图
    graph.fromJSON(layoutedData);
    graph.centerContent();

  }
}
