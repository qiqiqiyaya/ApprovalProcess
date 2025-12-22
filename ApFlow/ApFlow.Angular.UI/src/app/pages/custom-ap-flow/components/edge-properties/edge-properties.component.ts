import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Cell } from '@antv/x6';

@Component({
    selector: 'app-edge-properties',
    templateUrl: './edge-properties.component.html',
    styleUrls: ['./edge-properties.component.css'],
    standalone:false
})
export class EdgePropertiesComponent implements OnInit, OnChanges {
    @Input() cell: Cell | null = null;

    edgeForm: FormGroup;

    constructor(private fb: FormBuilder) {
        this.edgeForm = this.fb.group({
            label: ['', Validators.required],
            condition: [''],
            conditionType: ['always'], // always, expression, script
            expression: [''],
            priority: [1]
        });
    }

    ngOnInit(): void {
        this.initForm();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['cell'] && this.cell) {
            this.loadEdgeData();
        }
    }

    /**
     * 初始化表单
     */
    initForm(): void {
        this.edgeForm.valueChanges.subscribe(value => {
            if (this.cell) {
                this.updateEdgeData(value);
            }
        });
    }

    /**
     * 加载边数据到表单
     */
    loadEdgeData(): void {
        if (!this.cell) return;

        const edgeData = this.cell.getData() || {};

        this.edgeForm.patchValue({
            label: edgeData.label || '',
            condition: edgeData.condition || '',
            conditionType: edgeData.conditionType || 'always',
            expression: edgeData.expression || '',
            priority: edgeData.priority || 1
        });
    }

    /**
     * 更新边数据
     */
    updateEdgeData(formData: any): void {
        if (!this.cell) return;

        const edgeData = {
            ...this.cell.getData(),
            ...formData
        };

        this.cell.setData(edgeData);

        // 更新边的标签
        if (this.cell.isEdge() && formData.label) {
            const edge = this.cell;
            const labels = edge.getLabels();

            if (labels && labels.length > 0) {
                edge.setLabelAt(0, formData.label);
            } else {
                edge.setLabels([{ attrs: { label: { text: formData.label } } }]);
            }
        }
    }

    /**
     * 条件类型变更处理
     */
    onConditionTypeChange(): void {
        const conditionType = this.edgeForm.get('conditionType')?.value;

        if (conditionType === 'always') {
            this.edgeForm.get('condition')?.clearValidators();
            this.edgeForm.get('expression')?.clearValidators();
        } else if (conditionType === 'expression') {
            this.edgeForm.get('condition')?.setValidators(Validators.required);
            this.edgeForm.get('expression')?.clearValidators();
        } else if (conditionType === 'script') {
            this.edgeForm.get('condition')?.clearValidators();
            this.edgeForm.get('expression')?.setValidators(Validators.required);
        }

        this.edgeForm.get('condition')?.updateValueAndValidity();
        this.edgeForm.get('expression')?.updateValueAndValidity();
    }
}