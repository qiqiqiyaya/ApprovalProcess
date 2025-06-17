using Ap.Core.Builders;

namespace ApTest.FlowTest
{
    public class FlowPreBuilder : IPreBuilder
    {
        public const string FlowName = "FlowTest";

        public IStateSetBuilder Build(IStateSetBuilderProvider builderProvider)
        {
            var builder = builderProvider.Create("edit");

            builder.Id = "1";
            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .Then("ThirdApprove");

            builder.Name = FlowName;

            builder.EntryAction("edit", context =>
            {

            });

            builder.EntryAction("edit", context =>
            {

            });

            builder.AssignApprover("FirstApprove", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "11" });
            });
            builder.AssignApprover("SecondApprove", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "22" });
            });
            builder.AssignApprover("ThirdApprove", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "33" });
            });

            return builder;
        }
    }
}
