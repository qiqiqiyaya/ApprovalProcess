namespace Ap.Share.Entities
{
    public class ManagerEntity
    {
        public string Id { get; set; }

        public string EmployeeId { get; set; }

        public string OrganizationId { get; set; }

        public OrganizationEntity Organization { get; set; }
    }
}
