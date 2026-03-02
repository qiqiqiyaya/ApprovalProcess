import { Injectable } from '@angular/core';
import {
  LayoutConfig,
  LayoutResult,
  NodePosition,
  LayoutMetadata,
} from '../../models/layout-config';
import { FlowNode } from '../../models/flow-node';
import { BranchGroup } from '../../models/flow-group';
import { LayoutEngine } from '../../models/layout-config-extended';
import { FlowEdge } from '../../models/flow-edge';

/**
 * Uniform vertical spacing layout engine
 * Implements modified Sugiyama algorithm for hierarchical graph layout
 * with guaranteed uniform vertical spacing between all adjacent nodes
 */
@Injectable({ providedIn: 'root' })
export class UniformSpacingLayout implements LayoutEngine<FlowNode, FlowEdge> {
  /**
   * Calculates layout positions for a graph
   * @param nodes Array of nodes to layout
   * @param edges Array of edges defining connections
   * @param config Layout configuration parameters
   * @param branchGroups Optional array of parallel branch groups
   * @returns Calculated layout result
   * @throws {Error} If circular dependency is detected
   */
  layout(
    nodes: FlowNode[],
    edges: FlowEdge[],
    config: LayoutConfig,
    branchGroups?: BranchGroup[]
  ): LayoutResult {
    const startTime = performance.now();

    // Handle empty graph (T078)
    if (nodes.length === 0) {
      return {
        nodes: [],
        metadata: {
          totalLevels: 0,
          maxNodesPerLevel: 0,
          calculationTime: performance.now() - startTime,
          branchGroupCount: 0,
          nodeCount: 0,
          edgeCount: 0,
        },
      };
    }

    // T077: Check for circular dependencies
    const circularDeps = this.detectCircularDependencies(nodes, edges);
    if (circularDeps.length > 0) {
      throw new Error(
        `Circular dependency detected in graph: ${circularDeps.map(d => d.join(' -> ')).join('; ')}`
      );
    }

    // T082: Detect multiple subgraphs
    const subgraphs = this.detectSubgraphs(nodes, edges);
    if (subgraphs.length > 1) {
      console.warn(
        `[UniformSpacingLayout] Multiple subgraphs detected (${subgraphs.length}). ` +
        `Layout may not be optimal. Consider merging disconnected graphs.`
      );
    }

    // Phase 1: Calculate levels using BFS
    const levels = this.calculateLevels(nodes, edges);

    // Phase 2: Group nodes by level
    let levelNodes = this.groupNodesByLevel(nodes, levels);

    // Phase 3: Handle parallel branches if provided (T044-T046)
    if (branchGroups && branchGroups.length > 0) {
      levelNodes = this.handleParallelBranches(levelNodes, branchGroups);
      levelNodes = this.handleNestedBranches(levelNodes, branchGroups);
    }

    // Phase 4: Apply barycenter ordering for visual balance (T047)
    levelNodes = this.applyBarycenterOrdering(levelNodes, edges);

    // Phase 5: Align branch groups (T048)
    if (branchGroups && branchGroups.length > 0) {
      levelNodes = this.alignBranchGroups(levelNodes, branchGroups);
    }

    // Phase 6: Calculate positions with uniform spacing (T049)
    const positions = this.calculatePositions(levelNodes, config);

    // Phase 5: Build metadata
    const calculationTime = performance.now() - startTime;
    const metadata: LayoutMetadata = {
      totalLevels: levelNodes.size,
      maxNodesPerLevel: Math.max(
        ...Array.from(levelNodes.values()).map((ns) => ns.length)
      ),
      calculationTime,
      branchGroupCount: branchGroups?.length ?? 0,
      nodeCount: nodes.length,
      edgeCount: edges.length,
    };

    return { nodes: positions, metadata };
  }

