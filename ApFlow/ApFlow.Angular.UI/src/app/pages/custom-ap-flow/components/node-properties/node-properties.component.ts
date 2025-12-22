import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cell } from '@antv/x6';
import { ApproverNodeProperties, ParallelNodeProperties, CcNodeProperties, ConditionNodeProperties, ConditionParallelNodeProperties } from '../../models/custom-ap-flow.models';

@Component({
    selector: 'app-node-properties',
    templateUrl: './node-properties.component.html',
    styleUrls: ['./node-properties.component.css'],
    standalone: false
})
export class NodePropertiesComponent implements OnInit, OnChanges {
    @Input() cell: Cell | null = null; // X6 节点对象

    // 节点属性表单
    nodePropertiesForm: FormGroup;

    // 节点类型
    nodeType: string = '';

    // 模拟数据
    mockUsers = [
        { id: 'user1', name: '张三' },
        { id: 'user2', name: '李四' },
        { id: 'user3', name: '王五' }
    ];

    mockRoles = [
        { id: 'role1', name: '经理' },
        { id: 'role2', name: '主管' },
        { id: 'role3', name: '员工' }
    ];

    mockDepartments = [
        { id: 'dept1', name: '技术部' },
        { id: 'dept2', name: '市场部' },
        { id: 'dept3', name: '人事部' }
    ];

    mockFormFields = [
        { id: 'field1', name: '申请金额' },
        { id: 'field2', name: '申请部门' },
        { id: 'field3', name: '申请事由' }
    ];

    mockNodes = [
        { id: 'node1', name: '审批人1' },
        { id: 'node2', name: '审批人2' },
        { id: 'node3', name: '审批人3' }
    ];

    // 抄送内容选项
    private _ccContentOptions: string[] = ['includeFormData', 'includeComments', 'includeAttachments'];

    // 审批人属性
    approverProperties: ApproverNodeProperties = {
        approverType: 'specific',
        approvalMethod: 'any',
        timeLimit: 24,
        timeoutAction: 'remind'
    };

    // 并行审批属性
    parallelProperties: ParallelNodeProperties = {
        branchCount: 2,
        branches: [],
        mergeCondition: 'all'
    };

    // 抄送属性
    ccProperties: CcNodeProperties = {
        ccType: 'specific',
        ccTiming: 'immediate',
        ccContent: {
            includeFormData: true,
            includeComments: true,
            includeAttachments: false
        }
    };

    // 条件分支属性
    conditionProperties: ConditionNodeProperties = {
        conditionType: 'form_field',
        branches: []
    };

    // 条件并行属性
    conditionParallelProperties: ConditionParallelNodeProperties = {
        parallelCount: 2,
        condition: '',
        branches: [],
        mergeCondition: 'all'
    };

    constructor(
        private fb: FormBuilder
    ) {
        // 初始化表单
        this.nodePropertiesForm = this.fb.group({
            label: ['', Validators.required],
            nodeType: ['']
        });
    }

    ngOnInit(): void {
        // 初始化组件
        this.initializeComponent();
    }

    ngOnChanges(changes: SimpleChanges): void {
        // 监听输入变化
        if (changes && changes['cell'] && changes['cell'].currentValue) {
            this.initializeComponent();
        }
    }

    /**
     * 初始化组件
     */
    private initializeComponent(): void {
        if (!this.cell) return;

        // 获取节点数据
        const nodeData = this.cell.getData() || {};
        this.nodeType = nodeData.nodeType || '';

        // 设置表单值
        this.nodePropertiesForm.patchValue({
            label: this.cell.getAttrByPath('label/text') || '',
            nodeType: this.nodeType
        });

        // 根据节点类型加载对应的属性
        this.loadNodeProperties();
    }

    /**
     * 加载节点属性
     */
    private loadNodeProperties(): void {
        const nodeData = this.cell?.getData() || {};
        const properties = nodeData.properties || {};

        switch (this.nodeType) {
            case 'approver':
                this.approverProperties = { ...this.approverProperties, ...properties };
                this.initApproverForm();
                break;
            case 'parallel':
                this.parallelProperties = { ...this.parallelProperties, ...properties };
                this.initParallelForm();
                break;
            case 'cc':
                this.ccProperties = { ...this.ccProperties, ...properties };
                this.initCcForm();
                break;
            case 'condition':
                this.conditionProperties = { ...this.conditionProperties, ...properties };
                this.initConditionForm();
                break;
            case 'condition-parallel':
                this.conditionParallelProperties = { ...this.conditionParallelProperties, ...properties };
                this.initConditionParallelForm();
                break;
        }
    }

