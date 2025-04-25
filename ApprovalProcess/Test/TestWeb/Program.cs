using ApprovalProcess.Core.Workflows;
using ApprovalProcess.Service;
using System.Text.Json.Serialization;
using ApprovalProcess.Core;
using ApprovalProcess.Repository;
using Microsoft.EntityFrameworkCore;

namespace TestWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);

        builder.Services.AddDbContext<ApprovalProcessDbContext>(options =>
        {
            options.UseInMemoryDatabase("MyDatabase");
            options.UseSeeding((dbContext, a) =>
            {
                if (dbContext is ApprovalProcessDbContext db)
                {
                    SeedData.Initialize(db);
                }
            });
        });

        builder.Services.AddTransient<IPeService, PeService>();
        builder.Services.AddTransient<IWorkflow, PeWorkflow>();

        var app = builder.Build();

        app.Run();
    }
}