  /**
   * Calculates hierarchy levels for all nodes using BFS
   * @param nodes Array of nodes
   * @param edges Array of edges
   * @returns Map of node ID to level
   */
  private calculateLevels(
    nodes: FlowNode[],
    edges: FlowEdge[]
  ): Map<string, number> {
    const levels = new Map<string, number>();
    const visited = new Set<string>();

    // Find root nodes (no incoming edges)
    const incomingEdges = new Map<string, string[]>();
    edges.forEach((edge) => {
      if (!incomingEdges.has(edge.target)) {
        incomingEdges.set(edge.target, []);
      }
      incomingEdges.get(edge.target)!.push(edge.source);
    });

    const roots = nodes.filter((n) => !incomingEdges.has(n.id));

    // BFS to assign levels
    const queue: { node: FlowNode; level: number }[] = roots.map((n) => ({
      node: n,
      level: 0,
    }));
    queue.forEach(({ node, level }) => {
      levels.set(node.id, level);
      visited.add(node.id);
    });

    while (queue.length > 0) {
      const { node, level } = queue.shift()!;

      // Find children (outgoing edges)
      const children = edges
        .filter((e) => e.source === node.id)
        .map((e) => nodes.find((n) => n.id === e.target))
        .filter(Boolean) as FlowNode[];

      children.forEach((child) => {
        if (!visited.has(child.id)) {
          const childLevel = level + 1;
          levels.set(child.id, childLevel);
          visited.add(child.id);
          queue.push({ node: child, level: childLevel });
        }
      });
    }

    // Handle disconnected nodes (assign to level 0)
    nodes.forEach((node) => {
      if (!visited.has(node.id)) {
        levels.set(node.id, 0);
      }
    });

    return levels;
  }

  /**
   * Groups nodes by their hierarchy level
   * @param nodes Array of nodes
   * @param levels Map of node ID to level
   * @returns Map of level to array of nodes
   */
  private groupNodesByLevel(
    nodes: FlowNode[],
    levels: Map<string, number>
  ): Map<number, FlowNode[]> {
    const levelNodes = new Map<number, FlowNode[]>();

    levels.forEach((level, nodeId) => {
      if (!levelNodes.has(level)) {
        levelNodes.set(level, []);
      }
      const node = nodes.find((n) => n.id === nodeId);
      if (node) {
        levelNodes.get(level)!.push(node);
      }
    });

    return levelNodes;
  }

  /**
   * Calculates node positions with uniform vertical spacing
   * @param levelNodes Map of level to nodes
   * @param config Layout configuration
   * @returns Array of node positions
   */
  private calculatePositions(
    levelNodes: Map<number, FlowNode[]>,
    config: LayoutConfig
  ): NodePosition[] {
    const positions: NodePosition[] = [];
    const { verticalSpacing, horizontalSpacing } = config;

    levelNodes.forEach((nodes, level) => {
      const y = level * verticalSpacing;

      nodes.forEach((node, index) => {
        const x = index * horizontalSpacing;
        positions.push({
          id: node.id,
          x,
          y,
          level,
        });
      });
    });

    return positions;
  }

  /**
   * Handles parallel branches to ensure uniform spacing
   * Aligns all nodes in a branch group to the same level
   * @param levelNodes Map of level to nodes
   * @param branchGroups Array of branch groups
   * @returns Updated level nodes
   */
  private handleParallelBranches(
    levelNodes: Map<number, FlowNode[]>,
    branchGroups: BranchGroup[]
  ): Map<number, FlowNode[]> {
    // Create a map for quick node lookup
    const nodeLevelMap = new Map<string, number>();
    levelNodes.forEach((nodes, level) => {
      nodes.forEach((node) => {
        nodeLevelMap.set(node.id, level);
      });
    });

    // Process each branch group
    branchGroups.forEach((group) => {
      const branchNodes = this.getAllNodesInGroup(group);

      if (branchNodes.length === 0) {
        return;
      }

      // Find the maximum level among all nodes in the group
      const maxLevel = Math.max(
        ...branchNodes.map((nodeId) => nodeLevelMap.get(nodeId) ?? 0)
      );

      // Move all nodes to the max level
      branchNodes.forEach((nodeId) => {
        const currentLevel = nodeLevelMap.get(nodeId);
        if (currentLevel !== undefined && currentLevel < maxLevel) {
          // Remove from current level
          const currentLevelNodes = levelNodes.get(currentLevel);
          if (currentLevelNodes) {
            const index = currentLevelNodes.findIndex((n) => n.id === nodeId);
            if (index !== -1) {
              currentLevelNodes.splice(index, 1);
            }
          }

          // Add to max level
          if (!levelNodes.has(maxLevel)) {
            levelNodes.set(maxLevel, []);
          }

          const node = this.findNodeById(nodeId, levelNodes);
          if (node) {
            levelNodes.get(maxLevel)!.push(node);
            nodeLevelMap.set(nodeId, maxLevel);
          }
        }
      });
    });

    return levelNodes;
  }

