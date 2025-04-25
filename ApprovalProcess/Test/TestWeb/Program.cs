using Microsoft.EntityFrameworkCore;
using Test.Repository;

namespace TestWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateSlimBuilder(args);

        builder.Services.AddDbContext<TestDbContext>(options =>
        {
            options.UseInMemoryDatabase("MyDatabase");
            options.UseSeeding((dbContext, a) =>
            {
                if (dbContext is TestDbContext db)
                {
                    SeedData.Initialize(db);
                }
            });
        });


        var app = builder.Build();

        app.Run();
    }
}

