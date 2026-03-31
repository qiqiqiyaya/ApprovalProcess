import { Component, Input } from '@angular/core';
import { ComponentNode } from '../../../models/component-node';
import { IFlowNode } from '../../../models/graph-definition';

@Component({
    selector: 'app-start',
    templateUrl: './start.component.html',
    styleUrls: ['./start.component.css'],
    standalone: false,
})
export class StartComponent implements ComponentNode {

    @Input() node: IFlowNode

    close() {
    }
}
