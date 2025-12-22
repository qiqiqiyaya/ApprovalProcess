# 项目架构
1. .NET8 C# ABP vNext框架（版本8.3.4）领域驱动设计(DDD) 最佳实践
    1. 代码风格遵循 ABP vNext 领域驱动设计(DDD) 
2. 删除无用的代码，如注释、空行、命名空间引用等
3. 核心算法 图（graph），顶点 Node ，边 Flow
4. 命名采用 Pascal 命名法
5. 添加必要的注释，如类注释、属性注释、方法注释等
6. 项目前缀 ApFlow
7. 应用层 ApFlow.Application
    1. ApFlowAppService 所有应用服务的基础类
8. 应用合约层 ApFlow.Application.Contracts
9. 领域层 ApFlow.Domain
10. 领域共享层 ApFlow.Domain.Share
11. 仓储层 ApFlow.EntityFrameworkCore.Sqlserver 
    1. Entity Framwork Core框架 SQL Server 数据库 Code First 模式
12. Web主机层 ApFlow.Web.Host，部署为 Win Service 服务
13. 前端 ApFlow.Angular.UI
    1. 模仿钉钉、AntFlow(仓库地址：https://gitee.com/tylerzhou/Antflow)自定义流程
    2. Angular 框架 ng-zorro-antd  组件库（版本 19.3.1）
    3. 核心组件 @antv/x6 UI 图形库、NG-ALAIN 其下 @delon/form 动态表单
    4. 核心算法基于 @antv/x6 UI 图形库，图（graph）有向无环，顶点 Node ，边 Flow
    5. 实现鼠标拖拽自定义创建流程图
    6. 主要节点
        1. 审批人
        2. 并行审批
        3. 抄送人
        4. 条件分支
        5. 动态条件
        6. 条件并行