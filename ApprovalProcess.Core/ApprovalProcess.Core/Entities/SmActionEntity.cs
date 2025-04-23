namespace ApprovalProcess.Core.Entities
{
	public class SmActionEntity : Entity
	{
		public string Name { get; set; }

		public string Description { get; set; }

		/// <summary>
		/// action class 
		/// </summary>
		public string Type { get; set; }
	}
}
