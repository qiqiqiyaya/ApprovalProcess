using ApFlow.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ApFlow.EntityFrameworkCore.Sqlserver.DbContexts
{
    /// <summary>
    /// 审批流程数据库上下文
    /// </summary>
    public class ApFlowDbContext : AbpDbContext<ApFlowDbContext>
    {
        /// <summary>
        /// 审批流程表
        /// </summary>
        public DbSet<ApprovalFlow> ApprovalFlows { get; set; }

        /// <summary>
        /// 审批流程图表
        /// </summary>
        public DbSet<Graph> Graphs { get; set; }

        /// <summary>
        /// 审批节点表
        /// </summary>
        public DbSet<Node> Nodes { get; set; }

        /// <summary>
        /// 流程连线表
        /// </summary>
        public DbSet<Flow> Flows { get; set; }

        /// <summary>
        /// 表单表
        /// </summary>
        public DbSet<Form> Forms { get; set; }

        /// <summary>
        /// 表单字段表
        /// </summary>
        public DbSet<FormField> FormFields { get; set; }

        /// <summary>
        /// 审批实例表
        /// </summary>
        public DbSet<ApprovalFlowInstance> ApprovalFlowInstances { get; set; }

        /// <summary>
        /// 审批记录表
        /// </summary>
        public DbSet<ApprovalRecord> ApprovalRecords { get; set; }

        /// <summary>
        /// 实例表单数据表
        /// </summary>
        public DbSet<InstanceFormData> InstanceFormDatas { get; set; }

        /// <summary>
        /// 表单字段值表
        /// </summary>
        public DbSet<FormFieldValue> FormFieldValues { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">数据库上下文选项</param>
        public ApFlowDbContext(DbContextOptions<ApFlowDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// 配置模型
        /// </summary>
        /// <param name="modelBuilder">模型构建器</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 应用所有实体配置
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApFlowDbContext).Assembly);
        }
    }
}