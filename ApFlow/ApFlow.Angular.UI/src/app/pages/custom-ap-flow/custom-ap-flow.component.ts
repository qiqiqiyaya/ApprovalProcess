import { Component, OnInit, AfterViewInit, ViewChild, ElementRef, HostListener } from '@angular/core';
import { Graph, Shape } from '@antv/x6';

// 导入子组件
import { NodePropertiesComponent } from './components/node-properties/node-properties.component';
import { EdgePropertiesComponent } from './components/edge-properties/edge-properties.component';
import { FlowEdge, FlowNode, FlowGraph } from './models/custom-ap-flow.models';
import { CustomApFlowService } from './services/custom-ap-flow.service';

@Component({
    selector: 'app-custom-ap-flow',
    templateUrl: './custom-ap-flow.component.html',
    styleUrls: ['./custom-ap-flow.component.css'],
    standalone: false
})
export class CustomApFlowComponent implements OnInit, AfterViewInit {
    // 图形容器引用
    @ViewChild('graphContainer', { static: true }) graphContainer!: ElementRef;

    // X6 图形实例
    private graph!: Graph;

    // 当前流程数据
    currentFlowData: FlowGraph = {
        name: '新建流程',
        description: '',
        nodes: [],
        edges: [],
        variables: []
    };

    // 当前选中的节点或边
    selectedCell: any = null;

    // 模拟数据
    mockFlowTemplates = [
        { id: 'template1', name: '请假审批流程' },
        { id: 'template2', name: '报销审批流程' },
        { id: 'template3', name: '采购审批流程' }
    ];

    mockFlowInstances = [
        { id: 'instance1', name: '张三的请假申请', status: 'pending' },
        { id: 'instance2', name: '李四的报销申请', status: 'approved' },
        { id: 'instance3', name: '王五的采购申请', status: 'rejected' }
    ];

    // 流程数据
    flowNodes: FlowNode[] = [];
    flowEdges: FlowEdge[] = [];

    // 工具面板状态
    showToolPanel = true;
    showPropertyPanel = false;

    constructor(
        private customApFlowService: CustomApFlowService
    ) { }

    ngOnInit(): void {
        // 初始化组件数据
        this.initializeFlowData();
    }

    ngAfterViewInit(): void {
        // 初始化X6图形
        this.initializeGraph();

        // 监听窗口大小变化
        this.setupResizeListener();
    }

    /**
     * 初始化流程数据
     */
    private initializeFlowData(): void {
        // 从服务获取初始数据或设置默认数据
        this.flowNodes = this.customApFlowService.getDefaultNodes();
        this.flowEdges = this.customApFlowService.getDefaultEdges();
    }

    /**
     * 初始化X6图形
     */
    private initializeGraph(): void {
        // 创建图形实例
        this.graph = new Graph({
            container: this.graphContainer.nativeElement,
            // width: '100%',
            // height: '100%',
            background: {
                color: '#f5f5f5',
            },
            grid: {
                visible: true,
                type: 'doubleMesh',
                args: [
                    {
                        color: '#eee',
                        thickness: 1,
                    },
                    {
                        color: '#ddd',
                        thickness: 1,
                        factor: 4,
                    },
                ],
            },
            mousewheel: {
                enabled: true,
                zoomAtMousePosition: true,
                modifiers: 'ctrl',
                minScale: 0.5,
                maxScale: 3,
            },
            connecting: {
                router: {
                    name: 'manhattan',
                    args: {
                        padding: 1,
                    },
                },
                connector: {
                    name: 'rounded',
                    args: {
                        radius: 8,
                    },
                },
                anchor: 'center',
                connectionPoint: 'anchor',
                allowBlank: false,
                snap: {
                    radius: 20,
                },
                createEdge() {
                    return new Shape.Edge({
                        attrs: {
                            line: {
                                stroke: '#A2B1C3',
                                strokeWidth: 2,
                                targetMarker: {
                                    name: 'block',
                                    width: 12,
                                    height: 8,
                                },
                            },
                        },
                        zIndex: 0,
                    });
                },
                validateConnection({ targetMagnet }) {
                    return !!targetMagnet;
                },
            },
            highlighting: {
                magnetAvailable: {
                    name: 'stroke',
                    args: {
                        attrs: {
                            fill: '#fff',
                            stroke: '#31d0c6',
                            strokeWidth: 4,
                        },
                    },
                },
            },
            // resizing: true,
            // rotating: true,
            // selecting: {
            //     enabled: true,
            //     rubberband: true,
            //     movable: true,
            //     showNodeSelectionBox: true,
            // },
            // snapline: true,
            // keyboard: true,
            // clipboard: true,
        });

        // 注册自定义节点
        this.registerCustomNodes();

        // 绑定图形事件
        this.bindGraphEvents();

        // 渲染初始数据
        this.renderFlowData();
    }

