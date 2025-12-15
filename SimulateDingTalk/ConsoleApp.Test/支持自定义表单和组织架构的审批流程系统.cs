using System;
using System.Collections.Generic;
using System.Linq;

// 审批流程引擎 - 支持自定义表单和组织架构
public class FormBasedApprovalEngine
{
    // 存储注册的审批流程
    private Dictionary<string, ApprovalProcess> processes = new Dictionary<string, ApprovalProcess>();
    // 存储运行中的审批实例
    private Dictionary<int, ApprovalInstance> instances = new Dictionary<int, ApprovalInstance>();
    // 组织架构服务实例
    private OrganizationService organizationService = new OrganizationService();

    // 注册审批流程
    public void RegisterProcess(string processName, ApprovalProcess process)
    {
        processes[processName] = process;
    }

    // 启动审批流程实例
    public ApprovalInstance StartProcess(string processName, Dictionary<string, object> formData, string initiator)
    {
        // 检查流程是否已注册
        if (!processes.ContainsKey(processName))
            throw new ArgumentException($"流程 {processName} 未注册");

        // 获取流程定义
        var process = processes[processName];
        // 创建审批实例
        var instance = new ApprovalInstance
        {
            Id = instances.Count + 1, // 生成唯一实例ID
            Process = process, // 设置流程定义
            FormData = formData, // 设置表单数据
            CurrentNodeIds = new List<int> { process.StartNodeId }, // 设置当前节点ID列表
            Status = ApprovalStatus.Pending, // 设置初始状态为待审批
            Initiator = initiator, // 设置发起人
            CreateTime = DateTime.Now, // 设置创建时间
            NodeResults = new Dictionary<int, ApprovalNodeResult>(), // 初始化节点结果字典
            ActiveBranches = new List<int> { process.StartNodeId }, // 初始化活跃分支
            PendingApprovals = new List<int>() // 初始化待审批节点列表
        };

        // 将实例添加到实例字典中
        instances[instance.Id] = instance;
        // 处理节点
        ProcessNodes(instance, instance.CurrentNodeIds);

        return instance;
    }

    // 处理节点
    private void ProcessNodes(ApprovalInstance instance, List<int> nodeIds)
    {
        // 遍历节点ID列表
        foreach (var nodeId in nodeIds)
        {
            // 获取节点定义
            var node = instance.Process.Nodes[nodeId];

            // 根据节点类型处理
            switch (node.NodeType)
            {
                case NodeType.Approval: // 审批节点
                    ProcessApprovalNode(instance, node);
                    break;
                case NodeType.Gateway: // 网关节点
                    ProcessGatewayNode(instance, node);
                    break;
                case NodeType.End: // 结束节点
                    ProcessEndNode(instance, node);
                    break;
                case NodeType.Start: // 开始节点
                    // 开始节点，直接处理下一个节点
                    var nextNodes = node.OutgoingFlows.Select(f => f.TargetNodeId).ToList();
                    ProcessNodes(instance, nextNodes);
                    break;
            }
        }
    }

    // 处理审批节点
    private void ProcessApprovalNode(ApprovalInstance instance, ApprovalNode node)
    {
        // 检查节点是否已处理
        if (instance.NodeResults.ContainsKey(node.Id)) return;

        // 计算实际审批人（基于表单数据和组织架构）
        var actualApprovers = CalculateApprovers(instance, node);

        // 更新节点的实际审批人列表
        node.EffectiveApprovers = actualApprovers;

        // 添加到待处理节点
        if (!instance.PendingApprovals.Contains(node.Id))
        {
            instance.PendingApprovals.Add(node.Id);
            NotifyNode(instance, node);
        }
    }

