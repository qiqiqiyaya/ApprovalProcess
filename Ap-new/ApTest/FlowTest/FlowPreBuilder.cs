using Ap.Core.Builders;

namespace ApTest.FlowTest
{
    public class FlowPreBuilder : IPreBuilder
    {
        public const string FlowName = "FlowTest";

        public IStateSetBuilder Build(IStateSetBuilderProvider builderProvider)
        {
            var builder = builderProvider.Create("edit");

            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .Then("ThirdApprove");

            builder.Name = FlowName;

            return builder;
        }
    }
}
