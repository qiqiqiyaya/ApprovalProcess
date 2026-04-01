import { Component, Input } from '@angular/core';
import { ComponentNode } from '../../../models/component-node';
import { IFlowNode } from '../../../models/graph-definition';

@Component({
    selector: 'app-or',
    templateUrl: './or.component.html',
    styleUrls: ['./or.component.css'],
    standalone: false,
})
export class OrComponent implements ComponentNode {

    @Input() node: IFlowNode

    close() {
    }
}