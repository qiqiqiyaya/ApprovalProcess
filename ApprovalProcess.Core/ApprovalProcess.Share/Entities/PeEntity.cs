namespace ApprovalProcess.Share.Entities
{
    public class PeEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        public int EmployeeId { get; set; }

        public double Score { get; set; } = 0;

        public int WorkflowId { get; set; }

        public static PeEntity Create(int employeeId)
        {
            return new PeEntity() { EmployeeId = employeeId };
        }
    }
}
