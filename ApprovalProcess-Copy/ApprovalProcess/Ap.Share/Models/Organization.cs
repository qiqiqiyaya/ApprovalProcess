using System.Collections.Generic;

namespace Ap.Share.Models
{
	public class Organization
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string ParentId { get; set; }

		public List<Manager> Managers { get; set; } = new List<Manager>();
	}
}