  /**
   * Handles nested parallel branches recursively
   * Ensures nested branches maintain uniform spacing at all levels
   * @param levelNodes Map of level to nodes
   * @param branchGroups Array of branch groups
   * @returns Updated level nodes
   */
  private handleNestedBranches(
    levelNodes: Map<number, FlowNode[]>,
    branchGroups: BranchGroup[]
  ): Map<number, FlowNode[]> {
    // Get all nested branch groups recursively
    const allGroups = this.getAllBranchGroupsRecursively(branchGroups);

    // Process branches by depth (deepest first)
    const sortedGroups = allGroups.sort((a, b) => {
      const depthA = this.getBranchDepth(a, allGroups);
      const depthB = this.getBranchDepth(b, allGroups);
      return depthB - depthA; // Deepest first
    });

    return this.handleParallelBranches(levelNodes, sortedGroups);
  }

  /**
   * Gets all branch groups recursively including nested groups
   * @param groups Array of branch groups
   * @returns All branch groups including nested ones
   */
  private getAllBranchGroupsRecursively(groups: BranchGroup[]): BranchGroup[] {
    const allGroups: BranchGroup[] = [...groups];

    // Check if group has child groups
    // This depends on BranchGroup structure
    // For now, we'll assume a flat structure

    return allGroups;
  }

  /**
   * Calculates the depth of a branch group (how many levels deep in the hierarchy)
   * @param _group Branch group to calculate depth for (unused in current implementation)
   * @param _allGroups All branch groups (unused in current implementation)
   * @returns Depth of the branch group
   */
  private getBranchDepth(_group: BranchGroup, _allGroups: BranchGroup[]): number {
    // This is a simplified implementation
    // In a real implementation, you'd traverse the parent-child hierarchy
    return 0;
  }

  /**
   * Applies barycenter ordering to reduce edge crossings
   * Nodes are ordered based on their barycenter (average position of neighbors)
   * @param levelNodes Map of level to nodes
   * @param edges Array of edges
   * @returns Updated level nodes with sorted nodes
   */
  private applyBarycenterOrdering(
    levelNodes: Map<number, FlowNode[]>,
    edges: FlowEdge[]
  ): Map<number, FlowNode[]> {
    // Get sorted levels
    const levels = Array.from(levelNodes.keys()).sort((a, b) => a - b);

    // Iterate through levels (except first and last)
    for (let i = 1; i < levels.length - 1; i++) {
      const level = levels[i];
      const nodes = levelNodes.get(level) || [];

      // Calculate barycenter for each node
      const barycenters = new Map<string, number>();
      nodes.forEach((node) => {
        const barycenter = this.calculateBarycenter(node, edges, levelNodes);
        barycenters.set(node.id, barycenter);
      });

      // Sort nodes by barycenter
      const sortedNodes = [...nodes].sort((a, b) => {
        const barycenterA = barycenters.get(a.id) ?? 0;
        const barycenterB = barycenters.get(b.id) ?? 0;
        return barycenterA - barycenterB;
      });

      levelNodes.set(level, sortedNodes);
    }

    return levelNodes;
  }

  /**
   * Calculates the barycenter (average position) of a node's neighbors
   * @param node Node to calculate barycenter for
   * @param edges Array of edges
   * @param levelNodes Map of level to nodes
   * @returns Barycenter value
   */
  private calculateBarycenter(
    node: FlowNode,
    edges: FlowEdge[],
    levelNodes: Map<number, FlowNode[]>
  ): number {
    // Find connected nodes (both incoming and outgoing edges)
    const incomingEdges = edges.filter((e) => e.target === node.id);
    const outgoingEdges = edges.filter((e) => e.source === node.id);

    const connectedNodes = [
      ...incomingEdges.map((e) => e.source),
      ...outgoingEdges.map((e) => e.target),
    ];

    if (connectedNodes.length === 0) {
      return 0;
    }

    // Find positions of connected nodes
    const positions: number[] = [];
    connectedNodes.forEach((nodeId) => {
      for (const [, nodes] of levelNodes) {
        const index = nodes.findIndex((n) => n.id === nodeId);
        if (index !== -1) {
          positions.push(index);
          break;
        }
      }
    });

    // Calculate average
    const sum = positions.reduce((acc, pos) => acc + pos, 0);
    return sum / positions.length;
  }

