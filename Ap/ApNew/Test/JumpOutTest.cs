using ApNew.Nodes.Behaviours;
using ApNew.Nodes.Builders;
using ApNew.Nodes.Core;

namespace ApNew.Test
{
    public class JumpOutTest
    {
        public static void Test()
        {
            StateSetBuilderProvider provider = new StateSetBuilderProvider();

            var builder = provider.Create("edit");

            builder.Then("FirstApprove")
                .Jump("SecondApprove", "FifthApprove")
                .Children(containerBuilder =>
                {
                    containerBuilder.New("aaa")
                        .JumpOut("bbb", "FifthApprove");
                })
                .Then("FourthApprove")
                .Then("FifthApprove")
                .Complete();

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            Console.WriteLine("IsEnd:" + stateSet.CurrentState);
            var dictionary = stateSet.GetTrigger();
            var approve = dictionary[TransitionConst.Approve];

            stateSet.ExecuteTrigger(approve.ToParameter());
            Console.WriteLine("IsEnd:" + stateSet.CurrentState);
            dictionary = stateSet.GetTrigger();
            var cc = dictionary[TransitionConst.Jump];
            var parameter = cc.ToParameter();

            stateSet.ExecuteTrigger(parameter);
            dictionary = stateSet.GetTrigger();
            Console.WriteLine("IsEnd:" + stateSet.CurrentState);
        }
    }
}
