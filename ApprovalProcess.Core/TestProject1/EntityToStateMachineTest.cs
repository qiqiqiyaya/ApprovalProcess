using ApprovalProcess.Core;
using ApprovalProcess.Core.ConvertActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
	public class EntityToStateMachineTest : BaseTest
	{
		[Fact]
		public async Task ToTransition()
		{
			var smManger = GetRequiredService<IStateMachineManager>();
			var container = GetRequiredService<ConvertContainer>();


			var sr = await smManger.GetSr("2");

			var converter = container.Get<string, string>();

			var trList = sr.Transitions.Select(s => converter.To(s)).ToList();

			var srt = new StateRepresentation<string, string>(converter.ToState(sr.State), trList);


		}
	}
}
