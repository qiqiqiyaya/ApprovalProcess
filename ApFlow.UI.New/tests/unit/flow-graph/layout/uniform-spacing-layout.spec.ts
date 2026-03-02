import { describe, it, expect, beforeEach } from 'vitest';
import { UniformSpacingLayout } from '../../../../src/app/pages/flow-graph/services/layout/uniform-spacing-layout';
import { FlowNode } from '../../../../src/app/pages/flow-graph/models/flow-node';
import { FlowEdge } from '../../../../src/app/pages/flow-graph/models/flow-edge';
import { LayoutConfig } from '../../../../src/app/pages/flow-graph/models/layout-config';

describe('UniformSpacingLayout', () => {
  let layout: UniformSpacingLayout;
  let nodes: FlowNode[];
  let edges: FlowEdge[];
  let config: LayoutConfig;

  beforeEach(() => {
    layout = new UniformSpacingLayout();
    config = {
      verticalSpacing: 50,
      horizontalSpacing: 100,
      animate: true,
      animationDuration: 300,
    };
  });

  describe('Layer Assignment (T020)', () => {
    it('should assign level 0 to root node', () => {
      nodes = [
        { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      ];
      edges = [];

      const result = layout.layout(nodes, edges, config);

      const node1 = result.nodes.find(n => n.id === '1');
      expect(node1?.level).toBe(0);
      expect(node1?.y).toBe(0);
    });

    it('should assign increasing levels for linear flow', () => {
      nodes = [
        { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '2', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '3', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: '1', target: '2' } as FlowEdge,
        { id: 'e2', source: '2', target: '3' } as FlowEdge,
      ];

      const result = layout.layout(nodes, edges, config);

      const node1 = result.nodes.find(n => n.id === '1');
      const node2 = result.nodes.find(n => n.id === '2');
      const node3 = result.nodes.find(n => n.id === '3');

      expect(node1?.level).toBe(0);
      expect(node2?.level).toBe(1);
      expect(node3?.level).toBe(2);
    });

    it('should handle multiple root nodes', () => {
      nodes = [
        { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '2', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '3', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: '1', target: '3' } as FlowEdge,
        { id: 'e2', source: '2', target: '3' } as FlowEdge,
      ];

      const result = layout.layout(nodes, edges, config);

      const node1 = result.nodes.find(n => n.id === '1');
      const node2 = result.nodes.find(n => n.id === '2');
      const node3 = result.nodes.find(n => n.id === '3');

      expect(node1?.level).toBe(0);
      expect(node2?.level).toBe(0);
      expect(node3?.level).toBe(1);
    });
  });

  describe('Node Ordering (T021)', () => {
    it('should maintain node order within same level', () => {
      nodes = [
        { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '2', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '3', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      ];
      edges = [];

      const result = layout.layout(nodes, edges, config);

      // All nodes should be at level 0 with increasing x positions
      const node1 = result.nodes.find(n => n.id === '1');
      const node2 = result.nodes.find(n => n.id === '2');
      const node3 = result.nodes.find(n => n.id === '3');

      expect(node1?.level).toBe(0);
      expect(node2?.level).toBe(0);
      expect(node3?.level).toBe(0);
      expect(node1?.x).toBe(0);
      expect(node2?.x).toBe(100);
      expect(node3?.x).toBe(200);
    });
  });

  describe('Uniform Vertical Spacing Calculation (T022)', () => {
    it('should calculate uniform vertical spacing of 50px', () => {
      nodes = [
        { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '2', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '3', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '4', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: '1', target: '2' } as FlowEdge,
        { id: 'e2', source: '2', target: '3' } as FlowEdge,
        { id: 'e3', source: '3', target: '4' } as FlowEdge,
      ];

      const result = layout.layout(nodes, edges, config);

      const node1 = result.nodes.find(n => n.id === '1');
      const node2 = result.nodes.find(n => n.id === '2');
      const node3 = result.nodes.find(n => n.id === '3');
      const node4 = result.nodes.find(n => n.id === '4');

      expect(node1?.y).toBe(0);
      expect(node2?.y).toBe(50);
      expect(node3?.y).toBe(100);
      expect(node4?.y).toBe(150);

      // Verify uniform spacing
      expect(node2!.y - node1!.y).toBe(50);
      expect(node3!.y - node2!.y).toBe(50);
      expect(node4!.y - node3!.y).toBe(50);
    });

    it('should respect custom vertical spacing', () => {
      const customConfig: LayoutConfig = {
        verticalSpacing: 80,
        horizontalSpacing: 100,
      };

      nodes = [
        { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '2', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '3', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: '1', target: '2' } as FlowEdge,
        { id: 'e2', source: '2', target: '3' } as FlowEdge,
      ];

      const result = layout.layout(nodes, edges, customConfig);

      const node1 = result.nodes.find(n => n.id === '1');
      const node2 = result.nodes.find(n => n.id === '2');
      const node3 = result.nodes.find(n => n.id === '3');

      expect(node1?.y).toBe(0);
      expect(node2?.y).toBe(80);
      expect(node3?.y).toBe(160);

      // Verify custom spacing
      expect(node2!.y - node1!.y).toBe(80);
      expect(node3!.y - node2!.y).toBe(80);
    });
  });

  describe('Simple Linear Flow Graph (T023)', () => {
    it('should layout simple linear flow: start -> operation -> approve -> end', () => {
      nodes = [
        { id: 'start', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: 'operation', x: 0, y: 0, width: 120, height: 60 } as FlowNode,
        { id: 'approve', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: 'end', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: 'start', target: 'operation' } as FlowEdge,
        { id: 'e2', source: 'operation', target: 'approve' } as FlowEdge,
        { id: 'e3', source: 'approve', target: 'end' } as FlowEdge,
      ];

      const result = layout.layout(nodes, edges, config);

      // Verify all nodes are present
      expect(result.nodes).toHaveLength(4);

      // Verify uniform 50px spacing regardless of node heights
      const startNode = result.nodes.find(n => n.id === 'start');
      const operationNode = result.nodes.find(n => n.id === 'operation');
      const approveNode = result.nodes.find(n => n.id === 'approve');
      const endNode = result.nodes.find(n => n.id === 'end');

      expect(startNode?.y).toBe(0);
      expect(operationNode?.y).toBe(50);
      expect(approveNode?.y).toBe(100);
      expect(endNode?.y).toBe(150);

      // Verify spacing is exactly 50px between all adjacent nodes
      expect(operationNode!.y - startNode!.y).toBe(50);
      expect(approveNode!.y - operationNode!.y).toBe(50);
      expect(endNode!.y - approveNode!.y).toBe(50);

      // Verify metadata
      expect(result.metadata.totalLevels).toBe(4);
      expect(result.metadata.nodeCount).toBe(4);
      expect(result.metadata.edgeCount).toBe(3);
      expect(result.metadata.calculationTime).toBeGreaterThan(0);
    });
  });

  describe('Edge Cases', () => {
    it('should handle empty graph', () => {
      nodes = [];
      edges = [];

      const result = layout.layout(nodes, edges, config);

      expect(result.nodes).toHaveLength(0);
      expect(result.metadata.nodeCount).toBe(0);
      expect(result.metadata.edgeCount).toBe(0);
    });

    it('should handle single node graph', () => {
      nodes = [
        { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      ];
      edges = [];

      const result = layout.layout(nodes, edges, config);

      expect(result.nodes).toHaveLength(1);
      const node = result.nodes[0];
      expect(node.id).toBe('1');
      expect(node.level).toBe(0);
      expect(node.y).toBe(0);
      expect(node.x).toBe(0);
    });
  });

  // T040-T043: Tests for User Story 2 - Parallel Branch Support
  describe('User Story 2: Parallel Branch Support', () => {
    it('should align parallel branches vertically with consistent spacing', () => {
      // Create flow with parallel branch: Start → [Op1, Op2] → Approve → End
      nodes = [
        { id: 'start', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'op1', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'op2', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'approve', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: 'end', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: 'start', target: 'op1' } as FlowEdge,
        { id: 'e2', source: 'start', target: 'op2' } as FlowEdge,
        { id: 'e3', source: 'op1', target: 'approve' } as FlowEdge,
        { id: 'e4', source: 'op2', target: 'approve' } as FlowEdge,
        { id: 'e5', source: 'approve', target: 'end' } as FlowEdge,
      ];

      const result = layout.layout(nodes, edges, config);

      expect(result.nodes).toHaveLength(5);

      const startNode = result.nodes.find(n => n.id === 'start');
      const op1Node = result.nodes.find(n => n.id === 'op1');
      const op2Node = result.nodes.find(n => n.id === 'op2');
      const approveNode = result.nodes.find(n => n.id === 'approve');
      const endNode = result.nodes.find(n => n.id === 'end');

      // Level 0: Start
      expect(startNode!.level).toBe(0);
      expect(startNode!.y).toBe(0);

      // Level 1: Op1 and Op2 should be on same level (parallel branch)
      expect(op1Node!.level).toBe(1);
      expect(op2Node!.level).toBe(1);
      expect(op1Node!.y).toBe(50); // 50px from start
      expect(op2Node!.y).toBe(50);

      // Level 2: Approve
      expect(approveNode!.level).toBe(2);
      expect(approveNode!.y).toBe(100); // 50px from level 1

      // Level 3: End
      expect(endNode!.level).toBe(3);
      expect(endNode!.y).toBe(150); // 50px from level 2

      // Verify uniform vertical spacing between all levels
      expect(op1Node!.y - startNode!.y).toBe(50);
      expect(approveNode!.y - op1Node!.y).toBe(50);
      expect(endNode!.y - approveNode!.y).toBe(50);
    });

    it('should handle nested parallel branches', () => {
      // Create flow with nested parallel branches:
      // Start → [Op1, Op2 → [Op2a, Op2b]] → Approve → End
      nodes = [
        { id: 'start', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'op1', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'op2', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'op2a', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'op2b', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'approve', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: 'end', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: 'start', target: 'op1' } as FlowEdge,
        { id: 'e2', source: 'start', target: 'op2' } as FlowEdge,
        { id: 'e3', source: 'op2', target: 'op2a' } as FlowEdge,
        { id: 'e4', source: 'op2', target: 'op2b' } as FlowEdge,
        { id: 'e5', source: 'op1', target: 'approve' } as FlowEdge,
        { id: 'e6', source: 'op2a', target: 'approve' } as FlowEdge,
        { id: 'e7', source: 'op2b', target: 'approve' } as FlowEdge,
        { id: 'e8', source: 'approve', target: 'end' } as FlowEdge,
      ];

      const result = layout.layout(nodes, edges, config);

      expect(result.nodes).toHaveLength(7);

      const startNode = result.nodes.find(n => n.id === 'start');
      const op1Node = result.nodes.find(n => n.id === 'op1');
      const op2Node = result.nodes.find(n => n.id === 'op2');
      const op2aNode = result.nodes.find(n => n.id === 'op2a');
      const op2bNode = result.nodes.find(n => n.id === 'op2b');
      const approveNode = result.nodes.find(n => n.id === 'approve');
      const endNode = result.nodes.find(n => n.id === 'end');

      // Level 0: Start
      expect(startNode!.level).toBe(0);

      // Level 1: Op1 and Op2 (first parallel branch)
      expect(op1Node!.level).toBe(1);
      expect(op2Node!.level).toBe(1);

      // Level 2: Op2a and Op2b (nested parallel branch from Op2)
      expect(op2aNode!.level).toBe(2);
      expect(op2bNode!.level).toBe(2);

      // Level 3: Approve (converges from Op1, Op2a, Op2b)
      expect(approveNode!.level).toBe(3);

      // Level 4: End
      expect(endNode!.level).toBe(4);

      // Verify uniform spacing between all levels
      expect(op1Node!.y - startNode!.y).toBe(50);
      expect(op2aNode!.y - op2Node!.y).toBe(50);
      expect(approveNode!.y - op2aNode!.y).toBe(50);
      expect(endNode!.y - approveNode!.y).toBe(50);
    });

    it('should handle mixed node sizes while maintaining uniform spacing', () => {
      // Create flow with mixed node sizes: small (80×40), medium (100×50), large (200×100)
      nodes = [
        { id: 'start', x: 0, y: 0, width: 80, height: 40 } as FlowNode,    // small
        { id: 'op1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,     // medium
        { id: 'op2', x: 0, y: 0, width: 200, height: 100 } as FlowNode,    // large
        { id: 'approve', x: 0, y: 0, width: 80, height: 40 } as FlowNode,  // small
        { id: 'end', x: 0, y: 0, width: 100, height: 50 } as FlowNode,      // medium
      ];
      edges = [
        { id: 'e1', source: 'start', target: 'op1' } as FlowEdge,
        { id: 'e2', source: 'op1', target: 'op2' } as FlowEdge,
        { id: 'e3', source: 'op2', target: 'approve' } as FlowEdge,
        { id: 'e4', source: 'approve', target: 'end' } as FlowEdge,
      ];

      const result = layout.layout(nodes, edges, config);

      expect(result.nodes).toHaveLength(5);

      const startNode = result.nodes.find(n => n.id === 'start');
      const op1Node = result.nodes.find(n => n.id === 'op1');
      const op2Node = result.nodes.find(n => n.id === 'op2');
      const approveNode = result.nodes.find(n => n.id === 'approve');
      const endNode = result.nodes.find(n => n.id === 'end');

      // Verify all adjacent levels have 50px spacing regardless of node size
      expect(op1Node!.y - startNode!.y).toBe(50);
      expect(op2Node!.y - op1Node!.y).toBe(50);
      expect(approveNode!.y - op2Node!.y).toBe(50);
      expect(endNode!.y - approveNode!.y).toBe(50);

      // Verify nodes are at correct levels
      expect(startNode!.level).toBe(0);
      expect(op1Node!.level).toBe(1);
      expect(op2Node!.level).toBe(2);
      expect(approveNode!.level).toBe(3);
      expect(endNode!.level).toBe(4);
    });

    it('should handle empty branch gracefully', () => {
      // Create flow with potential parallel branch but only one branch has nodes
      nodes = [
        { id: 'start', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'op1', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
        { id: 'end', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: 'start', target: 'op1' } as FlowEdge,
        { id: 'e2', source: 'op1', target: 'end' } as FlowEdge,
      ];

      const result = layout.layout(nodes, edges, config);

      expect(result.nodes).toHaveLength(3);

      // Should still layout correctly with linear flow
      const startNode = result.nodes.find(n => n.id === 'start');
      const op1Node = result.nodes.find(n => n.id === 'op1');
      const endNode = result.nodes.find(n => n.id === 'end');

      expect(startNode!.level).toBe(0);
      expect(op1Node!.level).toBe(1);
      expect(endNode!.level).toBe(2);

      expect(op1Node!.y - startNode!.y).toBe(50);
      expect(endNode!.y - op1Node!.y).toBe(50);
    });
  });

  describe('Performance Benchmarking (T084)', () => {
    it('should calculate layout for 100 nodes in less than 100ms', () => {
      // Create 100 nodes in a complex graph structure
      const nodeCount = 100;
      const nodeIds = Array.from({ length: nodeCount }, (_, i) => `node${i}`);
      nodes = nodeIds.map(id => ({
        id,
        x: 0,
        y: 0,
        width: 100,
        height: 50,
      } as FlowNode));

      // Create a tree-like structure with some cross-connections
      edges = [];
      for (let i = 0; i < nodeCount - 1; i++) {
        const parentIndex = Math.floor(i / 3); // ~3 children per parent
        edges.push({
          id: `edge${i}`,
          source: `node${parentIndex}`,
          target: nodeIds[i + 1],
        } as FlowEdge);
      }

      // Add some cross-connections for complexity
      edges.push({ id: 'cross1', source: 'node10', target: 'node30' } as FlowEdge);
      edges.push({ id: 'cross2', source: 'node20', target: 'node50' } as FlowEdge);

      // Measure layout calculation time
      const startTime = performance.now();
      const result = layout.layout(nodes, edges, config);
      const calculationTime = performance.now() - startTime;

      console.log(`Layout calculation for ${nodeCount} nodes: ${calculationTime.toFixed(2)}ms`);

      expect(result.nodes).toHaveLength(nodeCount);
      expect(calculationTime).toBeLessThan(100); // T084: Target <100ms

      // Verify layout was successful
      expect(result.metadata.nodeCount).toBe(nodeCount);
      expect(result.metadata.calculationTime).toBeGreaterThan(0);
    });

    it('should scale efficiently with graph complexity', () => {
      const sizes = [10, 50, 100];
      const times: number[] = [];

      sizes.forEach(size => {
        const testNodes = Array.from({ length: size }, (_, i) => ({
          id: `node${i}`,
          x: 0,
          y: 0,
          width: 100,
          height: 50,
        } as FlowNode));

        // Create linear chain for consistent measurement
        const testEdges = Array.from({ length: size - 1 }, (_, i) => ({
          id: `edge${i}`,
          source: `node${i}`,
          target: `node${i + 1}`,
        } as FlowEdge));

        const startTime = performance.now();
        layout.layout(testNodes, testEdges, config);
        times.push(performance.now() - startTime);
      });

      console.log('Performance scaling:', {
        '10 nodes': times[0].toFixed(2) + 'ms',
        '50 nodes': times[1].toFixed(2) + 'ms',
        '100 nodes': times[2].toFixed(2) + 'ms',
      });

      // Verify performance scales roughly linearly (not exponentially)
      // 100 nodes should take less than 10x time of 10 nodes
      expect(times[2]).toBeLessThan(times[0] * 10);
    });
  });

  describe('Error Handling (T077, T078)', () => {
    it('should throw error for circular dependencies (T077)', () => {
      nodes = [
        { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '2', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '3', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: '1', target: '2' } as FlowEdge,
        { id: 'e2', source: '2', target: '3' } as FlowEdge,
        { id: 'e3', source: '3', target: '1' } as FlowEdge, // Circular!
      ];

      expect(() => layout.layout(nodes, edges, config)).toThrow('Circular dependency');
    });

    it('should handle empty graph gracefully (T078)', () => {
      nodes = [];
      edges = [];

      const result = layout.layout(nodes, edges, config);

      expect(result.nodes).toHaveLength(0);
      expect(result.metadata.nodeCount).toBe(0);
      expect(result.metadata.totalLevels).toBe(0);
    });
  });

  describe('Multiple Subgraph Detection (T082)', () => {
    it('should detect multiple disconnected subgraphs', () => {
      nodes = [
        // Subgraph 1
        { id: '1', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '2', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        // Subgraph 2 (disconnected)
        { id: '3', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
        { id: '4', x: 0, y: 0, width: 100, height: 50 } as FlowNode,
      ];
      edges = [
        { id: 'e1', source: '1', target: '2' } as FlowEdge,
        { id: 'e2', source: '3', target: '4' } as FlowEdge,
      ];

      // Should still layout successfully, but with warning
      const result = layout.layout(nodes, edges, config);

      expect(result.nodes).toHaveLength(4);
      expect(result.metadata.nodeCount).toBe(4);
    });
  });
});
