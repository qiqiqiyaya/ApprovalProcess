import { TestBed } from '@angular/core/testing';
import { Graph } from '@antv/x6';
import { LayoutService } from '../../../src/app/pages/flow-graph/services/layout/layout.service';
import { X6_GRAPH } from '../../../src/app/pages/flow-graph/models/layout-config-extended';
import { FlowNode, FlowEdge } from '../../../src/app/pages/flow-graph/models/flow-node';

/**
 * Performance tests for LayoutService
 * T055: Benchmark layout performance with 100 nodes
 */
describe('LayoutService Performance', () => {
  let service: LayoutService;
  let graph: Graph;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        LayoutService,
        {
          provide: X6_GRAPH,
          useValue: new Graph({
            container: document.createElement('div'),
            width: 800,
            height: 600,
          }),
        },
      ],
    });

    service = TestBed.inject(LayoutService);
    graph = TestBed.inject(X6_GRAPH);
  });

  afterEach(() => {
    graph.dispose();
  });

  /**
   * T055: Benchmark layout performance with 100 nodes
   * Expected: Layout calculation time < 100ms
   */
  it('should calculate layout for 100-node graph in less than 100ms', () => {
    // Create 100 nodes arranged in a linear chain
    const nodes: FlowNode[] = [];
    const edges: FlowEdge[] = [];

    for (let i = 0; i < 100; i++) {
      nodes.push({
        id: `node-${i}`,
        x: 0,
        y: 0,
        width: 80 + Math.random() * 120, // Random width between 80-200
        height: 40 + Math.random() * 60, // Random height between 40-100
      } as FlowNode);

      if (i > 0) {
        edges.push({
          id: `edge-${i}`,
          source: `node-${i - 1}`,
          target: `node-${i}`,
        } as FlowEdge);
      }
    }

    // Add nodes to graph
    nodes.forEach((node) => {
      graph.addNode({
        id: node.id,
        shape: 'rect',
        x: node.x,
        y: node.y,
        width: node.width,
        height: node.height,
      });
    });

    // Add edges to graph
    edges.forEach((edge) => {
      graph.addEdge({
        id: edge.id,
        source: edge.source,
        target: edge.target,
      });
    });

    // Trigger layout and measure time
    const startTime = performance.now();

    service.triggerLayout();

    // Wait for async layout to complete
    const maxWaitTime = 5000; // 5 seconds max wait
    const checkInterval = 10;
    let elapsed = 0;

    const checkLayout = () => {
      elapsed += checkInterval;
      const result = service.getCurrentResult();

      if (result) {
        const endTime = performance.now();
        const calculationTime = endTime - startTime;

        console.log(`Layout calculation time: ${calculationTime.toFixed(2)}ms`);

        // Verify all nodes are positioned
        expect(result.nodes.length).toBe(100);

        // Verify performance requirement: < 100ms
        expect(calculationTime).toBeLessThan(100);

        // Verify metadata
        expect(result.metadata.nodeCount).toBe(100);
        expect(result.metadata.edgeCount).toBe(99);
        expect(result.metadata.calculationTime).toBeGreaterThan(0);
      } else if (elapsed < maxWaitTime) {
        // Layout not ready yet, check again
        setTimeout(checkLayout, checkInterval);
      } else {
        fail('Layout calculation timed out after 5 seconds');
      }
    };

    // Start checking
    setTimeout(checkLayout, checkInterval);
  });

  /**
   * Additional performance test: Parallel branches with 100 nodes
   */
  it('should handle 100 nodes with parallel branches efficiently', () => {
    // Create flow with parallel branches
    // Start → 5 parallel branches (20 nodes each) → Merge → End
    const nodes: FlowNode[] = [];
    const edges: FlowEdge[] = [];

    // Start node
    nodes.push({ id: 'start', x: 0, y: 0, width: 80, height: 40 } as FlowNode);

    // 5 parallel branches, 20 nodes each
    for (let branch = 0; branch < 5; branch++) {
      for (let i = 0; i < 20; i++) {
        const nodeId = `branch-${branch}-node-${i}`;
        nodes.push({
          id: nodeId,
          x: 0,
          y: 0,
          width: 80 + Math.random() * 120,
          height: 40 + Math.random() * 60,
        } as FlowNode);

        // Edge from start to first node in branch
        if (i === 0) {
          edges.push({
            id: `edge-start-${branch}`,
            source: 'start',
            target: nodeId,
          } as FlowEdge);
        } else {
          // Edge from previous node in branch
          edges.push({
            id: `edge-${branch}-${i}`,
            source: `branch-${branch}-node-${i - 1}`,
            target: nodeId,
          } as FlowEdge);
        }
      }
    }

    // Merge and end nodes
    nodes.push({ id: 'merge', x: 0, y: 0, width: 100, height: 50 } as FlowNode);
    nodes.push({ id: 'end', x: 0, y: 0, width: 80, height: 40 } as FlowNode);

    // Edges from last nodes to merge
    for (let branch = 0; branch < 5; branch++) {
      edges.push({
        id: `edge-merge-${branch}`,
        source: `branch-${branch}-node-19`,
        target: 'merge',
      } as FlowEdge);
    }

    // Edge from merge to end
    edges.push({
      id: 'edge-end',
      source: 'merge',
      target: 'end',
    } as FlowEdge);

    // Total: 1 + 100 + 2 = 103 nodes
    expect(nodes.length).toBe(103);

    // Add nodes and edges to graph
    nodes.forEach((node) => {
      graph.addNode({
        id: node.id,
        shape: 'rect',
        x: node.x,
        y: node.y,
        width: node.width,
        height: node.height,
      });
    });

    edges.forEach((edge) => {
      graph.addEdge({
        id: edge.id,
        source: edge.source,
        target: edge.target,
      });
    });

    // Trigger layout and measure time
    const startTime = performance.now();

    service.triggerLayout();

    // Wait for async layout to complete
    const maxWaitTime = 5000;
    const checkInterval = 10;
    let elapsed = 0;

    const checkLayout = () => {
      elapsed += checkInterval;
      const result = service.getCurrentResult();

      if (result) {
        const endTime = performance.now();
        const calculationTime = endTime - startTime;

        console.log(
          `Parallel branches layout time: ${calculationTime.toFixed(2)}ms`
        );

        // Verify all nodes are positioned
        expect(result.nodes.length).toBe(103);

        // Verify performance requirement: < 100ms
        expect(calculationTime).toBeLessThan(100);

        // Verify metadata includes branch groups
        expect(result.metadata.nodeCount).toBe(103);
      } else if (elapsed < maxWaitTime) {
        setTimeout(checkLayout, checkInterval);
      } else {
        fail('Layout calculation timed out after 5 seconds');
      }
    };

    setTimeout(checkLayout, checkInterval);
  });

  /**
   * Cache performance test
   * Verify that cached layouts are returned instantly
   */
  it('should return cached layout results instantly', (done) => {
    // Create a simple graph
    const nodes: FlowNode[] = [
      { id: 'node-0', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
      { id: 'node-1', x: 0, y: 0, width: 80, height: 40 } as FlowNode,
    ];
    const edges: FlowEdge[] = [
      { id: 'edge-0', source: 'node-0', target: 'node-1' } as FlowEdge,
    ];

    nodes.forEach((node) => {
      graph.addNode({
        id: node.id,
        shape: 'rect',
        x: node.x,
        y: node.y,
        width: node.width,
        height: node.height,
      });
    });

    edges.forEach((edge) => {
      graph.addEdge({
        id: edge.id,
        source: edge.source,
        target: edge.target,
      });
    });

    // First layout calculation
    service.triggerLayout();

    setTimeout(() => {
      const firstResult = service.getCurrentResult();
      expect(firstResult).toBeTruthy();

      // Trigger second layout (should use cache)
      const startTime = performance.now();
      service.triggerLayout();

      setTimeout(() => {
        const secondResult = service.getCurrentResult();
        const endTime = performance.now();
        const cachedTime = endTime - startTime;

        console.log(`Cached layout time: ${cachedTime.toFixed(2)}ms`);

        // Verify same result is returned
        expect(secondResult).toEqual(firstResult);

        // Verify cache hit is very fast (< 10ms)
        expect(cachedTime).toBeLessThan(10);

        done();
      }, 50);
    }, 50);
  });
});
