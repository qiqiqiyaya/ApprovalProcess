import { Component, ElementRef, Injector, OnInit, viewChild, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Graph, Snapline } from '@antv/x6';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NodeRegister } from '../nodes/node-register';
import { FlowGraph } from '../../models/flow-graph';
import { EditorService } from '../../services/editor.service';
import { LayoutService } from '../../services/layout/layout.service';
import { LayoutConfigService } from '../../services/layout/layout-config.service';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.css'],
  standalone: false
})
export class EditorComponent implements OnInit, OnDestroy {

  private graph: Graph;
  private contianer = viewChild<ElementRef>('graphContainer');

  /**
   * Subscription to configuration changes
   * T076: Subscribe to config changes for layout updates
   */
  private configSubscription: Subscription = Subscription.EMPTY;

  constructor(
    injector: Injector,
    private editorService: EditorService,
    private layoutService: LayoutService,
    private layoutConfigService: LayoutConfigService,
    private message: NzMessageService
  ) {
    NodeRegister.register(injector);
  }

  ngOnInit() {
    this.graph = new Graph({
      container: this.contianer()?.nativeElement,
      grid: true,
      panning: true,
      mousewheel: true,
    });
    this.graph.use(new Snapline({ enabled: true }));
    this.editorService.setGraph(this.graph);
    this.editorService.setflowGraph(FlowGraph.new());
    this.editorService.renderGraph();

    // Subscribe to layout updates (T038)
    this.layoutService.layoutResult$.subscribe(result => {
      console.log('Layout updated:', result?.metadata);

      // T083: Notify user about multiple subgraphs
      if (result && result.metadata.branchGroupCount > 1) {
        this.message.warning(
          `Multiple disconnected graphs detected (${result.metadata.branchGroupCount}). ` +
          'Layout may not be optimal.'
        );
      }
    });

    // T076: Subscribe to configuration changes to trigger layout updates
    this.configSubscription = this.layoutConfigService.config$.subscribe(() => {
      // LayoutService will automatically trigger layout when config changes
      console.log('Configuration changed, layout will be updated');
    });

    // Trigger layout after graph is rendered (T039)
    setTimeout(() => {
      this.layoutService.triggerLayout();
    }, 100);
  }

  /**
   * Clean up subscriptions on component destroy
   */
  ngOnDestroy(): void {
    this.configSubscription.unsubscribe();
  }
}