    // 计算实际审批人
    private List<string> CalculateApprovers(ApprovalInstance instance, ApprovalNode node)
    {
        var approvers = new List<string>();

        // 按组织架构获取审批人
        if (node.ApproverSource == ApproverSource.Organization)
        {
            if (node.OrganizationLevel == OrganizationLevel.DepartmentManager)
            {
                var department = GetEmployeeDepartment(instance.Initiator);
                var manager = organizationService.GetDepartmentManager(department);
                if (!string.IsNullOrEmpty(manager))
                    approvers.Add(manager);
            }
            else if (node.OrganizationLevel == OrganizationLevel.CompanyLeader)
            {
                var leader = organizationService.GetCompanyLeader();
                if (!string.IsNullOrEmpty(leader))
                    approvers.Add(leader);
            }
            else if (node.OrganizationLevel == OrganizationLevel.RoleBased)
            {
                approvers.AddRange(organizationService.GetUsersByRole(node.RequiredRole));
            }
        }
        // 指定员工
        else if (node.ApproverSource == ApproverSource.SpecificUsers)
        {
            approvers.AddRange(node.SpecifiedApprovers);
        }
        // 基于表单数据动态确定
        else if (node.ApproverSource == ApproverSource.FormulaBased)
        {
            approvers.AddRange(EvaluateApproverFormula(instance, node.ApproverFormula));
        }

        return approvers.Distinct().ToList();
    }

    // 获取员工部门
    private string GetEmployeeDepartment(string employeeId)
    {
        // 实际应用中从组织架构服务获取
        return organizationService.GetUserDepartment(employeeId);
    }

    // 评估审批人公式
    private List<string> EvaluateApproverFormula(ApprovalInstance instance, string formula)
    {
        // 简化的公式计算，实际应用中需要完整的表达式解析器
        // 示例: "Amount > 5000 ? 'finance_manager' : 'department_manager'"
        if (formula.Contains("department_manager"))
        {
            var department = GetEmployeeDepartment(instance.Initiator);
            return new List<string> { organizationService.GetDepartmentManager(department) };
        }
        return new List<string>();
    }

    // 处理网关节点
    private void ProcessGatewayNode(ApprovalInstance instance, ApprovalNode node)
    {
        switch (node.GatewayType)
        {
            case GatewayType.Exclusive: // XOR - 排他网关
                ProcessExclusiveGateway(instance, node);
                break;
            case GatewayType.Parallel: // AND - 并行网关
                ProcessParallelGateway(instance, node);
                break;
            case GatewayType.Inclusive: // OR - 包含网关
                ProcessInclusiveGateway(instance, node);
                break;
        }
    }

    // 处理排他网关
    private void ProcessExclusiveGateway(ApprovalInstance instance, ApprovalNode node)
    {
        // 排他网关：根据条件选择一个分支
        var selectedFlow = node.OutgoingFlows.FirstOrDefault(f =>
            EvaluateCondition(instance, f.Condition));

        if (selectedFlow != null)
        {
            var nextNode = instance.Process.Nodes[selectedFlow.TargetNodeId];
            MoveToNode(instance, nextNode);
        }
        else
        {
            // 如果没有条件匹配，可以选择默认路径
            var defaultFlow = node.OutgoingFlows.FirstOrDefault(f => f.IsDefault);
            if (defaultFlow != null)
            {
                var nextNode = instance.Process.Nodes[defaultFlow.TargetNodeId];
                MoveToNode(instance, nextNode);
            }
        }
    }

    // 处理并行网关
    private void ProcessParallelGateway(ApprovalInstance instance, ApprovalNode node)
    {
        if (node.GatewayDirection == GatewayDirection.Split)
        {
            // 并行分裂：同时激活所有输出流
            foreach (var flow in node.OutgoingFlows)
            {
                var nextNode = instance.Process.Nodes[flow.TargetNodeId];
                MoveToNode(instance, nextNode);
            }
        }
        else if (node.GatewayDirection == GatewayDirection.Join)
        {
            // 并行汇聚：等待所有输入流完成
            var incomingCompleted = node.IncomingFlows.All(flow =>
                instance.NodeResults.ContainsKey(instance.Process.Nodes[flow.SourceNodeId].Id) &&
                instance.NodeResults[instance.Process.Nodes[flow.SourceNodeId].Id].Status == NodeStatus.Completed);

            if (incomingCompleted)
            {
                // 激活输出流
                foreach (var flow in node.OutgoingFlows)
                {
                    var nextNode = instance.Process.Nodes[flow.TargetNodeId];
                    MoveToNode(instance, nextNode);
                }
            }
        }
    }