    /**
     * 注册自定义节点
     */
    private registerCustomNodes(): void {
        // 注册审批人节点
        Graph.registerNode('approver-node', {
            inherit: 'rect',
            width: 180,
            height: 60,
            attrs: {
                body: {
                    strokeWidth: 1,
                    stroke: '#5F95FF',
                    fill: '#EFF4FF',
                    rx: 8,
                    ry: 8,
                },
                text: {
                    fontSize: 12,
                    fill: '#262626',
                },
            },
            ports: {
                groups: {
                    top: {
                        position: 'top',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    right: {
                        position: 'right',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    bottom: {
                        position: 'bottom',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    left: {
                        position: 'left',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                },
                items: [
                    { group: 'top' },
                    { group: 'right' },
                    { group: 'bottom' },
                    { group: 'left' },
                ],
            },
        });

        // 注册并行审批节点
        Graph.registerNode('parallel-node', {
            inherit: 'polygon',
            width: 180,
            height: 60,
            attrs: {
                body: {
                    strokeWidth: 1,
                    stroke: '#5F95FF',
                    fill: '#EFF4FF',
                    refPoints: '0,10 10,0 20,0 30,10 30,20 20,30 10,30 0,20',
                },
                text: {
                    fontSize: 12,
                    fill: '#262626',
                },
            },
            ports: {
                groups: {
                    top: {
                        position: 'top',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    right: {
                        position: 'right',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    bottom: {
                        position: 'bottom',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    left: {
                        position: 'left',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                },
                items: [
                    { group: 'top' },
                    { group: 'right' },
                    { group: 'bottom' },
                    { group: 'left' },
                ],
            },
        });

        // 注册抄送节点
        Graph.registerNode('cc-node', {
            inherit: 'ellipse',
            width: 180,
            height: 60,
            attrs: {
                body: {
                    strokeWidth: 1,
                    stroke: '#5F95FF',
                    fill: '#EFF4FF',
                },
                text: {
                    fontSize: 12,
                    fill: '#262626',
                },
            },
            ports: {
                groups: {
                    top: {
                        position: 'top',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    right: {
                        position: 'right',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    bottom: {
                        position: 'bottom',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    left: {
                        position: 'left',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                },
                items: [
                    { group: 'top' },
                    { group: 'right' },
                    { group: 'bottom' },
                    { group: 'left' },
                ],
            },
        });

        // 注册条件分支节点
        Graph.registerNode('condition-node', {
            inherit: 'polygon',
            width: 180,
            height: 80,
            attrs: {
                body: {
                    strokeWidth: 1,
                    stroke: '#5F95FF',
                    fill: '#EFF4FF',
                    refPoints: '0,40 40,0 80,40 40,80',
                },
                text: {
                    fontSize: 12,
                    fill: '#262626',
                },
            },
            ports: {
                groups: {
                    top: {
                        position: 'top',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    right: {
                        position: 'right',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    bottom: {
                        position: 'bottom',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                    left: {
                        position: 'left',
                        attrs: {
                            circle: {
                                magnet: true,
                                stroke: '#31d0c6',
                                r: 4,
                            },
                        },
                    },
                },
                items: [
                    { group: 'top' },
                    { group: 'right' },
                    { group: 'bottom' },
                    { group: 'left' },
                ],
            },
        });
    }

    /**
     * 绑定图形事件
     */
    private bindGraphEvents(): void {
        // 节点点击事件
        this.graph.on('node:click', ({ node }) => {
            this.selectedCell = node;
            this.showPropertyPanel = true;
            this.onNodeSelected(node);
        });

        // 边点击事件
        this.graph.on('edge:click', ({ edge }) => {
            this.selectedCell = edge;
            this.showPropertyPanel = true;
            this.onEdgeSelected(edge);
        });

        // 空白区域点击事件
        this.graph.on('blank:click', () => {
            this.selectedCell = null;
            this.showPropertyPanel = false;
        });

        // 节点添加事件
        this.graph.on('node:added', ({ node }) => {
            this.onNodeAdded(node);
        });

        // 边添加事件
        this.graph.on('edge:added', ({ edge }) => {
            this.onEdgeAdded(edge);
        });

        // 节点删除事件
        this.graph.on('node:removed', ({ node }) => {
            this.onNodeRemoved(node);
        });

        // 边删除事件
        this.graph.on('edge:removed', ({ edge }) => {
            this.onEdgeRemoved(edge);
        });
    }

    /**
     * 渲染流程数据
     */
    private renderFlowData(): void {
        // 清空现有图形
        this.graph.clearCells();

        // 添加节点
        const nodeMap = new Map<string, any>();
        this.flowNodes.forEach(node => {
            const graphNode = this.graph.addNode({
                id: node.id,
                shape: node.shape,
                x: node.x,
                y: node.y,
                label: node.label,
                data: node.data,
            });
            nodeMap.set(node.id, graphNode);
        });

        // 添加边
        this.flowEdges.forEach(edge => {
            this.graph.addEdge({
                id: edge.id,
                source: edge.source,
                target: edge.target,
                label: edge.label,
                data: edge.data,
            });
        });
    }

    /**
     * 添加节点
     */
    addNode(nodeType: string): void {
        const id = `node_${Date.now()}`;
        const nodeData = {
            id,
            shape: `${nodeType}-node`,
            x: 100 + Math.random() * 200,
            y: 100 + Math.random() * 200,
            label: this.getNodeLabel(nodeType),
            data: {
                nodeType,
                properties: {},
            },
        };

        this.graph.addNode(nodeData);
        this.flowNodes.push(nodeData);
    }

    /**
     * 获取节点标签
     */
    private getNodeLabel(nodeType: string): string {
        switch (nodeType) {
            case 'approver':
                return '审批人';
            case 'parallel':
                return '并行审批';
            case 'cc':
                return '抄送人';
            case 'condition':
                return '条件分支';
            default:
                return '未知节点';
        }
    }

    /**
     * 节点选中处理
     */
    private onNodeSelected(node: any): void {
        // 获取节点数据并显示属性面板
        console.log('Node selected:', node.getData());
    }

    /**
     * 边选中处理
     */
    private onEdgeSelected(edge: any): void {
        // 获取边数据并显示属性面板
        console.log('Edge selected:', edge.getData());
    }

    /**
     * 节点添加处理
     */
    private onNodeAdded(node: any): void {
        // 更新数据模型
        const nodeData = {
            id: node.id,
            shape: node.shape,
            x: node.position().x,
            y: node.position().y,
            label: node.getLabel(),
            data: node.getData() || {},
        };

        // 检查节点是否已存在
        const existingIndex = this.flowNodes.findIndex(n => n.id === node.id);
        if (existingIndex === -1) {
            this.flowNodes.push(nodeData);
        } else {
            this.flowNodes[existingIndex] = nodeData;
        }
    }

    /**
     * 边添加处理
     */
    private onEdgeAdded(edge: any): void {
        // 更新数据模型
        const edgeData = {
            id: edge.id,
            source: edge.getSource().cell,
            target: edge.getTarget().cell,
            label: edge.getLabel(),
            data: edge.getData() || {},
        };

        // 检查边是否已存在
        const existingIndex = this.flowEdges.findIndex(e => e.id === edge.id);
        if (existingIndex === -1) {
            this.flowEdges.push(edgeData);
        } else {
            this.flowEdges[existingIndex] = edgeData;
        }
    }

    /**
     * 节点删除处理
     */
    private onNodeRemoved(node: any): void {
        // 更新数据模型
        this.flowNodes = this.flowNodes.filter(n => n.id !== node.id);

        // 删除相关边
        this.flowEdges = this.flowEdges.filter(e => e.source !== node.id && e.target !== node.id);
    }

    /**
     * 边删除处理
     */
    private onEdgeRemoved(edge: any): void {
        // 更新数据模型
        this.flowEdges = this.flowEdges.filter(e => e.id !== edge.id);
    }

    /**
   * 保存流程
   */
    saveFlow(): void {
        // 更新节点位置
        this.flowNodes.forEach(node => {
            const graphNode = this.graph.getCellById(node.id);
            if (graphNode) {
                const position = (graphNode as any).position || (graphNode as any).getPosition();
                if (position) {
                    node.x = position.x;
                    node.y = position.y;
                }
            }
        });

        // 保存到服务
        this.customApFlowService.saveFlow({
            name: this.currentFlowData.name,
            description: this.currentFlowData.description,
            nodes: this.flowNodes,
            edges: this.flowEdges,
            variables: this.currentFlowData.variables || []
        });
    }

    /**
     * 加载流程模板
     */
    loadTemplate(templateId: string): void {
        // 这里应该从服务加载模板数据
        console.log('加载模板:', templateId);

        // 模拟加载模板数据
        this.flowNodes = this.customApFlowService.getDefaultNodes();
        this.flowEdges = this.customApFlowService.getDefaultEdges();

        // 重新渲染图形
        this.renderFlowData();
    }

    /**
     * 创建流程实例
     */
    createInstance(templateId: string): void {
        // 这里应该调用服务创建流程实例
        console.log('创建流程实例:', templateId);

        // 模拟创建流程实例
        const formData = {
            applicant: '张三',
            department: '技术部',
            reason: '事假',
            startDate: '2023-12-25',
            endDate: '2023-12-26'
        };

        this.customApFlowService.createFlowInstance(templateId, formData, 'user1').subscribe(
            instance => {
                console.log('流程实例创建成功:', instance);
                // 这里可以添加成功提示
            },
            error => {
                console.error('流程实例创建失败:', error);
                // 这里可以添加错误提示
            }
        );
    }

    /**
     * 验证流程图
     */
    validateFlow(): void {
        const flowData = {
            name: this.currentFlowData.name,
            description: this.currentFlowData.description,
            nodes: this.flowNodes,
            edges: this.flowEdges,
            variables: this.currentFlowData.variables || []
        };

        const validation = this.customApFlowService.validateFlowGraph(flowData);

        if (validation.valid) {
            console.log('流程图验证通过');
            // 这里可以添加成功提示
        } else {
            console.error('流程图验证失败:', validation.errors);
            // 这里可以显示验证错误
        }
    }

    /**
     * 导出流程图
     */
    exportFlow(): void {
        const flowData = {
            name: this.currentFlowData.name,
            description: this.currentFlowData.description,
            nodes: this.flowNodes,
            edges: this.flowEdges
        };

        const dataStr = JSON.stringify(flowData, null, 2);
        const dataBlob = new Blob([dataStr], { type: 'application/json' });

        const link = document.createElement('a');
        link.href = URL.createObjectURL(dataBlob);
        link.download = `${this.currentFlowData.name}.json`;
        link.click();
    }

    /**
     * 导入流程图
     */
    importFlow(event: any): void {
        const file = event.target.files[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onload = (e) => {
            try {
                const flowData = JSON.parse(e.target?.result as string);

                // 更新流程数据
                this.currentFlowData.name = flowData.name || '导入的流程';
                this.currentFlowData.description = flowData.description || '';
                this.flowNodes = flowData.nodes || [];
                this.flowEdges = flowData.edges || [];

                // 重新渲染图形
                this.renderFlowData();

                console.log('流程图导入成功');
            } catch (error) {
                console.error('流程图导入失败:', error);
            }
        };

        reader.readAsText(file);
    }

    /**
     * 清空画布
     */
    clearCanvas(): void {
        if (confirm('确定要清空画布吗？此操作不可撤销。')) {
            this.graph.clearCells();
            this.flowNodes = [];
            this.flowEdges = [];
            this.selectedCell = null;
            this.showPropertyPanel = false;
        }
    }

    /**
     * 缩放适配
     */
    zoomToFit(): void {
        this.graph.zoomToFit({ padding: 20 });
    }

    /**
     * 监听窗口大小变化
     */
    @HostListener('window:resize')
    setupResizeListener(): void {
        // 重新计算图形大小
        setTimeout(() => {
            if (this.graph && this.graphContainer) {
                this.graph.resize(this.graphContainer.nativeElement.clientWidth, this.graphContainer.nativeElement.clientHeight);
            }
        }, 300);
    }

    /**
     * 切换工具面板
     */
    toggleToolPanel(): void {
        this.showToolPanel = !this.showToolPanel;
        setTimeout(() => {
            this.graph.resize(this.graphContainer.nativeElement.clientWidth, this.graphContainer.nativeElement.clientHeight);
        }, 300);
    }

    /**
   * 切换属性面板
   */
    togglePropertyPanel(): void {
        this.showPropertyPanel = !this.showPropertyPanel;
        setTimeout(() => {
            this.graph.resize(this.graphContainer.nativeElement.clientWidth, this.graphContainer.nativeElement.clientHeight);
        }, 300);
    }

    /**
   * 关闭属性面板
   */
    closePropertiesPanel(): void {
        this.showPropertyPanel = false;
        this.selectedCell = null;
    }

    /**
     * 更新节点位置
     */
    updateNodePositions(): void {
        this.flowNodes.forEach(node => {
            const graphNode = this.graph.getCellById(node.id);
            if (graphNode) {
                const position = (graphNode as any).position || (graphNode as any).getPosition();
                if (position) {
                    node.x = position.x;
                    node.y = position.y;
                }
            }
        });
    }
}