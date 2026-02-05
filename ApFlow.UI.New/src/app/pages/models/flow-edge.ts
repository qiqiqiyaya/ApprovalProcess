export class FlowEdge {
    source: string;
    target: string;
    router: string = 'orth';

    constructor(source: string, target: string) {
        this.source = source;
        this.target = target;
    }
}
