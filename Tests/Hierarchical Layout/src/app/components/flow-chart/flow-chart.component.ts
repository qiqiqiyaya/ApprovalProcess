import { Component, ElementRef, ViewChild, AfterViewInit, OnDestroy, HostListener } from '@angular/core';
import { Graph, Shape } from '@antv/x6';
import { FlowLayoutService } from '../../services/flow-layout.service';

@Component({
  selector: 'app-flow-chart',
  templateUrl: './flow-chart.component.html',
  styleUrls: ['./flow-chart.component.scss']
})
export class FlowChartComponent implements AfterViewInit, OnDestroy {
  @ViewChild('container', { static: true }) containerRef!: ElementRef;

  private graph!: Graph;
  private layoutService = new FlowLayoutService();

  // 拖拽相关
  public isDragging = false;
  private dragNodeType: string = '';
  private dragNodeLabel: string = '';

  // 节点计数器
  private nodeCounter = 0;

  // 节点配置
  private nodeConfigs = {
    start: {
      shape: 'circle',
      width: 60,
      height: 60,
      attrs: {
        body: { fill: '#52c41a', stroke: '#52c41a' },
        label: { fill: '#fff', fontSize: 14 }
      }
    },
    end: {
      shape: 'circle',
      width: 60,
      height: 60,
      attrs: {
        body: { fill: '#ff4d4f', stroke: '#ff4d4f' },
        label: { fill: '#fff', fontSize: 14 }
      }
    },
    task: {
      shape: 'rect',
      width: 120,
      height: 40,
      attrs: {
        body: {
          stroke: '#d9d9d9',
          strokeWidth: 1,
          fill: '#fafafa',
          rx: 4,
          ry: 4,
        },
        label: { fontSize: 14 }
      }
    },
    parallel: {
      shape: 'rect',
      width: 100,
      height: 60,
      attrs: {
        body: {
          stroke: '#1890ff',
          strokeWidth: 2,
          fill: '#e6f7ff',
          rx: 4,
          ry: 4,
        },
        label: { fill: '#1890ff', fontSize: 14 }
      }
    },
    merge: {
      shape: 'rect',
      width: 100,
      height: 60,
      attrs: {
        body: {
          stroke: '#52c41a',
          strokeWidth: 2,
          fill: '#f6ffed',
          rx: 4,
          ry: 4,
        },
        label: { fill: '#52c41a', fontSize: 14 }
      }
    }
  };

  ngAfterViewInit(): void {
    this.initGraph();
    this.registerCustomNodes();
    this.createSampleFlow();

    // 延迟初始化事件
    // setTimeout(() => {
    //   this.initEvents();
    // }, 1000);
    this.graph.centerContent();
  }

  ngOnDestroy(): void {
    if (this.graph) {
      this.graph.dispose();
    }
  }

  /**
   * 初始化图实例 - 简化配置
   */
  private initGraph(): void {
    this.graph = new Graph({
      container: this.containerRef.nativeElement,
      grid: {
        size: 10,
        visible: true,
        type: 'dot',
        args: {
          color: '#d0d0d0',
          thickness: 1,
        },
      },
      background: {
        color: '#fafafa',
      },
      panning: true,
      mousewheel: true,
      connecting: {
        router: 'manhattan',
        connector: 'rounded',
        anchor: 'center',
        connectionPoint: 'boundary',
        allowBlank: false,
        snap: {
          radius: 20,
        },
        createEdge() {
          return new Shape.Edge({
            attrs: {
              line: {
                stroke: '#d9d9d9',
                strokeWidth: 2,
                targetMarker: {
                  name: 'block',
                  width: 12,
                  height: 8,
                },
              },
            },
          });
        },
      },
    });
  }

