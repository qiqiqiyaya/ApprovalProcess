import { Injectable } from '@angular/core';
import { FlowGraph, FlowNode, FlowEdge, FlowTemplate, FlowInstance } from '../models/custom-ap-flow.models';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CustomApFlowService {
    // 流程数据流
    private flowDataSubject = new BehaviorSubject<FlowGraph>({
        name: '新建流程',
        description: '',
        nodes: [],
        edges: [],
        variables: []
    });

    // 流程模板数据流
    private flowTemplatesSubject = new BehaviorSubject<FlowTemplate[]>([]);

    // 流程实例数据流
    private flowInstancesSubject = new BehaviorSubject<FlowInstance[]>([]);

    constructor() { }

    /**
     * 获取流程数据流
     */
    getFlowData(): Observable<FlowGraph> {
        return this.flowDataSubject.asObservable();
    }

    /**
     * 获取当前流程数据
     */
    getCurrentFlowData(): FlowGraph {
        return this.flowDataSubject.value;
    }

    /**
     * 更新流程数据
     */
    updateFlowData(flowData: FlowGraph): void {
        this.flowDataSubject.next(flowData);
    }

    /**
     * 获取默认节点列表
     */
    getDefaultNodes(): FlowNode[] {
        return [
            {
                id: 'start-node',
                shape: 'circle',
                x: 100,
                y: 200,
                label: '开始',
                data: {
                    nodeType: 'start',
                    properties: {}
                }
            },
            {
                id: 'approver-node-1',
                shape: 'approver-node',
                x: 300,
                y: 200,
                label: '审批人',
                data: {
                    nodeType: 'approver',
                    properties: {
                        approverType: 'specific',
                        approvers: ['user1'],
                        approvalMethod: 'any',
                        timeLimit: 24,
                        timeoutAction: 'remind'
                    }
                }
            },
            {
                id: 'end-node',
                shape: 'circle',
                x: 500,
                y: 200,
                label: '结束',
                data: {
                    nodeType: 'end',
                    properties: {}
                }
            }
        ];
    }

    /**
     * 获取默认边列表
     */
    getDefaultEdges(): FlowEdge[] {
        return [
            {
                id: 'edge-1',
                source: 'start-node',
                target: 'approver-node-1',
                label: '',
                data: {
                    properties: {}
                }
            },
            {
                id: 'edge-2',
                source: 'approver-node-1',
                target: 'end-node',
                label: '',
                data: {
                    properties: {}
                }
            }
        ];
    }

    /**
     * 保存流程
     */
    saveFlow(flowData: FlowGraph): void {
        // 更新本地数据
        this.updateFlowData(flowData);

        // 这里应该调用API保存到后端
        console.log('保存流程数据:', flowData);

        // 模拟API调用
        this.saveFlowToBackend(flowData).subscribe(
            response => {
                console.log('流程保存成功:', response);
            },
            error => {
                console.error('流程保存失败:', error);
            }
        );
    }

    /**
     * 模拟后端API调用
     */
    private saveFlowToBackend(flowData: FlowGraph): Observable<any> {
        // 这里应该使用HttpClient调用实际API
        return new Observable(observer => {
            setTimeout(() => {
                observer.next({ success: true, message: '流程保存成功' });
                observer.complete();
            }, 500);
        });
    }

    /**
     * 加载流程
     */
    loadFlow(flowId: string): Observable<FlowGraph> {
        // 这里应该调用API从后端加载
        console.log('加载流程:', flowId);

        // 模拟API调用
        return new Observable(observer => {
            setTimeout(() => {
                const flowData = this.getCurrentFlowData();
                observer.next(flowData);
                observer.complete();
            }, 300);
        });
    }

    /**
     * 获取流程模板列表
     */
    getFlowTemplates(): Observable<FlowTemplate[]> {
        // 这里应该调用API从后端获取模板列表
        console.log('获取流程模板列表');

        // 模拟API调用
        return new Observable(observer => {
            setTimeout(() => {
                const templates = this.flowTemplatesSubject.value;
                observer.next(templates);
                observer.complete();
            }, 300);
        });
    }

    /**
     * 保存流程模板
     */
    saveFlowTemplate(template: FlowTemplate): Observable<FlowTemplate> {
        // 这里应该调用API保存到后端
        console.log('保存流程模板:', template);

        // 更新本地数据
        const templates = [...this.flowTemplatesSubject.value];
        const existingIndex = templates.findIndex(t => t.id === template.id);

        if (existingIndex >= 0) {
            templates[existingIndex] = template;
        } else {
            templates.push(template);
        }

        this.flowTemplatesSubject.next(templates);

        // 模拟API调用
        return new Observable(observer => {
            setTimeout(() => {
                observer.next(template);
                observer.complete();
            }, 500);
        });
    }

    /**
     * 删除流程模板
     */
    deleteFlowTemplate(templateId: string): Observable<boolean> {
        // 这里应该调用API删除后端数据
        console.log('删除流程模板:', templateId);

        // 更新本地数据
        const templates = this.flowTemplatesSubject.value.filter(t => t.id !== templateId);
        this.flowTemplatesSubject.next(templates);

        // 模拟API调用
        return new Observable(observer => {
            setTimeout(() => {
                observer.next(true);
                observer.complete();
            }, 300);
        });
    }

    /**
     * 获取流程实例列表
     */
    getFlowInstances(): Observable<FlowInstance[]> {
        // 这里应该调用API从后端获取实例列表
        console.log('获取流程实例列表');

        // 模拟API调用
        return new Observable(observer => {
            setTimeout(() => {
                const instances = this.flowInstancesSubject.value;
                observer.next(instances);
                observer.complete();
            }, 300);
        });
    }

    /**
     * 创建流程实例
     */
    createFlowInstance(templateId: string, formData: any, initiatorId: string): Observable<FlowInstance> {
        // 这里应该调用API创建实例
        console.log('创建流程实例:', { templateId, formData, initiatorId });

        // 创建新实例
        const newInstance: FlowInstance = {
            id: `instance_${Date.now()}`,
            templateId,
            name: `流程实例_${Date.now()}`,
            initiatorId,
            status: 'running',
            currentNodeIds: [],
            completedNodeIds: [],
            formData,
            approvalRecords: [],
            createdAt: new Date(),
            updatedAt: new Date()
        };

        // 更新本地数据
        const instances = [...this.flowInstancesSubject.value, newInstance];
        this.flowInstancesSubject.next(instances);

        // 模拟API调用
        return new Observable(observer => {
            setTimeout(() => {
                observer.next(newInstance);
                observer.complete();
            }, 500);
        });
    }

    /**
     * 处理审批
     */
    processApproval(instanceId: string, nodeId: string, approverId: string, result: 'approved' | 'rejected' | 'withdrawn', comment?: string): Observable<FlowInstance> {
        // 这里应该调用API处理审批
        console.log('处理审批:', { instanceId, nodeId, approverId, result, comment });

        // 更新本地数据
        const instances = [...this.flowInstancesSubject.value];
        const instanceIndex = instances.findIndex(i => i.id === instanceId);

        if (instanceIndex >= 0) {
            const instance = instances[instanceIndex];

            // 添加审批记录
            instance.approvalRecords.push({
                nodeId,
                approverId,
                result,
                comment,
                timestamp: new Date()
            });

            // 更新状态
            instance.updatedAt = new Date();

            // 这里应该有更复杂的状态机逻辑来更新当前节点和状态
            // 简化处理，假设审批后流程完成
            if (result === 'approved') {
                instance.status = 'completed';
                instance.completedAt = new Date();
            }

            this.flowInstancesSubject.next(instances);

            // 模拟API调用
            return new Observable(observer => {
                setTimeout(() => {
                    observer.next(instance);
                    observer.complete();
                }, 500);
            });
        } else {
            // 实例不存在
            return new Observable(observer => {
                observer.error(new Error('流程实例不存在'));
                observer.complete();
            });
        }
    }

    /**
     * 验证流程图
     */
    validateFlowGraph(flowData: FlowGraph): { valid: boolean; errors: string[] } {
        const errors: string[] = [];

        // 检查是否有节点
        if (flowData.nodes.length === 0) {
            errors.push('流程图必须包含至少一个节点');
        }

        // 检查是否有开始节点
        const startNodes = flowData.nodes.filter(node => node.data.nodeType === 'start');
        if (startNodes.length === 0) {
            errors.push('流程图必须包含一个开始节点');
        } else if (startNodes.length > 1) {
            errors.push('流程图只能包含一个开始节点');
        }

        // 检查是否有结束节点
        const endNodes = flowData.nodes.filter(node => node.data.nodeType === 'end');
        if (endNodes.length === 0) {
            errors.push('流程图必须包含至少一个结束节点');
        }

        // 检查边的有效性
        flowData.edges.forEach(edge => {
            const sourceExists = flowData.nodes.some(node => node.id === edge.source);
            const targetExists = flowData.nodes.some(node => node.id === edge.target);

            if (!sourceExists) {
                errors.push(`边 ${edge.id} 的源节点 ${edge.source} 不存在`);
            }

            if (!targetExists) {
                errors.push(`边 ${edge.id} 的目标节点 ${edge.target} 不存在`);
            }
        });

        // 检查是否有孤立节点（没有入边或出边的节点，除了开始和结束节点）
        flowData.nodes.forEach(node => {
            if (node.data.nodeType !== 'start' && node.data.nodeType !== 'end') {
                const hasIncomingEdges = flowData.edges.some(edge => edge.target === node.id);
                const hasOutgoingEdges = flowData.edges.some(edge => edge.source === node.id);

                if (!hasIncomingEdges) {
                    errors.push(`节点 ${node.id} 没有入边`);
                }

                if (!hasOutgoingEdges) {
                    errors.push(`节点 ${node.id} 没有出边`);
                }
            }
        });

        return {
            valid: errors.length === 0,
            errors
        };
    }
}