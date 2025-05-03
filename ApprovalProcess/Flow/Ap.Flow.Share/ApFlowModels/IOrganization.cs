namespace Ap.Flow.Share.ApFlowModels
{
	public interface IOrganization
	{
		string Name { get; set; }

		string Code { get; set; }

		string ParentCode { get; set; }
	}
}
