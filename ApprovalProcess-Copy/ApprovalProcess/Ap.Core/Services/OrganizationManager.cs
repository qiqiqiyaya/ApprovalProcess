using Ap.Share.Models;
using Ap.Share.Services;
using System.Threading.Tasks;

namespace Ap.Core.Services
{
    public class OrganizationManager : IOrganizationManager
    {
        public OrganizationManager() { }

        public ValueTask<Organization> GetPreviousOrg(string parentCode)
        {
            return new ValueTask<Organization>(new Organization());
        }
    }
}
