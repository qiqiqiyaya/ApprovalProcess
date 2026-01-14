export interface FlowNode {
  id: string;
  type: FlowNodeType;
  label: string;
  x: number;
  y: number;
  width?: number;
  height?: number;
  shape?: string;
  config?: any;
}

export type FlowNodeType = 
  | 'start' 
  | 'end' 
  | 'task' 
  | 'decision' 
  | 'parallel' 
  | 'merge' 
  | 'custom';

export interface FlowEdge {
  id: string;
  source: string;
  target: string;
  vertices?: Array<{x: number, y: number}>;
  label?: string;
}

export interface FlowGraphData {
  nodes: FlowNode[];
  edges: FlowEdge[];
}

export interface BranchInfo {
  type: 'parallel' | 'merge';
  branches?: number;
  startNode?: string;
  depth?: number;
}