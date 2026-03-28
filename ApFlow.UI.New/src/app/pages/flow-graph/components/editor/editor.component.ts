import { Component, ElementRef, Injector, OnInit, viewChild, OnDestroy } from '@angular/core';
import { Graph, Snapline } from '@antv/x6';
import { NodeRegister } from '../nodes/node-register';
import { GraphManagerService } from '../../services/graph-manager.service';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css'],
  standalone: false
})
export class EditorComponent implements OnInit, OnDestroy {

  private graph: Graph;
  private contianer = viewChild<ElementRef>('graphContainer');

  constructor(
    injector: Injector,
    private graphManager: GraphManagerService,
  ) {
    NodeRegister.register(injector);
  }

  ngOnInit() {
    this.graph = new Graph({
      container: this.contianer()?.nativeElement,
      grid: true,
      panning: true,
      mousewheel: true,
      // 添加背景层，用于显示标记点
      background: {
        color: '#f5f5f5',
      },
    });


    // 添加标记点（在所有节点渲染后）
    this.graph.on('render:done', () => {
      const nodes = this.graph.getNodes();

      nodes.forEach(node => {
        const position = node.position();
        const bbox = node.getBBox();

        // 在计算的中心点画一个红点
        const centerX = position.x;
        const centerY = position.y;

        console.log(`${node.id}: 计算中心 (${centerX.toFixed(2)}, ${centerY.toFixed(2)})`);

        // 方法一：通过 DOM 添加标记点
        //     const container = this.graph.container;
        //     const marker = document.createElement('div');
        //     marker.style.cssText = `
        //   position: absolute;
        //   left: ${centerX}px;
        //   top: ${centerY}px;
        //   width: 6px;
        //   height: 6px;
        //   background: red;
        //   border-radius: 50%;
        //   transform: translate(-50%, -50%);
        //   z-index: 9999;
        //   pointer-events: none;
        // `;
        //     container.appendChild(marker);

        // 方法二：使用 X6 的标记节点
        this.graph.addNode({
          id: `marker-${node.id}`,
          x: centerX,
          y: centerY,
          width: 10,
          height: 10,
          shape: 'circle',
          attrs: {
            body: {
              fill: 'red',
            },
          },
        });
      });
    });

    this.graph.use(new Snapline({ enabled: true }));
    this.graphManager.setGraph(this.graph);
    this.graphManager.setflowGraph(this.graphManager.newFlow());
    this.graphManager.render();


  }

  ngOnDestroy(): void {

  }
}
