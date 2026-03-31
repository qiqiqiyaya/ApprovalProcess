import { Component, ElementRef, Injector, OnInit, viewChild, OnDestroy, input, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Graph, Snapline } from '@antv/x6';
import { NodeRegister } from '../nodes/node-register';
import { GraphManagerService } from '../../services/graph-manager.service';
import { IFlowGraph } from '../../models/graph-definition';
import { filter, Subject } from 'rxjs';

@Component({
  selector: 'flow-graph-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css'],
  standalone: false
})
export class EditorComponent implements OnInit, OnDestroy, OnChanges {

  private graph: Graph;
  private contianer = viewChild<ElementRef>('graphContainer');
  @Input() flowGraph: IFlowGraph;

  private isInit = false;
  private $graphInit = new Subject<IFlowGraph>();

  constructor(
    injector: Injector,
    private graphManager: GraphManagerService) {
    NodeRegister.register(injector);

    this.$graphInit.pipe(filter(graph => this.isInit)).subscribe(graph => {
      this.graphManager.setflowGraph(graph);
      this.graphManager.render();
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['flowGraph']) {
      this.$graphInit.next(this.flowGraph);
    }
  }

  ngOnInit() {
    this.isInit = false;
    this.graph = new Graph({
      container: this.contianer()?.nativeElement,
      grid: true,
      panning: true,
      mousewheel: true
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
    this.isInit = true;
    if (this.flowGraph) this.$graphInit.next(this.flowGraph);
  }

  ngOnDestroy(): void {
    this.$graphInit.complete();
    this.$graphInit.unsubscribe();

  }
}
