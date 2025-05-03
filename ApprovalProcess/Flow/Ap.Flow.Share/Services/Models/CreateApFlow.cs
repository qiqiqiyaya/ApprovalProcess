using Ap.Flow.Share.Actions.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Ap.Flow.Share.Services.Models
{
	public class CreateApFlow
	{
		public ActionRecord Action { get; set; }

		public IActionParameter Parameter { get; set; }
	}
}
