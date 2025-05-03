namespace Ap.Flow.Share.ApFlowModels
{
	public interface IEmployee
	{
		string Code { get; set; }

		IOrganization Organization { get; set; }
	}
}
