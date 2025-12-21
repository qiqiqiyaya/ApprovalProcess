import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { ProcessDefinition, ProcessNode, NodeType, ProcessEdge } from '../models/process-designer.models';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-flow-designer',
  templateUrl: './flow-designer.component.html',
  styleUrls: ['./flow-designer.component.css'],
  imports:[FormsModule,CommonModule]
})
export class FlowDesignerComponent implements AfterViewInit {
  @ViewChild('canvas') canvasRef!: ElementRef<HTMLDivElement>;
  @Input() processDefinition!: ProcessDefinition;
  @Output() processDefinitionChange = new EventEmitter<ProcessDefinition>();
  
  private canvas!: HTMLDivElement;
  selectedNode: ProcessNode | null = null;
  private isDragging = false;
  private dragStartPos = { x: 0, y: 0 };
  NodeType: typeof NodeType = NodeType;

  jsonString='{"name":"默认流程","description":"","nodes":[{"id":"start","type":"start","name":"开始","position":{"x":100,"y":100},"size":{"width":80,"height":40},"config":{},"nextNodes":["approval1"]},{"id":"approval1","type":"approval","name":"主管审批","position":{"x":300,"y":100},"size":{"width":120,"height":60},"config":{"approvers":[{"type":"user","value":"manager1","name":"张主管","required":true}]},"nextNodes":["end"]},{"id":"end","type":"end","name":"结束","position":{"x":500,"y":100},"size":{"width":80,"height":40},"config":{},"nextNodes":[]}],"edges":[{"id":"edge1","source":"start","target":"approval1"},{"id":"edge2","source":"approval1","target":"end"}],"variables":[]}';
  
  nodeTemplates = [
    { type: NodeType.START, name: '开始', icon: 'play_arrow', color: '#52c41a' },
    { type: NodeType.APPROVAL, name: '审批', icon: 'how_to_reg', color: '#1890ff' },
    { type: NodeType.CONDITION, name: '条件', icon: 'code', color: '#722ed1' },
    { type: NodeType.CC, name: '抄送', icon: 'people', color: '#fa8c16' },
    { type: NodeType.END, name: '结束', icon: 'flag', color: '#f5222d' }
  ];
  private isAddSvgContainer=false;

  constructor(private renderer2: Renderer2) {

  }

  ngAfterViewInit() {
    this.processDefinition = JSON.parse(this.jsonString);

    this.canvas = this.canvasRef.nativeElement;
    this.initCanvas();
    this.renderProcess();
  }

  private initCanvas() {
    this.canvas.addEventListener('dragover', this.onDragOver.bind(this));
    this.canvas.addEventListener('drop', this.onDrop.bind(this));
    this.canvas.addEventListener('click', this.onCanvasClick.bind(this));
  }

  public renderProcess() {
    if(!this.processDefinition)return;
    this.clearCanvas();
    
    this.processDefinition.nodes.forEach(node => {
      this.renderNode(node);
    });
    
    this.processDefinition.edges.forEach(edge => {
      this.renderEdge(edge);
    });
  }

  private renderNode(node: ProcessNode) {
    const nodeElement = document.createElement('div');
    nodeElement.className = 'process-node';
    nodeElement.id = node.id;
    nodeElement.style.left = node.position.x + 'px';
    nodeElement.style.top = node.position.y + 'px';
    nodeElement.style.width = node.size.width + 'px';
    nodeElement.style.height = node.size.height + 'px';
    nodeElement.style.position = 'absolute';
    
    const template = this.nodeTemplates.find(t => t.type === node.type);
    nodeElement.style.borderColor = template?.color || '#ccc';
    
    nodeElement.innerHTML = `
      <div class="node-header" style="background: ${template?.color}">
        <span class="material-icons node-icon">${template?.icon}</span>
        <span class="node-name">${node.name}</span>
      </div>
      <div class="node-content">
        ${this.getNodeContent(node)}
      </div>
      <div class="connection-handle source"></div>
      <div class="connection-handle target"></div>
    `;
    
    nodeElement.addEventListener('mousedown', this.onNodeMouseDown.bind(this));
    nodeElement.addEventListener('dblclick', this.onNodeDoubleClick.bind(this));
    
    this.canvas.appendChild(nodeElement);
  }

  private getNodeContent(node: ProcessNode): string {
    switch (node.type) {
      case NodeType.APPROVAL:
        const approvers = node.config.approvers || [];
        return approvers.map(a => a.name).join('、') || '请设置审批人';
      case NodeType.CONDITION:
        const conditions = node.config.conditions || [];
        return conditions.length > 0 ? `${conditions.length}个条件` : '请设置条件';
      case NodeType.CC:
        return '抄送人设置';
      default:
        return '';
    }
  }

  private svgContainer:SVGElement;

