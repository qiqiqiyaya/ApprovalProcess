/**
 * Unit Tests for FlowLayoutEngine
 *
 * Comprehensive test suite for the custom layout engine service.
 * Target coverage: 95%+
 *
 * @module FlowLayoutEngine.spec
 * @version 1.0.0
 * @since 2026-03-04
 */

import { FlowLayoutEngine } from './flow-layout-engine.service';
import { FlowGraph } from '../models/flow-graph';
import { FlowNode } from '../models/flow-node';
import { FlowEdge } from '../models/flow-edge';
import { LayoutError, LayoutErrorCode } from '../models/layout.models';

describe('FlowLayoutEngine', () => {
  let layoutEngine: FlowLayoutEngine;

  beforeEach(() => {
    layoutEngine = new FlowLayoutEngine();
  });

  describe('layout()', () => {
    it('should calculate layout for valid linear graph (3 nodes in sequence)', () => {
      // Arrange: Create a linear graph: start -> operation -> end
      const node1 = new FlowNode('rect', { width: 80, height: 40 });
      const node2 = new FlowNode('rect', { width: 80, height: 40 });
      const node3 = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph(
        [node1, node2, node3],
        [new FlowEdge(node1.id, node2.id), new FlowEdge(node2.id, node3.id)],
      );

      // Act
      const result = layoutEngine.layout(graph);

      // Assert
      expect(result.nodePositions.size).toBe(3);
      expect(result.levels.length).toBe(3);
      expect(result.maxLevel).toBe(2);

      // Verify vertical spacing is exactly 50px
      const pos1 = result.nodePositions.get(node1.id)!;
      const pos2 = result.nodePositions.get(node2.id)!;
      const pos3 = result.nodePositions.get(node3.id)!;

      expect(pos2.layoutY - pos1.layoutY).toBe(50);
      expect(pos3.layoutY - pos2.layoutY).toBe(50);

      // Verify levels
      expect(pos1.level).toBe(0);
      expect(pos2.level).toBe(1);
      expect(pos3.level).toBe(2);
    });

    it('should calculate layout for parallel branches (2 parallel branches)', () => {
      // Arrange: Create graph with parallel branches
      const root = new FlowNode('rect', { width: 80, height: 40 });
      const branch1 = new FlowNode('rect', { width: 80, height: 40 });
      const branch2 = new FlowNode('rect', { width: 80, height: 40 });
      const merge = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph(
        [root, branch1, branch2, merge],
        [
          new FlowEdge(root.id, branch1.id),
          new FlowEdge(root.id, branch2.id),
          new FlowEdge(branch1.id, merge.id),
          new FlowEdge(branch2.id, merge.id),
        ],
      );

      // Act
      const result = layoutEngine.layout(graph);

      // Assert
      expect(result.nodePositions.size).toBe(4);
      expect(result.levels.length).toBe(3);

      // Root at level 0
      const posRoot = result.nodePositions.get(root.id)!;
      expect(posRoot.level).toBe(0);

      // Both branches at level 1
      const posBranch1 = result.nodePositions.get(branch1.id)!;
      const posBranch2 = result.nodePositions.get(branch2.id)!;
      expect(posBranch1.level).toBe(1);
      expect(posBranch2.level).toBe(1);

      // Merge at level 2
      const posMerge = result.nodePositions.get(merge.id)!;
      expect(posMerge.level).toBe(2);

      // Verify vertical spacing
      expect(posBranch1.layoutY - posRoot.layoutY).toBe(50);
      expect(posMerge.layoutY - posBranch1.layoutY).toBe(50);
    });

    it('should throw LayoutError with INVALID_GRAPH code for null graph', () => {
      // Act & Assert
      expect(() => layoutEngine.layout(null as any)).toThrowError(LayoutError);
      try {
        layoutEngine.layout(null as any);
      } catch (error) {
        expect((error as LayoutError).code).toBe(LayoutErrorCode.INVALID_GRAPH);
      }
    });

    it('should throw LayoutError with INVALID_GRAPH code for empty graph', () => {
      // Arrange
      const graph = new FlowGraph([], []);

      // Act & Assert
      expect(() => layoutEngine.layout(graph)).toThrowError(LayoutError);
      try {
        layoutEngine.layout(graph);
      } catch (error) {
        expect((error as LayoutError).code).toBe(LayoutErrorCode.INVALID_GRAPH);
      }
    });

    it('should throw LayoutError with CYCLE_DETECTED code for cyclic graph', () => {
      // Arrange: Create graph with cycle: A -> B -> C -> A
      const nodeA = new FlowNode('rect', { width: 80, height: 40 });
      const nodeB = new FlowNode('rect', { width: 80, height: 40 });
      const nodeC = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph(
        [nodeA, nodeB, nodeC],
        [
          new FlowEdge(nodeA.id, nodeB.id),
          new FlowEdge(nodeB.id, nodeC.id),
          new FlowEdge(nodeC.id, nodeA.id),
        ],
      );

      // Act & Assert
      expect(() => layoutEngine.layout(graph)).toThrowError(LayoutError);
      try {
        layoutEngine.layout(graph);
      } catch (error) {
        expect((error as LayoutError).code).toBe(LayoutErrorCode.CYCLE_DETECTED);
      }
    });
  });

  describe('assignLevels()', () => {
    it('should assign levels for simple graph (root + 2 children)', () => {
      // Arrange
      const root = new FlowNode('rect', { width: 80, height: 40 });
      const child1 = new FlowNode('rect', { width: 80, height: 40 });
      const child2 = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph(
        [root, child1, child2],
        [new FlowEdge(root.id, child1.id), new FlowEdge(root.id, child2.id)],
      );

      // Act
      const levels = layoutEngine.assignLevels(graph);

      // Assert
      expect(levels.size).toBe(3);
      expect(levels.get(root.id)).toBe(0);
      expect(levels.get(child1.id)).toBe(1);
      expect(levels.get(child2.id)).toBe(1);
    });

    it('should assign levels for parallel branches (all branch nodes at same level)', () => {
      // Arrange
      const root = new FlowNode('rect', { width: 80, height: 40 });
      const branch1 = new FlowNode('rect', { width: 80, height: 40 });
      const branch2 = new FlowNode('rect', { width: 80, height: 40 });
      const merge = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph(
        [root, branch1, branch2, merge],
        [
          new FlowEdge(root.id, branch1.id),
          new FlowEdge(root.id, branch2.id),
          new FlowEdge(branch1.id, merge.id),
          new FlowEdge(branch2.id, merge.id),
        ],
      );

      // Act
      const levels = layoutEngine.assignLevels(graph);

      // Assert
      expect(levels.get(root.id)).toBe(0);
      expect(levels.get(branch1.id)).toBe(1);
      expect(levels.get(branch2.id)).toBe(1);
      expect(levels.get(merge.id)).toBe(2);
    });

    it('should throw LayoutError with CYCLE_DETECTED code for cyclic graph', () => {
      // Arrange
      const nodeA = new FlowNode('rect', { width: 80, height: 40 });
      const nodeB = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph(
        [nodeA, nodeB],
        [new FlowEdge(nodeA.id, nodeB.id), new FlowEdge(nodeB.id, nodeA.id)],
      );

      // Act & Assert
      expect(() => layoutEngine.assignLevels(graph)).toThrowError(LayoutError);
      try {
        layoutEngine.assignLevels(graph);
      } catch (error) {
        expect((error as LayoutError).code).toBe(LayoutErrorCode.CYCLE_DETECTED);
      }
    });
  });

  describe('calculatePositions()', () => {
    it('should calculate positions with nodes of varying heights (20px, 100px, 200px)', () => {
      // Arrange
      const node1 = new FlowNode('rect', { width: 80, height: 20 });
      const node2 = new FlowNode('rect', { width: 80, height: 100 });
      const node3 = new FlowNode('rect', { width: 80, height: 200 });
      const graph = new FlowGraph(
        [node1, node2, node3],
        [new FlowEdge(node1.id, node2.id), new FlowEdge(node2.id, node3.id)],
      );

      const levels = new Map<string, number>();
      levels.set(node1.id, 0);
      levels.set(node2.id, 1);
      levels.set(node3.id, 2);

      const config = { verticalSpacing: 50, horizontalSpacing: 75 };

      // Act
      const result = layoutEngine.calculatePositions(graph, levels, config);

      // Assert
      expect(result.nodePositions.size).toBe(3);

      const pos1 = result.nodePositions.get(node1.id)!;
      const pos2 = result.nodePositions.get(node2.id)!;
      const pos3 = result.nodePositions.get(node3.id)!;

      // Verify 50px vertical spacing regardless of node heights
      expect(pos2.layoutY - pos1.layoutY).toBe(50);
      expect(pos3.layoutY - pos2.layoutY).toBe(50);
    });

    it('should verify 50px vertical spacing between all consecutive levels', () => {
      // Arrange: Create 5-level graph
      const nodes = Array.from(
        { length: 5 },
        (_, i) => new FlowNode('rect', { width: 80, height: 40 }),
      );
      const edges = nodes.slice(0, -1).map((node, i) => new FlowEdge(node.id, nodes[i + 1].id));
      const graph = new FlowGraph(nodes, edges);

      const levels = new Map<string, number>();
      nodes.forEach((node, i) => levels.set(node.id, i));

      const config = { verticalSpacing: 50, horizontalSpacing: 75 };

      // Act
      const result = layoutEngine.calculatePositions(graph, levels, config);

      // Assert: Verify all consecutive levels have exactly 50px spacing
      for (let i = 0; i < nodes.length - 1; i++) {
        const pos1 = result.nodePositions.get(nodes[i].id)!;
        const pos2 = result.nodePositions.get(nodes[i + 1].id)!;
        expect(pos2.layoutY - pos1.layoutY).toBe(50);
      }
    });

    it('should verify centering offset (x = layoutX - width/2)', () => {
      // Arrange
      const node = new FlowNode('rect', { width: 160, height: 80 });
      const graph = new FlowGraph([node], []);
      const levels = new Map([[node.id, 0]]);
      const config = { verticalSpacing: 50, horizontalSpacing: 75, centerGraph: false };

      // Act
      const result = layoutEngine.calculatePositions(graph, levels, config);
      const pos = result.nodePositions.get(node.id)!;

      // Assert: centering offset calculation
      expect(pos.x).toBe(pos.layoutX - pos.width / 2);
      expect(pos.y).toBe(pos.layoutY - pos.height / 2);
    });

    it('should handle configuration options (horizontalSpacing, baseYOffset, centerGraph)', () => {
      // Arrange
      const node1 = new FlowNode('rect', { width: 80, height: 40 });
      const node2 = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph([node1, node2], [new FlowEdge(node1.id, node2.id)]);
      const levels = new Map([
        [node1.id, 0],
        [node2.id, 1],
      ]);
      const config = {
        verticalSpacing: 50,
        horizontalSpacing: 100, // Custom horizontal spacing
        baseYOffset: 20, // Custom Y offset
        centerGraph: true,
      };

      // Act
      const result = layoutEngine.calculatePositions(graph, levels, config);

      // Assert
      const pos1 = result.nodePositions.get(node1.id)!;
      const pos2 = result.nodePositions.get(node2.id)!;

      // Verify baseYOffset is applied
      expect(pos1.layoutY).toBe(20);
      expect(pos2.layoutY).toBe(70); // 20 + 50

      // Verify totalHeight includes baseYOffset
      expect(result.totalHeight).toBe(120); // (2 levels) * 50 + 20
    });
  });

  describe('Caching', () => {
    it('should return cached result on second call with same graph', () => {
      // Arrange
      const node = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph([node], []);

      // Act
      const result1 = layoutEngine.layout(graph);
      const result2 = layoutEngine.layout(graph);

      // Assert
      expect(result1).toBe(result2); // Same object reference
    });

    it('should invalidate cache for different graph structure', () => {
      // Arrange
      const node1 = new FlowNode('rect', { width: 80, height: 40 });
      const node2 = new FlowNode('rect', { width: 80, height: 40 });
      const graph1 = new FlowGraph([node1], []);
      const graph2 = new FlowGraph([node1, node2], [new FlowEdge(node1.id, node2.id)]);

      // Act
      const result1 = layoutEngine.layout(graph1);
      const result2 = layoutEngine.layout(graph2);

      // Assert
      expect(result1).not.toBe(result2); // Different objects
    });

    it('should clear cache when clearCache() is called', () => {
      // Arrange
      const node = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph([node], []);
      layoutEngine.layout(graph);

      // Act
      layoutEngine.clearCache();
      const stats = layoutEngine.getCacheStats();

      // Assert
      expect(stats.size).toBe(0);
    });

    it('should return correct cache stats', () => {
      // Arrange
      const node = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph([node], []);
      layoutEngine.layout(graph);

      // Act
      const stats = layoutEngine.getCacheStats();

      // Assert
      expect(stats.size).toBe(1);
      expect(stats.keys.length).toBe(1);
    });
  });

  describe('Error Handling', () => {
    it('should throw LayoutError with INVALID_NODE_SIZE code for invalid width', () => {
      // Arrange
      const node = new FlowNode('rect', { width: -10, height: 40 }); // Invalid width
      const graph = new FlowGraph([node], []);
      const levels = new Map([[node.id, 0]]);
      const config = { verticalSpacing: 50, horizontalSpacing: 75 };

      // Act & Assert
      expect(() => layoutEngine.calculatePositions(graph, levels, config)).toThrowError(
        LayoutError,
      );
      try {
        layoutEngine.calculatePositions(graph, levels, config);
      } catch (error) {
        expect((error as LayoutError).code).toBe(LayoutErrorCode.INVALID_NODE_SIZE);
      }
    });

    it('should throw LayoutError with INVALID_NODE_SIZE code for invalid height', () => {
      // Arrange
      const node = new FlowNode('rect', { width: 80, height: 0 }); // Invalid height
      const graph = new FlowGraph([node], []);
      const levels = new Map([[node.id, 0]]);
      const config = { verticalSpacing: 50, horizontalSpacing: 75 };

      // Act & Assert
      expect(() => layoutEngine.calculatePositions(graph, levels, config)).toThrowError(
        LayoutError,
      );
      try {
        layoutEngine.calculatePositions(graph, levels, config);
      } catch (error) {
        expect((error as LayoutError).code).toBe(LayoutErrorCode.INVALID_NODE_SIZE);
      }
    });
  });

  describe('Edge Cases', () => {
    it('should handle graph with single level (start and end nodes at same level)', () => {
      // Arrange
      const node1 = new FlowNode('rect', { width: 80, height: 40 });
      const node2 = new FlowNode('rect', { width: 80, height: 40 });
      const graph = new FlowGraph([node1, node2], []); // No edges

      // Act
      const result = layoutEngine.layout(graph);

      // Assert: Both nodes at level 0 (both are roots)
      const pos1 = result.nodePositions.get(node1.id)!;
      const pos2 = result.nodePositions.get(node2.id)!;
      expect(pos1.level).toBe(0);
      expect(pos2.level).toBe(0);
    });

    it('should handle deeply nested hierarchy (10+ levels)', () => {
      // Arrange: Create 10-level linear graph
      const nodes = Array.from(
        { length: 10 },
        (_, i) => new FlowNode('rect', { width: 80, height: 40 }),
      );
      const edges = nodes.slice(0, -1).map((node, i) => new FlowEdge(node.id, nodes[i + 1].id));
      const graph = new FlowGraph(nodes, edges);

      // Act
      const result = layoutEngine.layout(graph);

      // Assert
      expect(result.maxLevel).toBe(9);
      expect(result.totalHeight).toBe(500); // 10 levels * 50px

      // Verify all levels have 50px spacing
      for (let i = 0; i < nodes.length - 1; i++) {
        const pos1 = result.nodePositions.get(nodes[i].id)!;
        const pos2 = result.nodePositions.get(nodes[i + 1].id)!;
        expect(pos2.layoutY - pos1.layoutY).toBe(50);
      }
    });

    it('should handle large graph performance (100 nodes, verify < 100ms)', () => {
      // Arrange: Create 100-node linear graph
      const nodes = Array.from(
        { length: 100 },
        (_, i) => new FlowNode('rect', { width: 80, height: 40 }),
      );
      const edges = nodes.slice(0, -1).map((node, i) => new FlowEdge(node.id, nodes[i + 1].id));
      const graph = new FlowGraph(nodes, edges);

      // Act
      const startTime = performance.now();
      const result = layoutEngine.layout(graph);
      const endTime = performance.now();
      const duration = endTime - startTime;

      // Assert
      expect(result.nodePositions.size).toBe(100);
      expect(duration).toBeLessThan(100); // < 100ms
    });
  });
});