    /**
     * 初始化审批人表单
     */
    private initApproverForm(): void {
        // 这里可以添加审批人特定的表单控件
        // 为了简化，我们在模板中直接处理
    }

    /**
     * 初始化并行审批表单
     */
    private initParallelForm(): void {
        // 这里可以添加并行审批特定的表单控件
        // 为了简化，我们在模板中直接处理
    }

    /**
     * 初始化抄送表单
     */
    private initCcForm(): void {
        // 这里可以添加抄送特定的表单控件
        // 为了简化，我们在模板中直接处理
    }

    /**
     * 初始化条件分支表单
     */
    private initConditionForm(): void {
        // 这里可以添加条件分支特定的表单控件
        // 为了简化，我们在模板中直接处理
    }

    /**
     * 保存节点属性
     */
    saveNodeProperties(): void {
        if (!this.cell || this.nodePropertiesForm.invalid) {
            // 标记所有控件为 touched 以显示验证错误
            Object.values(this.nodePropertiesForm.controls).forEach(control => {
                control.markAsTouched();
            });
            return;
        }

        // 更新节点标签
        const label = this.nodePropertiesForm.get('label')?.value;
        this.cell.setAttrByPath('label/text', label);

        // 根据节点类型更新属性
        let properties = {};

        switch (this.nodeType) {
            case 'approver':
                properties = this.approverProperties;
                break;
            case 'parallel':
                properties = this.parallelProperties;
                break;
            case 'cc':
                properties = this.ccProperties;
                break;
            case 'condition':
                properties = this.conditionProperties;
                break;
            case 'condition-parallel':
                properties = this.conditionParallelProperties;
                break;
        }

        // 更新节点数据
        const nodeData = {
            nodeType: this.nodeType,
            properties
        };

        this.cell.setData(nodeData);

        // 这里可以添加保存成功的提示
        console.log('节点属性已保存:', nodeData);
    }

    /**
     * 获取抄送内容选项
     */
    get ccContentOptions(): string[] {
        const options = [];
        if (this.ccProperties.ccContent.includeFormData) {
            options.push('includeFormData');
        }
        if (this.ccProperties.ccContent.includeComments) {
            options.push('includeComments');
        }
        if (this.ccProperties.ccContent.includeAttachments) {
            options.push('includeAttachments');
        }
        return options;
    }

    /**
     * 设置抄送内容选项
     */
    set ccContentOptions(options: string[]) {
        this.ccProperties.ccContent.includeFormData = options.includes('includeFormData');
        this.ccProperties.ccContent.includeComments = options.includes('includeComments');
        this.ccProperties.ccContent.includeAttachments = options.includes('includeAttachments');
    }

    /**
     * 添加并行分支
     */
    addParallelBranch(): void {
        const newBranch = {
            id: `branch_${Date.now()}`,
            name: `分支${this.parallelProperties.branches.length + 1}`,
            approvers: [{
                approverType: 'specific' as const,
                approvers: [],
                role: undefined,
                department: undefined
            }],
            approvalMethod: 'any' as const,
            timeLimit: 24
        };

        this.parallelProperties.branches.push(newBranch);
        this.parallelProperties.branchCount = this.parallelProperties.branches.length;
    }

    /**
     * 删除并行分支
     */
    removeParallelBranch(index: number): void {
        if (this.parallelProperties.branches.length > 1) {
            this.parallelProperties.branches.splice(index, 1);
            this.parallelProperties.branchCount = this.parallelProperties.branches.length;
        }
    }

    /**
     * 获取条件分支的字段条件字段ID
     */
    getFieldConditionFieldId(branch: any): string {
        return branch.condition?.fieldCondition?.fieldId || '';
    }

    /**
     * 设置条件分支的字段条件字段ID
     */
    setFieldConditionFieldId(branch: any, value: string): void {
        if (!branch.condition) {
            branch.condition = {};
        }
        if (!branch.condition.fieldCondition) {
            branch.condition.fieldCondition = {
                fieldId: '',
                operator: 'eq' as const,
                value: ''
            };
        }
        branch.condition.fieldCondition.fieldId = value;
    }

    /**
     * 获取条件分支的字段条件操作符
     */
    getFieldConditionOperator(branch: any): string {
        return branch.condition?.fieldCondition?.operator || 'eq';
    }

