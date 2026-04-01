import { Component, Input } from '@angular/core';
import { ComponentNode } from '../../../models/component-node';
import { IFlowNode } from '../../../models/graph-definition';

@Component({
    selector: 'app-or-merge',
    templateUrl: './or-merge.component.html',
    styleUrls: ['./or-merge.component.css'],
    standalone: false,
})
export class OrMergeComponent implements ComponentNode {

    @Input() node: IFlowNode

    close() {
    }
}