using System.Collections.Generic;

namespace Ap.Share.Entities
{
    public class OrganizationEntity
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public ICollection<ManagerEntity> Managers { get; set; } = new List<ManagerEntity>();
    }
}
