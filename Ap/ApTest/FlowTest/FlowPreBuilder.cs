﻿using Ap.Core.Builders;

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

            builder.ConfigureEntry("edit", context =>
            {

            });

            builder.ConfigureEntry("edit", context =>
            {

            });

            builder.AssignApproverService("FirstApprove", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "11" });
            });
            builder.AssignApproverService("SecondApprove", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "22" });
            });
            builder.AssignApproverService("ThirdApprove", context =>
            {
                return new ValueTask<List<string>>(new List<string>() { "33" });
            });

            return builder;
        }
    }
}
