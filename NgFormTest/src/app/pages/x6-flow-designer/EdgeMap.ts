import { Edge, Graph, Node as XNode } from '@antv/x6';

export class EdgeMap {
    public edge: Edge<Edge.Properties>;
    public target: XNode | null;

    public get targetId() {
        return this.target?.id;
    }

    constructor(edge: Edge<Edge.Properties>) {
        this.edge = edge;
        this.target = edge.getTargetNode();
    }

    /**
     * Edge<Edge.Properties>[] 转 EdgeMap[]
     * @param edges Edge<Edge.Properties>[]
     * @returns 
     */
    static toMaps(edges: Edge<Edge.Properties>[]): EdgeMap[] {

        return edges.map(res => {

            return new EdgeMap(res)
        })
    }
}
