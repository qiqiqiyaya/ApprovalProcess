import { Component, Input } from '@angular/core';
import { ComponentNode } from '../../../models/component-node';
import { IFlowNode } from '../../../models/graph-definition';

@Component({
    selector: 'app-end',
    templateUrl: './end.component.html',
    styleUrls: ['./end.component.css'],
    standalone: false,
})
export class EndComponent implements ComponentNode {

    @Input() node: IFlowNode

    close() {
    }
}
