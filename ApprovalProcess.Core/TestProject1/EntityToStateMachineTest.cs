using ApprovalProcess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalProcess.Core.Converts.ToTransitions;

namespace TestProject1
{
    public class EntityToStateMachineTest : BaseTest
    {
        [Fact]
        public async Task ToTransition()
        {
            var smManger = GetRequiredService<IStateMachineLoader>();
            var container = GetRequiredService<ToTransitionContainer>();


            var sr = await smManger.GetSr("2");

            var converter = container.Get<string, string>();

            var trList = sr.Transitions.Select(s => converter.To(s)).ToList();

            var srt = new StateSettings<string, string>(converter.ToState(sr.State), trList);


        }
    }
}
