using System;

namespace Ap.Share.Entities
{
	public class TriggeredRecordEntity
	{
		public string Id { get; set; }

		public string StateMachineId { get; set; }

		public string SourceState { get; set; }

		public string DtState { get; set; }

		public string Trigger { get; set; }

		/// <summary>
		/// 当前状态
		/// </summary>
		public string CurrentState { get; set; }

		public DateTime CreateTime { get; set; }
	}
}