    /**
     * 设置条件分支的字段条件操作符
     */
    setFieldConditionOperator(branch: any, value: string): void {
        if (!branch.condition) {
            branch.condition = {};
        }
        if (!branch.condition.fieldCondition) {
            branch.condition.fieldCondition = {
                fieldId: '',
                operator: 'eq' as const,
                value: ''
            };
        }
        branch.condition.fieldCondition.operator = value as any;
    }

    /**
     * 获取条件分支的字段条件值
     */
    getFieldConditionValue(branch: any): string {
        return branch.condition?.fieldCondition?.value || '';
    }

    /**
     * 设置条件分支的字段条件值
     */
    setFieldConditionValue(branch: any, value: string): void {
        if (!branch.condition) {
            branch.condition = {};
        }
        if (!branch.condition.fieldCondition) {
            branch.condition.fieldCondition = {
                fieldId: '',
                operator: 'eq' as const,
                value: ''
            };
        }
        branch.condition.fieldCondition.value = value;
    }

    /**
     * 获取条件分支的审批结果条件节点ID
     */
    getApprovalResultConditionNodeId(branch: any): string {
        return branch.condition?.approvalResultCondition?.nodeId || '';
    }

    /**
     * 设置条件分支的审批结果条件节点ID
     */
    setApprovalResultConditionNodeId(branch: any, value: string): void {
        if (!branch.condition) {
            branch.condition = {};
        }
        if (!branch.condition.approvalResultCondition) {
            branch.condition.approvalResultCondition = {
                nodeId: '',
                result: 'approved' as const
            };
        }
        branch.condition.approvalResultCondition.nodeId = value;
    }

    /**
     * 获取条件分支的审批结果条件结果
     */
    getApprovalResultConditionResult(branch: any): string {
        return branch.condition?.approvalResultCondition?.result || 'approved';
    }

    /**
     * 设置条件分支的审批结果条件结果
     */
    setApprovalResultConditionResult(branch: any, value: string): void {
        if (!branch.condition) {
            branch.condition = {};
        }
        if (!branch.condition.approvalResultCondition) {
            branch.condition.approvalResultCondition = {
                nodeId: '',
                result: 'approved' as const
            };
        }
        branch.condition.approvalResultCondition.result = value as 'approved' | 'rejected' | 'withdrawn';
    }

    /**
     * 添加条件分支
     */
    addConditionBranch(): void {
        const newBranch = {
            id: `branch_${Date.now()}`,
            name: `条件${this.conditionProperties.branches.length + 1}`,
            condition: {
                fieldCondition: {
                    fieldId: '',
                    operator: 'eq' as const,
                    value: ''
                },
                scriptCondition: undefined,
                approvalResultCondition: {
                    nodeId: '',
                    result: 'approved' as const
                }
            },
            priority: this.conditionProperties.branches.length,
            isDefault: false
        };

        this.conditionProperties.branches.push(newBranch);
    }

    /**
     * 删除条件分支
     */
    removeConditionBranch(index: number): void {
        if (this.conditionProperties.branches.length > 1) {
            this.conditionProperties.branches.splice(index, 1);
        }
    }

    /**
     * 设置默认条件分支
     */
    setDefaultConditionBranch(index: number): void {
        this.conditionProperties.branches.forEach((branch, i) => {
            branch.isDefault = i === index;
        });
    }

    /**
     * 初始化条件并行表单
     */
    private initConditionParallelForm(): void {
        // 确保至少有一个分支
        if (this.conditionParallelProperties.branches.length === 0) {
            this.addConditionParallelBranch();
        }
    }

    /**
     * 添加条件并行分支
     */
    addConditionParallelBranch(): void {
        const newBranch = {
            id: `branch_${Date.now()}`,
            name: `分支${this.conditionParallelProperties.branches.length + 1}`,
            condition: '',
            approvers: [{
                approverType: 'specific' as const,
                approvers: [],
                role: undefined,
                department: undefined
            }],
            approvalMethod: 'any' as const,
            timeLimit: 24
        };

        this.conditionParallelProperties.branches.push(newBranch);
        this.conditionParallelProperties.parallelCount = this.conditionParallelProperties.branches.length;
    }

    /**
     * 删除条件并行分支
     */
    removeConditionParallelBranch(index: number): void {
        if (this.conditionParallelProperties.branches.length > 1) {
            this.conditionParallelProperties.branches.splice(index, 1);
            this.conditionParallelProperties.parallelCount = this.conditionParallelProperties.branches.length;
        }
    }
}