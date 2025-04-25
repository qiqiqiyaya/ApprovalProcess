using System.Collections.Generic;

namespace Test.Share.Entities
{
    public class OrganizationEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public ICollection<ManagerEntity> Managers { get; set; } = new List<ManagerEntity>();
    }
}