  /**
   * 初始化事件 - 优化事件处理
   */
  private initEvents(): void {
    // 移除之前的重复事件监听
    this.graph.off();

    // 节点添加事件 - 延迟布局
    this.graph.on('node:added', ({ node }) => {
      console.log('节点添加:', node.id);

      // 延迟执行布局，确保节点完全渲染
      setTimeout(() => {
        this.layoutService.autoLayout(this.graph);
      }, 200);
    });

    // 节点删除事件
    this.graph.on('node:removed', () => {
      setTimeout(() => {
        this.layoutService.autoLayout(this.graph);
      }, 200);
    });

    // 节点移动事件 - 仅处理合并和并行节点
    this.graph.on('node:moving', ({ node, x, y }) => {
      const shape = node.shape;
      if (shape === 'parallel' || shape === 'merge') {
        const currentPos = node.position();
        node.position(currentPos.x, y);
      }
    });

    // 边添加事件
    this.graph.on('edge:connected', ({ edge }) => {
      console.log('边连接:', edge.id);

      setTimeout(() => {
        this.layoutService.autoLayout(this.graph);
      }, 200);
    });

    // 空白处点击事件 - 只有在拖拽状态下才添加节点
    this.graph.on('blank:click', ({ x, y }) => {
      // 只有当没有节点被拖拽时才在空白处添加节点
      if (!this.isDragging) {
        this.addNodeAtPosition('task', '新节点', x, y);
      }
      this.isDragging = false;
    });

    // 节点点击事件
    this.graph.on('node:click', ({ node }) => {
      console.log('点击节点:', node.id, node.shape);
      this.isDragging = false;
    });

    // 边点击事件
    this.graph.on('edge:click', ({ edge }) => {
      console.log('点击边:', edge.id);
      this.isDragging = false;
    });

    // 键盘快捷键支持
    this.graph.bindKey(['backspace', 'delete'], (e) => {
      const cells = this.graph.getSelectedCells();
      if (cells.length > 0) {
        this.graph.removeCells(cells);
      }
    });

    this.graph.bindKey(['meta+a', 'ctrl+a'], (e) => {
      this.graph.select(this.graph.getCells());
    });

    this.graph.bindKey(['meta+z', 'ctrl+z'], (e) => {
      if (this.graph.canUndo()) {
        this.graph.undo();
      }
    });

    this.graph.bindKey(['meta+shift+z', 'ctrl+shift+z'], (e) => {
      if (this.graph.canRedo()) {
        this.graph.redo();
      }
    });

    this.graph.bindKey(['equal', 'numpadadd', 'minus', 'numpadsubtract'], (e) => {
      const zoom = this.graph.zoom();
      if (e.key === 'equal' || e.key === 'numpadadd') {
        this.graph.zoom(zoom * 1.1);
      } else {
        this.graph.zoom(zoom * 0.9);
      }
    });
  }

