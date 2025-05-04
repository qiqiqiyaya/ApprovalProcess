using System.Threading.Tasks;
using Ap.Share.ApFlowModels;
using Ap.Share.Models;

namespace Ap.Share.Services
{
    public class OrganizationManager : IOrganizationManager
    {
        public OrganizationManager() { }

        public ValueTask<IOrganization> GetPreviousOrg(string parentCode)
        {
            return new ValueTask<IOrganization>(new OrganizationEntity()
            {
                Code = "2",
                Name = "Next Org",
                ParentCode = "0"
            });
        }
    }
}