    // 处理包含网关
    private void ProcessInclusiveGateway(ApprovalInstance instance, ApprovalNode node)
    {
        if (node.GatewayDirection == GatewayDirection.Split)
        {
            // 包含分裂：激活满足条件的多个分支
            var activeFlows = node.OutgoingFlows.Where(f =>
                EvaluateCondition(instance, f.Condition) || f.IsDefault);

            foreach (var flow in activeFlows)
            {
                var nextNode = instance.Process.Nodes[flow.TargetNodeId];
                MoveToNode(instance, nextNode);
            }
        }
        else if (node.GatewayDirection == GatewayDirection.Join)
        {
            // 包含汇聚：等待至少一个输入流完成
            var completedIncoming = node.IncomingFlows
                .Where(flow => instance.NodeResults.ContainsKey(instance.Process.Nodes[flow.SourceNodeId].Id) &&
                              instance.NodeResults[instance.Process.Nodes[flow.SourceNodeId].Id].Status == NodeStatus.Completed)
                .ToList();

            // 检查是否所有可能的输入都已完成
            var allPossibleInputs = node.IncomingFlows
                .Where(f => EvaluateCondition(instance, f.Condition) || f.IsDefault)
                .ToList();

            if (completedIncoming.Count == allPossibleInputs.Count)
            {
                // 激活输出流
                foreach (var flow in node.OutgoingFlows)
                {
                    var nextNode = instance.Process.Nodes[flow.TargetNodeId];
                    MoveToNode(instance, nextNode);
                }
            }
        }
    }

    // 处理结束节点
    private void ProcessEndNode(ApprovalInstance instance, ApprovalNode node)
    {
        // 标记节点完成
        instance.NodeResults[node.Id] = new ApprovalNodeResult
        {
            NodeId = node.Id,
            Status = NodeStatus.Completed,
            Time = DateTime.Now
        };

        // 检查是否所有活跃分支都已完成
        var activeNodes = instance.ActiveBranches
            .Where(id => !instance.NodeResults.ContainsKey(id) ||
                        instance.NodeResults[id].Status != NodeStatus.Completed)
            .ToList();

        if (!activeNodes.Any())
        {
            instance.Status = ApprovalStatus.Approved;
            OnProcessCompleted(instance);
        }
    }

    // 移动到下一个节点
    private void MoveToNode(ApprovalInstance instance, ApprovalNode node)
    {
        if (!instance.ActiveBranches.Contains(node.Id))
        {
            instance.ActiveBranches.Add(node.Id);
        }

        ProcessNodes(instance, new List<int> { node.Id });
    }

    // 处理审批操作
    public bool ProcessApproval(int instanceId, int nodeId, string approver, ApprovalAction action, string comment = "")
    {
        // 检查实例是否存在
        if (!instances.ContainsKey(instanceId))
            throw new ArgumentException($"审批实例 {instanceId} 不存在");

        var instance = instances[instanceId];
        if (instance.Status != ApprovalStatus.Pending)
            throw new InvalidOperationException("审批实例状态不正确");

        var node = instance.Process.Nodes[nodeId];
        if (node.NodeType != NodeType.Approval)
            throw new InvalidOperationException("指定节点不是审批节点");

        // 验证审批人权限（检查是否在有效审批人列表中）
        if (!node.EffectiveApprovers.Contains(approver))
            throw new UnauthorizedAccessException($"用户 {approver} 无权审批此节点");

        // 记录审批结果
        var result = new ApprovalNodeResult
        {
            NodeId = nodeId,
            Approver = approver,
            Action = action,
            Comment = comment,
            Time = DateTime.Now,
            Status = NodeStatus.Completed
        };

        instance.NodeResults[nodeId] = result;
        instance.PendingApprovals.Remove(nodeId);

        if (action == ApprovalAction.Reject)
        {
            instance.Status = ApprovalStatus.Rejected;
            OnProcessCompleted(instance);
            return true;
        }

        // 激活后续节点
        var outgoingFlows = node.OutgoingFlows;
        var nextNodes = outgoingFlows.Select(f => instance.Process.Nodes[f.TargetNodeId]).ToList();

        foreach (var nextNode in nextNodes)
        {
            MoveToNode(instance, nextNode);
        }

        return true;
    }