  /**
   * 注册自定义节点 - 优化端口配置
   */
  private registerCustomNodes(): void {
    // 注册并行节点
    Graph.registerNode('parallel', {
      inherit: 'rect',
      width: 100,
      height: 60,
      attrs: {
        body: {
          stroke: '#1890ff',
          strokeWidth: 2,
          fill: '#e6f7ff',
          rx: 4,
          ry: 4,
        },
        label: {
          text: '并行',
          fill: '#1890ff',
          fontSize: 14,
          refX: 0.5,
          refY: 0.5,
        },
        symbol: {
          at: '0.5 0.5',
          d: 'M -15 -10 L 15 -10 M -15 0 L 15 0 M -15 10 L 15 10',
          stroke: '#1890ff',
          strokeWidth: 2,
        }
      },
      ports: {
        groups: {
          top: {
            position: 'top',
            attrs: {
              circle: {
                r: 4,
                magnet: true,
                stroke: '#1890ff',
                strokeWidth: 2,
                fill: '#fff',
              },
            },
          },
          bottom: {
            position: 'bottom',
            attrs: {
              circle: {
                r: 4,
                magnet: true,
                stroke: '#1890ff',
                strokeWidth: 2,
                fill: '#fff',
              },
            },
          },
        },
        items: [
          { group: 'top' },
          { group: 'bottom' },
        ],
      },
    });

    // 注册合并节点
    Graph.registerNode('merge', {
      inherit: 'rect',
      width: 100,
      height: 60,
      attrs: {
        body: {
          stroke: '#52c41a',
          strokeWidth: 2,
          fill: '#f6ffed',
          rx: 4,
          ry: 4,
        },
        label: {
          text: '合并',
          fill: '#52c41a',
          fontSize: 14,
          refX: 0.5,
          refY: 0.5,
        },
        symbol: {
          at: '0.5 0.5',
          d: 'M -15 -10 L 15 -10 M -15 0 L 15 0 M -15 10 L 15 10',
          stroke: '#52c41a',
          strokeWidth: 2,
        }
      },
      ports: {
        groups: {
          top: {
            position: 'top',
            attrs: {
              circle: {
                r: 4,
                magnet: true,
                stroke: '#52c41a',
                strokeWidth: 2,
                fill: '#fff',
              },
            },
          },
          bottom: {
            position: 'bottom',
            attrs: {
              circle: {
                r: 4,
                magnet: true,
                stroke: '#52c41a',
                strokeWidth: 2,
                fill: '#fff',
              },
            },
          },
        },
        items: [
          { group: 'top' },
          { group: 'bottom' },
        ],
      },
    });

    // 注册其他节点类型
    Graph.registerNode('start', {
      inherit: 'circle',
      width: 60,
      height: 60,
      attrs: {
        body: {
          fill: '#52c41a',
          stroke: '#52c41a',
        },
        label: {
          text: '开始',
          fill: '#fff',
          fontSize: 14,
          refX: 0.5,
          refY: 0.5,
        }
      },
      ports: {
        groups: {
          bottom: {
            position: 'bottom',
            attrs: {
              circle: {
                r: 4,
                magnet: true,
                stroke: '#52c41a',
                strokeWidth: 2,
                fill: '#fff',
              },
            },
          },
        },
        items: [
          { group: 'bottom' },
        ],
      },
    });

    Graph.registerNode('end', {
      inherit: 'circle',
      width: 60,
      height: 60,
      attrs: {
        body: {
          fill: '#ff4d4f',
          stroke: '#ff4d4f',
        },
        label: {
          text: '结束',
          fill: '#fff',
          fontSize: 14,
          refX: 0.5,
          refY: 0.5,
        }
      },
      ports: {
        groups: {
          top: {
            position: 'top',
            attrs: {
              circle: {
                r: 4,
                magnet: true,
                stroke: '#ff4d4f',
                strokeWidth: 2,
                fill: '#fff',
              },
            },
          },
        },
        items: [
          { group: 'top' },
        ],
      },
    });
  }

  /**
   * 创建示例流程 - 优化初始位置
   */
  private createSampleFlow(): void {
    // 清空画布
    this.graph.clearCells();

    // 初始创建节点，先不连接，等布局后再连接
    const startNode = this.addNode('circle', '开始', 400, 50, false);
    const taskNode = this.addNode('rect', '任务节点', 400, 150, false);
    const parallelNode = this.addNode('parallel', '并行节点', 400, 250, false);

    const branch1Node1 = this.addNode('rect', '分支1-节点1', 240, 350, false);
    const branch1Node2 = this.addNode('rect', '分支1-节点2', 240, 450, false);

    const branch2Node1 = this.addNode('rect', '分支2-节点1', 560, 350, false);
    const branch2Node2 = this.addNode('rect', '分支2-节点2', 560, 450, false);

    const mergeNode = this.addNode('merge', '合并节点', 400, 550, false);
    const endNode = this.addNode('circle', '结束', 400, 650, false);

    this.connectNodes(startNode.id!, taskNode.id!);
    this.connectNodes(taskNode.id!, parallelNode.id!);

    this.connectNodes(parallelNode.id!, branch1Node1.id!);
    this.connectNodes(branch1Node1.id!, branch1Node2.id!);

    this.connectNodes(parallelNode.id!, branch2Node1.id!);
    this.connectNodes(branch2Node1.id!, branch2Node2.id!);

    this.connectNodes(branch1Node2.id!, mergeNode.id!);
    this.connectNodes(branch2Node2.id!, mergeNode.id!);
    this.connectNodes(mergeNode.id!, endNode.id!);

    debugger;
    this.layoutService.autoLayout(this.graph);
  }

