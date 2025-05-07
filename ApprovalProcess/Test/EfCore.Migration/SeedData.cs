using Ap.Repository.EfSqlserver;
using Ap.Share.Entities;

namespace EfCore.Migration
{
    public class SeedData
    {
        public static void Initialize(ApDbContext context)
        {
            var org1 = new OrganizationEntity { Id = Guid.NewGuid().ToString("N"), Name = "Organization 1" };
            context.Organizations.Add(org1);
            var org2 = new OrganizationEntity { Id = Guid.NewGuid().ToString("N"), Name = "Organization 2", ParentId = org1.Id };
            context.Organizations.Add(org2);
            var org3 = new OrganizationEntity { Id = Guid.NewGuid().ToString("N"), Name = "Organization 3", ParentId = org2.Id };
            context.Organizations.Add(org3);
            var org4 = new OrganizationEntity { Id = Guid.NewGuid().ToString("N"), Name = "Organization 4", ParentId = org3.Id };
            context.Organizations.Add(org4);
            var org5 = new OrganizationEntity { Id = Guid.NewGuid().ToString("N"), Name = "Organization 5", ParentId = org2.Id };
            context.Organizations.Add(org5);

            var aaa = new EmployeeEntity { Id = Guid.NewGuid().ToString("N"), Name = "AAAAAAAAAAAA", OrganizationId = org1.Id };
            context.Employees.Add(aaa);
            var bbb = new EmployeeEntity { Id = Guid.NewGuid().ToString("N"), Name = "BBBBBBBBBBBB", OrganizationId = org2.Id };
            context.Employees.Add(bbb);
            var ccc = new EmployeeEntity { Id = Guid.NewGuid().ToString("N"), Name = "CCCCCCCCCCCCCCCC", OrganizationId = org3.Id };
            context.Employees.Add(ccc);
            var ddd = new EmployeeEntity { Id = Guid.NewGuid().ToString("N"), Name = "DDDDDDDDDDDDDDDD", OrganizationId = org4.Id };
            context.Employees.Add(ddd);
            var eee = new EmployeeEntity { Id = Guid.NewGuid().ToString("N"), Name = "EEEEEEEEEEEEEEE", OrganizationId = org5.Id };
            context.Employees.Add(eee);
            var fff = new EmployeeEntity { Id = Guid.NewGuid().ToString("N"), Name = "FFFFFFFFFFFFFFF", OrganizationId = org5.Id };
            context.Employees.Add(eee);

            context.Managers.Add(new ManagerEntity { Id = Guid.NewGuid().ToString("N"), EmployeeId = aaa.Id, Organization = org1, OrganizationId = org1.Id });
            context.Managers.Add(new ManagerEntity { Id = Guid.NewGuid().ToString("N"), EmployeeId = bbb.Id, Organization = org2, OrganizationId = org2.Id });
            context.Managers.Add(new ManagerEntity { Id = Guid.NewGuid().ToString("N"), EmployeeId = ccc.Id, Organization = org3, OrganizationId = org3.Id });
            context.Managers.Add(new ManagerEntity { Id = Guid.NewGuid().ToString("N"), EmployeeId = ddd.Id, Organization = org4, OrganizationId = org4.Id });
            context.Managers.Add(new ManagerEntity { Id = Guid.NewGuid().ToString("N"), EmployeeId = eee.Id, Organization = org5, OrganizationId = org5.Id });
            context.Managers.Add(new ManagerEntity { Id = Guid.NewGuid().ToString("N"), EmployeeId = fff.Id, Organization = org5, OrganizationId = org5.Id });

            context.SaveChanges();
        }
    }
}
