namespace Ap.Flow.Share.Actions.Models
{
	public class ExecutableActionRecord
	{
		public ActionRecord Action { get; set; }

		public IActionParameter Parameter { get; set; }
	}
}
