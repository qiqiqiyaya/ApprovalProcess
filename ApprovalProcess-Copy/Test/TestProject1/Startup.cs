using Ap.Register;
using Microsoft.Extensions.Hosting;
using Sm.Repository.EfSqlserver;

namespace TestProject1
{
	internal class Startup
	{
		public void ConfigureHost(IHostBuilder hostBuilder)
		{
			hostBuilder.ConfigureServices(services =>
			{
				services.AddAp();
				services.AddSmSqlserverRepository();
			});
		}
	}
}