    // 评估条件
    private bool EvaluateCondition(ApprovalInstance instance, string condition)
    {
        // 简单的条件评估，实际应用中可能需要更复杂的表达式解析
        if (string.IsNullOrEmpty(condition)) return true;

        // 示例：基于表单数据的条件判断
        try
        {
            if (condition.Contains("Amount"))
            {
                var amountStr = instance.FormData["Amount"]?.ToString();
                if (decimal.TryParse(amountStr, out decimal amount))
                {
                    if (condition.Contains(">"))
                    {
                        var thresholdStr = condition.Split('>')[1].Trim();
                        if (decimal.TryParse(thresholdStr, out decimal threshold))
                            return amount > threshold;
                    }
                    else if (condition.Contains("<"))
                    {
                        var thresholdStr = condition.Split('<')[1].Trim();
                        if (decimal.TryParse(thresholdStr, out decimal threshold))
                            return amount < threshold;
                    }
                }
            }
            return true; // 默认返回true
        }
        catch
        {
            return false;
        }
    }

    // 通知节点
    private void NotifyNode(ApprovalInstance instance, ApprovalNode node)
    {
        Console.WriteLine($"通知: 审批实例 {instance.Id} 需要节点 '{node.Name}' 进行审批");
        Console.WriteLine($"审批人: {string.Join(", ", node.EffectiveApprovers)}");
        Console.WriteLine($"表单数据: {string.Join(", ", instance.FormData.Select(kvp => $"{kvp.Key}={kvp.Value}"))}");
    }

    // 审批完成回调
    private void OnProcessCompleted(ApprovalInstance instance)
    {
        Console.WriteLine($"审批流程 {instance.Id} 已完成，最终状态: {instance.Status}");
    }

    // 获取审批实例
    public ApprovalInstance GetInstance(int instanceId)
    {
        return instances.ContainsKey(instanceId) ? instances[instanceId] : null;
    }
}

// 表单字段定义
public class FormField
{
    public string FieldId { get; set; } // 字段ID
    public string FieldName { get; set; } // 字段名称
    public FieldType FieldType { get; set; } // 字段类型
    public bool IsRequired { get; set; } // 是否必填
    public string DefaultValue { get; set; } // 默认值
    public List<string> Options { get; set; } = new List<string>(); // 下拉选项
}

// 字段类型
public enum FieldType
{
    Text, // 文本
    Number, // 数字
    Date, // 日期
    Select, // 下拉选择
    MultiSelect, // 多选
    Checkbox, // 复选框
    TextArea // 文本域
}

// 审批流程定义
public class ApprovalProcess
{
    public string Name { get; set; } // 流程名称
    public string Description { get; set; } // 流程描述
    public List<FormField> FormFields { get; set; } = new List<FormField>(); // 表单字段列表
    public Dictionary<int, ApprovalNode> Nodes { get; set; } = new Dictionary<int, ApprovalNode>(); // 节点字典
    public List<ApprovalFlow> Flows { get; set; } = new List<ApprovalFlow>(); // 连接线列表
    public int StartNodeId { get; set; } // 开始节点ID

    public ApprovalNode this[int id] => Nodes.ContainsKey(id) ? Nodes[id] : null; // 索引器
}

// 审批节点
public class ApprovalNode
{
    public int Id { get; set; } // 节点ID
    public string Name { get; set; } // 节点名称
    public NodeType NodeType { get; set; } // 节点类型

    // 审批人配置
    public ApproverSource ApproverSource { get; set; } = ApproverSource.SpecificUsers; // 审批人来源
    public OrganizationLevel OrganizationLevel { get; set; } = OrganizationLevel.DepartmentManager; // 组织层级
    public string RequiredRole { get; set; } // 角色基础审批
    public List<string> SpecifiedApprovers { get; set; } = new List<string>(); // 指定审批人
    public string ApproverFormula { get; set; } // 审批人公式

