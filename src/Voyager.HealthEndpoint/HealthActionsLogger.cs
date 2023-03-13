using Voyager.HealthEndpoint.Interface;

namespace Voyager.HealthEndpoint
{
	public class HealthActionsLogger : HealthActions
	{
		private readonly ILogger logger;
		private readonly RemoteAddress remoteAddress;

		public HealthActionsLogger(AppStatus datastore, ILogger<HealthActionsLogger> logger, RemoteAddress remoteAddr) : base(datastore)
		{
			this.logger = logger;
			this.remoteAddress = remoteAddr;
		}

		public override string GetSomething()
		{
			logger?.LogInformation($"Endpoint Liveness  {remoteAddress.Get()}");
			return base.GetSomething();
		}

		public override async Task<string> GetIntegrationTest()
		{
			using var bs = logger?.BeginScope("GetIntegrationTest");
			logger?.LogInformation($"Endpoint Readiness  {remoteAddress.Get()}");
			return await base.GetIntegrationTest();
		}

		public override async Task<string> GetSourceName()
		{
			using var bs = logger?.BeginScope("GetSourceName");
			string name = await base.GetSourceName();
			logger?.LogInformation($"Storname {name}  {remoteAddress.Get()} ");
			return name;
		}
	}

}
