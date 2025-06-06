using Ap.Core.Behaviours;
using Ap.Core.Builders;
using Ap.Core.Definitions;
using Ap.Core.Exceptions;
using Ap.Core.Extensions;

namespace ApTest
{
    public class ChildrenTest : Base
    {
        [Fact]
        public void Test1()
        {
            var provider = GetService<IStateSetBuilderProvider>();

            Assert.Throws<ApAlreadyExistsException<List<StateLinkedList>>>(() =>
            {
                var builder = provider.Create("edit");
                builder.Then("FirstApprove")
                    .Then("SecondApprove")
                    .Children(branch =>
                    {
                        branch.New("aaa");
                        branch.New("bbb");
                        branch.New("ccc");
                        branch.New("aaa");
                    })
                    .Then("ThirdApprove");
            });
        }

        [Fact]
        public void Test2()
        {
            var provider = GetService<IStateSetBuilderProvider>();

            var builder = provider.Create("edit");
            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .Children(branch =>
                {
                    branch.New("aaa");
                    branch.New("bbb");
                    branch.New("ccc");
                });

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            var child = stateSet.GetTrigger();
            var aTrigger = child.GetTrigger("aaa", TransitionConst.Approve);
            stateSet.ExecuteTrigger(aTrigger!);
            Assert.True(!stateSet.IsEnd);
            var bTrigger = child.GetTrigger("bbb", TransitionConst.Approve);
            stateSet.ExecuteTrigger(bTrigger!);
            Assert.True(!stateSet.IsEnd);
            var cTrigger = child.GetTrigger("ccc", TransitionConst.Approve);
            stateSet.ExecuteTrigger(cTrigger!);
            Assert.True(stateSet.IsEnd);
        }

        [Fact]
        public void Test3()
        {
            var provider = GetService<IStateSetBuilderProvider>();

            var builder = provider.Create("edit");
            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .Children(branch =>
                {
                    branch.New("aaa").Then("aaa1");
                    branch.New("bbb").Then("bbb2");
                    branch.New("ccc").Then("ccc2");
                });

            IStateSet stateSet = builder.Build();
            stateSet.ExecuteTrigger(TransitionConst.Submit);
            stateSet.ExecuteTrigger(TransitionConst.Approve);
            stateSet.ExecuteTrigger(TransitionConst.Approve);

            var child = stateSet.GetTrigger();
            var aTrigger1 = child.GetTrigger("aaa", TransitionConst.Approve);
            stateSet.ExecuteTrigger(aTrigger1!);
            child = stateSet.GetTrigger();
            var aTrigger2 = child.GetTrigger("aaa1", TransitionConst.Approve);
            stateSet.ExecuteTrigger(aTrigger2!);
            Assert.True(!stateSet.IsEnd);


            child = stateSet.GetTrigger();
            var bTrigger1 = child.GetTrigger("bbb", TransitionConst.Approve);
            stateSet.ExecuteTrigger(bTrigger1!);
            child = stateSet.GetTrigger();
            var bTrigger2 = child.GetTrigger("bbb2", TransitionConst.Approve);
            stateSet.ExecuteTrigger(bTrigger2!);
            Assert.True(!stateSet.IsEnd);

            child = stateSet.GetTrigger();
            var cTrigger1 = child.GetTrigger("ccc", TransitionConst.Approve);
            stateSet.ExecuteTrigger(cTrigger1!);
            child = stateSet.GetTrigger();
            var cTrigger2 = child.GetTrigger("ccc2", TransitionConst.Approve);
            stateSet.ExecuteTrigger(cTrigger2!);
            Assert.True(stateSet.IsEnd);
        }
    }
}