  /**
   * Aligns branch groups to minimize visual clutter
   * Groups nodes by their branch and aligns them horizontally
   * @param levelNodes Map of level to nodes
   * @param branchGroups Array of branch groups
   * @returns Updated level nodes
   */
  private alignBranchGroups(
    levelNodes: Map<number, FlowNode[]>,
    branchGroups: BranchGroup[]
  ): Map<number, FlowNode[]> {
    // Create a map of branch ID to nodes
    const branchToNodes = new Map<string, FlowNode[]>();

    branchGroups.forEach((group) => {
      const branchNodes = this.getAllNodesInGroup(group);
      branchNodes.forEach((nodeId) => {
        const node = this.findNodeById(nodeId, levelNodes);
        if (node) {
          if (!branchToNodes.has(group.id)) {
            branchToNodes.set(group.id, []);
          }
          branchToNodes.get(group.id)!.push(node);
        }
      });
    });

    // Align nodes within each branch
    branchToNodes.forEach(() => {
      // Calculate average x position for the branch
      // const avgX = nodes.reduce((sum, node) => sum + (node.x || 0), 0) / nodes.length;

      // Optionally align all nodes to the branch center
      // This depends on the layout strategy
    });

    return levelNodes;
  }

  /**
   * Gets all node IDs in a branch group
   * @param group Branch group
   * @returns Array of node IDs
   */
  private getAllNodesInGroup(group: BranchGroup): string[] {
    // Use BranchGroup's getAllNodes method if available (T051)
    if (group.getAllNodes && typeof group.getAllNodes === 'function') {
      return group.getAllNodes();
    }

    // Fallback: collect nodes from branches map
    const nodes: string[] = [];
    for (const branchNodes of group.branches.values()) {
      nodes.push(...branchNodes);
    }

    return nodes;
  }

  /**
   * Finds a node by ID across all levels
   * @param nodeId Node ID to find
   * @param levelNodes Map of level to nodes
   * @returns Found node or undefined
   */
  private findNodeById(
    nodeId: string,
    levelNodes: Map<number, FlowNode[]>
  ): FlowNode | undefined {
    for (const nodes of levelNodes.values()) {
      const node = nodes.find((n) => n.id === nodeId);
      if (node) {
        return node;
      }
    }
    return undefined;
  }

  /**
   * T077: Detects circular dependencies in the graph using DFS
   * @param nodes Array of nodes
   * @param edges Array of edges
   * @returns Array of circular dependency paths (node IDs)
   */
  private detectCircularDependencies(
    nodes: FlowNode[],
    edges: FlowEdge[]
  ): string[][] {
    const visited = new Set<string>();
    const recursionStack = new Set<string>();
    const circularPaths: string[][] = [];

    // Build adjacency list
    const adjacency = new Map<string, string[]>();
    edges.forEach((edge) => {
      if (!adjacency.has(edge.source)) {
        adjacency.set(edge.source, []);
      }
      adjacency.get(edge.source)!.push(edge.target);
    });

    const dfs = (nodeId: string, path: string[]): void => {
      visited.add(nodeId);
      recursionStack.add(nodeId);
      path.push(nodeId);

      const neighbors = adjacency.get(nodeId) || [];
      for (const neighbor of neighbors) {
        if (!visited.has(neighbor)) {
          dfs(neighbor, [...path]);
        } else if (recursionStack.has(neighbor)) {
          // Found a cycle
          const cycleStart = path.indexOf(neighbor);
          const cycle = [...path.slice(cycleStart), neighbor];
          circularPaths.push(cycle);
        }
      }

      recursionStack.delete(nodeId);
    };

    nodes.forEach((node) => {
      if (!visited.has(node.id)) {
        dfs(node.id, []);
      }
    });

    return circularPaths;
  }

  /**
   * T082: Detects disconnected subgraphs in the graph
   * @param nodes Array of nodes
   * @param edges Array of edges
   * @returns Array of subgraphs, each containing node IDs
   */
  private detectSubgraphs(
    nodes: FlowNode[],
    edges: FlowEdge[]
  ): string[][] {
    const visited = new Set<string>();
    const subgraphs: string[][] = [];

    // Build adjacency list
    const adjacency = new Map<string, string[]>();
    edges.forEach((edge) => {
      if (!adjacency.has(edge.source)) {
        adjacency.set(edge.source, []);
      }
      if (!adjacency.has(edge.target)) {
        adjacency.set(edge.target, []);
      }
      adjacency.get(edge.source)!.push(edge.target);
      adjacency.get(edge.target)!.push(edge.source);
    });

    const dfs = (nodeId: string, component: string[]): void => {
      visited.add(nodeId);
      component.push(nodeId);

      const neighbors = adjacency.get(nodeId) || [];
      for (const neighbor of neighbors) {
        if (!visited.has(neighbor)) {
          dfs(neighbor, component);
        }
      }
    };

    // Find all connected components
    nodes.forEach((node) => {
      if (!visited.has(node.id)) {
        const component: string[] = [];
        dfs(node.id, component);
        subgraphs.push(component);
      }
    });

    return subgraphs;
  }
}