  /**
   * 添加节点（支持延迟布局）
   */
  addNode(type: string, label: string, x?: number, y?: number, autoLayout: boolean = true): any {
    const id = `node_${this.nodeCounter++}`;
    const config = this.nodeConfigs[type as keyof typeof this.nodeConfigs] || this.nodeConfigs.task;

    const node = this.graph.addNode({
      id,
      shape: type,
      // x: x || 100,
      // y: y || 100,
      width: config.width,
      height: config.height,
      label: label,
      attrs: config.attrs,
    });
    // if (autoLayout) {
    //   // 延迟执行布局
    //   this.layoutService.autoLayout(this.graph);
    // }

    return node;
  }

  /**
   * 在指定位置添加节点（用于拖拽）
   */
  addNodeAtPosition(type: string, label: string, x: number, y: number): void {
    // 调整坐标，使节点中心对准鼠标位置
    const config = this.nodeConfigs[type as keyof typeof this.nodeConfigs] || this.nodeConfigs.task;
    const adjustedX = x - config.width / 2;
    const adjustedY = y - config.height / 2;

    this.addNode(type, label, adjustedX, adjustedY, true);
  }

  /**
   * 连接节点
   */
  connectNodes(sourceId: string, targetId: string): void {
    try {
      this.graph.addEdge({
        source: { cell: sourceId },
        target: { cell: targetId },
        attrs: {
          line: {
            stroke: '#d9d9d9',
            strokeWidth: 2,
            targetMarker: {
              name: 'block',
              width: 12,
              height: 8,
            },
          },
        },
      });
    } catch (error) {
      console.error('连接节点失败:', error);
    }
  }

  // === 拖拽相关方法 ===

  /**
   * 开始拖拽节点
   */
  startDrag(nodeType: string, label: string, event: DragEvent): void {
    this.isDragging = true;
    this.dragNodeType = nodeType;
    this.dragNodeLabel = label;

    // 设置拖拽数据
    if (event.dataTransfer) {
      event.dataTransfer.setData('text/plain', JSON.stringify({
        type: nodeType,
        label: label
      }));
      event.dataTransfer.effectAllowed = 'copy';
    }

    // 添加拖拽反馈样式
    const target = event.target as HTMLElement;
    if (target) {
      target.classList.add('dragging');
    }
  }

  /**
   * 结束拖拽
   */
  endDrag(event: DragEvent): void {
    const target = event.target as HTMLElement;
    if (target) {
      target.classList.remove('dragging');
    }
  }

  /**
   * 处理画布上的拖拽放置
   */
  @HostListener('document:dragover', ['$event'])
  onDragOver(event: DragEvent): void {
    // 阻止默认行为以允许放置
    event.preventDefault();
  }

  /**
   * 处理画布上的放置
   */
  onDrop(event: DragEvent): void {
    event.preventDefault();

    if (!this.isDragging || !this.dragNodeType) return;

    // 获取画布相对坐标
    const container = this.containerRef.nativeElement;
    const rect = container.getBoundingClientRect();

    // 计算相对于画布的坐标
    const x = event.clientX - rect.left;
    const y = event.clientY - rect.top;

    // 添加节点
    this.addNodeAtPosition(this.dragNodeType, this.dragNodeLabel, x, y);

    // 重置拖拽状态
    this.isDragging = false;
    this.dragNodeType = '';
    this.dragNodeLabel = '';
  }

