using Ap.Core.Builders;

namespace ApTest.FlowTest
{
    public class FlowPreBuilder : IPreBuilder
    {
        public IStateSetBuilder Build(IStateSetBuilderProvider builderProvider)
        {
            var builder = builderProvider.Create("edit");

            builder.Then("FirstApprove")
                .Then("SecondApprove")
                .Then("ThirdApprove");

            builder.Name = "FlowTest";

            return builder;
        }
    }
}
