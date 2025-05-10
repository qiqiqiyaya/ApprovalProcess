namespace Ap.Share.Entities
{
	public class NextApproverEntity
	{
		public string Id { get; set; }

		/// <summary>
		/// employee or organization
		/// </summary>
		public string ApproverId { get; set; }

		/// <summary>
		/// define approver <see cref="ApproverId"/>
		/// </summary>
		public string Definition { get; set; }

		public string TriggeredRecordId { get; set; }
	}
}
