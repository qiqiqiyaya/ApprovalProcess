using Ap.Flow.Share.ApFlowModels;
using System.Threading.Tasks;

namespace Ap.Flow.Share.Services
{
	public class OrganizationManager : IOrganizationManager
	{
		public OrganizationManager() { }

		public ValueTask<IOrganization> GetPreviousOrg(string parentCode)
		{
			return new ValueTask<IOrganization>(new Organization()
			{
				Code = "2",
				Name = "Next Org",
				ParentCode = "0"
			});
		}
	}
}