  private renderEdge(edge: ProcessEdge) {
    const sourceNode = this.processDefinition.nodes.find(n => n.id === edge.source);
    const targetNode = this.processDefinition.nodes.find(n => n.id === edge.target);

    if (!sourceNode || !targetNode) return;

    if (!this.isAddSvgContainer) {
      this.svgContainer = document.createElementNS('http://www.w3.org/2000/svg', 'svg');
      this.svgContainer.setAttribute('id', 'process-edge-svg');
      this.svgContainer.style.position = 'absolute';
      this.svgContainer.style.pointerEvents = 'none';
      this.svgContainer.style.zIndex = '1';
      this.svgContainer.style.width = '100%';
      this.svgContainer.style.height = '100%';

      const path = this.calculateEdgePath(sourceNode, targetNode);
      this.svgContainer.innerHTML = `
        <path d="${path}" stroke="#999" stroke-width="2" fill="none" marker-end="url(#arrowhead)" />
        <defs>
          <marker id="arrowhead" markerWidth="10" markerHeight="7" refX="9" refY="3.5" orient="auto">
            <polygon points="0 0, 10 3.5, 0 7" fill="#999"/>
          </marker>
        </defs>`;

      this.canvas.appendChild(this.svgContainer);
      this.isAddSvgContainer = true;
    } else {
      let allHtml = this.svgContainer.innerHTML;
      const path = this.calculateEdgePath(sourceNode, targetNode);
      allHtml += `
        <path d="${path}" stroke="#999" stroke-width="2" fill="none" marker-end="url(#arrowhead)" />
        <defs>
          <marker id="arrowhead" markerWidth="10" markerHeight="7" refX="9" refY="3.5" orient="auto">
            <polygon points="0 0, 10 3.5, 0 7" fill="#999"/>
          </marker>
        </defs>`;

      this.svgContainer.innerHTML = allHtml;
    }
  }

  private calculateEdgePath(source: ProcessNode, target: ProcessNode): string {
    const startX = source.position.x + source.size.width;
    const startY = source.position.y + source.size.height / 2;
    const endX = target.position.x;
    const endY = target.position.y + target.size.height / 2;
    
    const midX = (startX + endX) / 2;
    
    return `M ${startX} ${startY} C ${midX} ${startY}, ${midX} ${endY}, ${endX} ${endY}`;
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    const nodeType = event.dataTransfer?.getData('nodeType') as NodeType;
    if (!nodeType) return;
    
    const rect = this.canvas.getBoundingClientRect();
    const x = event.clientX - rect.left - 60;
    const y = event.clientY - rect.top - 30;
    
    this.addNode(nodeType, x, y);
  }

  onNodeMouseDown(event: MouseEvent) {
    const nodeElement = (event.target as HTMLElement).closest('.process-node') as HTMLElement;
    if (!nodeElement) return;
    
    const nodeId = nodeElement.id;
    this.selectedNode = this.processDefinition.nodes.find(n => n.id === nodeId) || null;
    
    this.isDragging = true;
    this.dragStartPos = { x: event.clientX, y: event.clientY };
    
    document.addEventListener('mousemove', this.onMouseMove.bind(this));
    document.addEventListener('mouseup', this.onMouseUp.bind(this));
  }

  onMouseMove(event: MouseEvent) {
    if (!this.isDragging || !this.selectedNode) return;
    
    const deltaX = event.clientX - this.dragStartPos.x;
    const deltaY = event.clientY - this.dragStartPos.y;
    
    this.selectedNode.position.x += deltaX;
    this.selectedNode.position.y += deltaY;
    
    this.dragStartPos = { x: event.clientX, y: event.clientY };
    this.renderProcess();
  }

  onMouseUp() {
    this.isDragging = false;
    document.removeEventListener('mousemove', this.onMouseMove);
    document.removeEventListener('mouseup', this.onMouseUp);
    this.processDefinitionChange.emit(this.processDefinition);
  }

  onNodeDoubleClick(event: Event) {
    event.stopPropagation();
    if (this.selectedNode) {
      this.openNodeConfig(this.selectedNode);
    }
  }

  onCanvasClick() {
    this.selectedNode = null;
  }

  addNode(type: NodeType, x: number, y: number) {
    const nodeId = 'node_' + Date.now();
    const template = this.nodeTemplates.find(t => t.type === type);
    
    const newNode: ProcessNode = {
      id: nodeId,
      type: type,
      name: template?.name || '未命名',
      position: { x, y },
      size: { width: 120, height: 60 },
      config: {},
      nextNodes: []
    };
    
    this.processDefinition.nodes.push(newNode);
    this.renderProcess();
    this.processDefinitionChange.emit(this.processDefinition);
  }

  deleteNode(nodeId: string) {
    this.processDefinition.nodes = this.processDefinition.nodes.filter(n => n.id !== nodeId);
    this.processDefinition.edges = this.processDefinition.edges.filter(
      e => e.source !== nodeId && e.target !== nodeId
    );
    
    this.processDefinition.nodes.forEach(node => {
      node.nextNodes = node.nextNodes.filter(id => id !== nodeId);
    });
    
    this.renderProcess();
    this.processDefinitionChange.emit(this.processDefinition);
  }

  connectNodes(sourceId: string, targetId: string) {
    const edgeId = `edge_${sourceId}_${targetId}`;
    
    if (this.processDefinition.edges.some(e => e.source === sourceId && e.target === targetId)) {
      return;
    }
    
    const newEdge: ProcessEdge = {
      id: edgeId,
      source: sourceId,
      target: targetId
    };
    
    this.processDefinition.edges.push(newEdge);
    
    const sourceNode = this.processDefinition.nodes.find(n => n.id === sourceId);
    if (sourceNode && !sourceNode.nextNodes.includes(targetId)) {
      sourceNode.nextNodes.push(targetId);
    }
    
    this.renderProcess();
    this.processDefinitionChange.emit(this.processDefinition);
  }

  openNodeConfig(node: ProcessNode) {
    // 在实际应用中，这里应该打开一个配置对话框
    console.log('配置节点:', node);
  }

  private clearCanvas() {
    while (this.canvas.firstChild) {
      this.canvas.removeChild(this.canvas.firstChild);
    }
  }

  // 工具函数
  allowDrop(event: DragEvent) {
    event.preventDefault();
  }

  drag(event: DragEvent, nodeType: NodeType) {
    event.dataTransfer?.setData('nodeType', nodeType);
  }

  addApprover(){
    
  }

  addCondition(){
    
  }
}