  /**
   * 处理拖拽离开
   */
  @HostListener('document:dragleave', ['$event'])
  onDragLeave(event: DragEvent): void {
    // 如果拖拽离开文档，重置状态
    if (!event.relatedTarget ||
      !document.body.contains(event.relatedTarget as Node)) {
      this.isDragging = false;
    }
  }

  /**
   * 点击添加节点（兼容原有功能）
   */
  onClickAddNode(nodeType: string, label: string): void {
    // 在画布中心添加节点
    const centerX = 400;
    const centerY = 300;
    this.addNodeAtPosition(nodeType, label, centerX, centerY);
  }

  // === 工具栏方法 ===

  /**
   * 执行自动布局
   */
  autoLayout(): void {
    this.layoutService.autoLayout(this.graph);
  }

  /**
   * 对齐并行和合并节点
   */
  alignParallelAndMerge(): void {
    this.layoutService.alignParallelAndMerge(this.graph);
  }

  /**
   * 垂直对齐选中的节点
   */
  alignSelectedVertical(): void {
    const selectedCells = this.graph.getSelectedCells();
    const nodeIds = selectedCells
      .filter(cell => cell.isNode())
      .map(cell => cell.id);

    this.layoutService.alignNodesVertical(this.graph, nodeIds);
  }

  /**
   * 清除画布
   */
  clearCanvas(): void {
    this.graph.clearCells();
  }

  /**
   * 创建新的并行流程
   */
  createNewParallelFlow(): void {
    this.createSampleFlow();
  }

  /**
   * 添加并行分支
   */
  addParallelBranch(): void {
    const selectedNodes = this.graph.getSelectedCells().filter(cell => cell.isNode());
    if (selectedNodes.length === 0) {
      alert('请先选择一个节点作为并行节点');
      return;
    }

    const sourceNode = selectedNodes[0];

    // 添加两个分支节点
    const branch1Node = this.addNode('task', '新分支1', sourceNode.position().x - 160, sourceNode.position().y + 80);
    const branch2Node = this.addNode('task', '新分支2', sourceNode.position().x + 160, sourceNode.position().y + 80);

    // 连接到源节点
    this.connectNodes(sourceNode.id!, branch1Node.id!);
    this.connectNodes(sourceNode.id!, branch2Node.id!);

    // 添加合并节点
    const mergeNode = this.addNode('merge', '合并节点', sourceNode.position().x, sourceNode.position().y + 240);

    // 连接分支到合并节点
    this.connectNodes(branch1Node.id!, mergeNode.id!);
    this.connectNodes(branch2Node.id!, mergeNode.id!);
  }

  /**
   * 获取图数据
   */
  getGraphData(): any {
    return this.graph?.toJSON() || {};
  }

  /**
   * 获取节点数量
   */
  getNodeCount(): number {
    const data = this.getGraphData();
    return data?.cells?.filter((c: any) => c.shape !== 'edge')?.length || 0;
  }

  /**
   * 获取边数量
   */
  getEdgeCount(): number {
    const data = this.getGraphData();
    return data?.cells?.filter((c: any) => c.shape === 'edge')?.length || 0;
  }

  /**
   * 导出为图片
   */
  exportAsImage(): void {
    this.graph.toPNG((dataUri: string) => {
      const link = document.createElement('a');
      link.download = 'flow-chart.png';
      link.href = dataUri;
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }, {
      width: 800,
      height: 600,
      backgroundColor: '#fafafa',
      padding: 20,
      quality: 1
    });
  }

  /**
   * 导出为JSON
   */
  exportAsJson(): void {
    const data = this.getGraphData();
    const dataStr = JSON.stringify(data, null, 2);
    const dataUri = 'data:application/json;charset=utf-8,' + encodeURIComponent(dataStr);

    const link = document.createElement('a');
    link.download = 'flow-chart.json';
    link.href = dataUri;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
}