    // 实际审批人（运行时计算）
    public List<string> EffectiveApprovers { get; set; } = new List<string>();

    // 仅用于网关节点
    public GatewayType GatewayType { get; set; } // 网关类型
    public GatewayDirection GatewayDirection { get; set; } // 网关方向

    // 连接关系
    public List<ApprovalFlow> IncomingFlows { get; set; } = new List<ApprovalFlow>(); // 输入流
    public List<ApprovalFlow> OutgoingFlows { get; set; } = new List<ApprovalFlow>(); // 输出流
}

// 审批人来源
public enum ApproverSource
{
    SpecificUsers, // 指定用户
    Organization, // 组织架构
    FormulaBased // 公式计算
}

// 组织层级
public enum OrganizationLevel
{
    DepartmentManager, // 部门经理
    CompanyLeader, // 公司领导
    RoleBased // 角色基础
}

// 节点类型
public enum NodeType
{
    Approval, // 审批节点
    Gateway, // 网关节点
    Start, // 开始节点
    End // 结束节点
}

// 网关类型
public enum GatewayType
{
    Exclusive, // 排他网关 (XOR)
    Parallel, // 并行网关 (AND)
    Inclusive // 包含网关 (OR)
}

// 网关方向
public enum GatewayDirection
{
    Split, // 分裂
    Join // 汇聚
}

// 连接线
public class ApprovalFlow
{
    public int Id { get; set; } // 连接线ID
    public int SourceNodeId { get; set; } // 源节点ID
    public int TargetNodeId { get; set; } // 目标节点ID
    public string Condition { get; set; } // 条件表达式
    public bool IsDefault { get; set; } // 是否为默认路径
}

// 审批动作
public enum ApprovalAction
{
    Approve, // 同意
    Reject // 拒绝
}

// 审批状态
public enum ApprovalStatus
{
    Pending, // 待审批
    Approved, // 已通过
    Rejected, // 已拒绝
    Canceled // 已取消
}

// 节点状态
public enum NodeStatus
{
    Pending, // 待处理
    Completed, // 已完成
    Rejected // 已拒绝
}

// 审批实例
public class ApprovalInstance
{
    public int Id { get; set; } // 实例ID
    public ApprovalProcess Process { get; set; } // 关联的流程定义
    public Dictionary<string, object> FormData { get; set; } // 表单数据
    public List<int> CurrentNodeIds { get; set; } // 当前节点ID列表
    public List<int> ActiveBranches { get; set; } // 活跃分支列表
    public List<int> PendingApprovals { get; set; } // 待审批节点列表
    public ApprovalStatus Status { get; set; } // 审批状态
    public string Initiator { get; set; } // 发起人
    public DateTime CreateTime { get; set; } // 创建时间
    public Dictionary<int, ApprovalNodeResult> NodeResults { get; set; } // 节点结果字典
}

// 节点审批结果
public class ApprovalNodeResult
{
    public int NodeId { get; set; } // 节点ID
    public string Approver { get; set; } // 审批人
    public ApprovalAction Action { get; set; } // 审批动作
    public string Comment { get; set; } // 审批意见
    public DateTime Time { get; set; } // 审批时间
    public NodeStatus Status { get; set; } // 节点状态
}

// 组织服务模拟
public class OrganizationService
{
    // 用户部门映射
    private Dictionary<string, string> userDepartments = new Dictionary<string, string>
    {
        { "zhangsan", "IT" },
        { "lisi", "HR" },
        { "wangwu", "Finance" },
        { "manager1", "IT" },
        { "hr1", "HR" },
        { "finance1", "Finance" },
        { "ceo1", "Executive" }
    };

    // 部门经理映射
    private Dictionary<string, string> departmentManagers = new Dictionary<string, string>
    {
        { "IT", "manager1" },
        { "HR", "hr_leader" },
        { "Finance", "finance_manager" }
    };

    // 角色用户映射
    private Dictionary<string, List<string>> roleUsers = new Dictionary<string, List<string>>
    {
        { "hr_specialist", new List<string> { "hr1", "hr2" } },
        { "finance_approver", new List<string> { "finance1", "finance2" } }
    };

    // 获取用户部门
    public string GetUserDepartment(string userId)
    {
        return userDepartments.ContainsKey(userId) ? userDepartments[userId] : "Unknown";
    }

    // 获取部门经理
    public string GetDepartmentManager(string department)
    {
        return departmentManagers.ContainsKey(department) ? departmentManagers[department] : null;
    }

    // 获取公司领导
    public string GetCompanyLeader()
    {
        return "ceo1";
    }

    // 按角色获取用户列表
    public List<string> GetUsersByRole(string role)
    {
        return roleUsers.ContainsKey(role) ? roleUsers[role] : new List<string>();
    }
}

// 示例使用
class Program
{
    static void Main(string[] args)
    {
        var engine = new FormBasedApprovalEngine(); // 创建审批引擎实例

        // 创建带有自定义表单的审批流程
        var formBasedProcess = new ApprovalProcess
        {
            Name = "ExpenseApproval", // 设置流程名称
            Description = "费用报销审批流程", // 设置流程描述
            StartNodeId = 1, // 设置开始节点ID
            // 定义表单字段
            FormFields = new List<FormField>
            {
                new FormField { FieldId = "employee_name", FieldName = "申请人姓名", FieldType = FieldType.Text, IsRequired = true },
                new FormField { FieldId = "department", FieldName = "申请部门", FieldType = FieldType.Select, IsRequired = true, Options = new List<string> { "IT", "HR", "Finance", "Sales" } },
                new FormField { FieldId = "amount", FieldName = "报销金额", FieldType = FieldType.Number, IsRequired = true },
                new FormField { FieldId = "expense_type", FieldName = "费用类型", FieldType = FieldType.Select, IsRequired = true, Options = new List<string> { "差旅费", "办公费", "招待费", "培训费" } },
                new FormField { FieldId = "description", FieldName = "费用说明", FieldType = FieldType.TextArea, IsRequired = false }
            }
        };

        // 创建开始节点
        var startNode = new ApprovalNode
        {
            Id = 1,
            Name = "开始",
            NodeType = NodeType.Start
        };

        // 创建直属领导审批节点
        var directManagerApproval = new ApprovalNode
        {
            Id = 2,
            Name = "直属领导审批",
            NodeType = NodeType.Approval,
            ApproverSource = ApproverSource.Organization,
            OrganizationLevel = OrganizationLevel.DepartmentManager
        };

        // 创建金额检查网关节点
        var amountCheckGateway = new ApprovalNode
        {
            Id = 3,
            Name = "金额检查网关",
            NodeType = NodeType.Gateway,
            GatewayType = GatewayType.Exclusive,
            GatewayDirection = GatewayDirection.Split
        };

        // 创建常规审批节点
        var normalApproval = new ApprovalNode
        {
            Id = 4,
            Name = "常规审批",
            NodeType = NodeType.Approval,
            ApproverSource = ApproverSource.Organization,
            OrganizationLevel = OrganizationLevel.RoleBased,
            RequiredRole = "finance_approver"
        };

        // 创建大额审批节点
        var highAmountApproval = new ApprovalNode
        {
            Id = 5,
            Name = "大额审批",
            NodeType = NodeType.Approval,
            ApproverSource = ApproverSource.Organization,
            OrganizationLevel = OrganizationLevel.CompanyLeader
        };

        // 创建结束节点
        var endNode = new ApprovalNode
        {
            Id = 6,
            Name = "结束",
            NodeType = NodeType.End
        };

        // 添加节点到流程
        formBasedProcess.Nodes.Add(startNode.Id, startNode);
        formBasedProcess.Nodes.Add(directManagerApproval.Id, directManagerApproval);
        formBasedProcess.Nodes.Add(amountCheckGateway.Id, amountCheckGateway);
        formBasedProcess.Nodes.Add(normalApproval.Id, normalApproval);
        formBasedProcess.Nodes.Add(highAmountApproval.Id, highAmountApproval);
        formBasedProcess.Nodes.Add(endNode.Id, endNode);

        // 创建连接线
        var flow1 = new ApprovalFlow { Id = 1, SourceNodeId = 1, TargetNodeId = 2 };
        var flow2 = new ApprovalFlow { Id = 2, SourceNodeId = 2, TargetNodeId = 3 };
        var flow3 = new ApprovalFlow { Id = 3, SourceNodeId = 3, TargetNodeId = 4, Condition = "amount < 5000" };
        var flow4 = new ApprovalFlow { Id = 4, SourceNodeId = 3, TargetNodeId = 5, Condition = "amount >= 5000" };
        var flow5 = new ApprovalFlow { Id = 5, SourceNodeId = 4, TargetNodeId = 6 };
        var flow6 = new ApprovalFlow { Id = 6, SourceNodeId = 5, TargetNodeId = 6 };

        formBasedProcess.Flows.AddRange(new[] { flow1, flow2, flow3, flow4, flow5, flow6 });

        // 建立节点连接关系
        LinkNodes(formBasedProcess);

        // 注册流程
        engine.RegisterProcess("ExpenseApproval", formBasedProcess);

        // 准备表单数据
        var formData = new Dictionary<string, object>
        {
            { "employee_name", "张三" },
            { "department", "IT" },
            { "amount", 3000 }, // 小于5000，走常规审批
            { "expense_type", "差旅费" },
            { "description", "出差交通费" }
        };

        // 启动审批实例
        var instance = engine.StartProcess("ExpenseApproval", formData, "zhangsan");

        Console.WriteLine("开始审批流程...");

        // 获取实际审批人信息
        var managerApprovalNode = instance.Process.Nodes[2];
        Console.WriteLine($"直属领导审批节点实际审批人: {string.Join(", ", managerApprovalNode.EffectiveApprovers)}");

        // 完成直属领导审批
        engine.ProcessApproval(instance.Id, 2, "manager1", ApprovalAction.Approve, "符合规定，同意报销");

        // 获取下一步审批人
        var financeApprovalNode = instance.Process.Nodes[4];
        Console.WriteLine($"财务审批节点实际审批人: {string.Join(", ", financeApprovalNode.EffectiveApprovers)}");

        // 完成财务审批
        engine.ProcessApproval(instance.Id, 4, "finance1", ApprovalAction.Approve, "财务审核通过");

        // 查看最终结果
        var finalInstance = engine.GetInstance(instance.Id);
        Console.WriteLine($"最终状态: {finalInstance.Status}");

        // 查看节点处理历史
        Console.WriteLine("节点处理历史:");
        foreach (var kvp in finalInstance.NodeResults)
        {
            var node = finalInstance.Process[kvp.Key];
            var result = kvp.Value;
            Console.WriteLine($"节点'{node.Name}': {result.Approver} - {result.Action} - {result.Comment} - {result.Time}");
        }

        // 显示表单数据
        Console.WriteLine("提交的表单数据:");
        foreach (var kvp in finalInstance.FormData)
        {
            Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
        }

        Console.Read();
    }

    // 建立节点连接关系
    static void LinkNodes(ApprovalProcess process)
    {
        // 建立节点的流入流出关系
        foreach (var flow in process.Flows)
        {
            if (process.Nodes.ContainsKey(flow.SourceNodeId))
            {
                var sourceNode = process.Nodes[flow.SourceNodeId];
                if (!sourceNode.OutgoingFlows.Exists(f => f.Id == flow.Id))
                    sourceNode.OutgoingFlows.Add(flow);
            }

            if (process.Nodes.ContainsKey(flow.TargetNodeId))
            {
                var targetNode = process.Nodes[flow.TargetNodeId];
                if (!targetNode.IncomingFlows.Exists(f => f.Id == flow.Id))
                    targetNode.IncomingFlows.Add(flow);
            }
        }
    }
}



