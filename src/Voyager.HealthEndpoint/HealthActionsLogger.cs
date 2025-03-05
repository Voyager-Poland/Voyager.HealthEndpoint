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
			logger?.LogDebug($"Endpoint Liveness  {remoteAddress.Get()}");
			return base.GetSomething();
		}

		public override async Task<string> GetIntegrationTestAsync()
		{
			using var bs = logger?.BeginScope("GetIntegrationTest");
			logger?.LogDebug($"Endpoint Readiness  {remoteAddress.Get()}");
			return await base.GetIntegrationTestAsync();
		}

		public override async Task<string> GetSourceNameAsync()
		{
			using var bs = logger?.BeginScope("GetSourceName");
			string name = await base.GetSourceNameAsync();
			logger?.LogDebug($"Storname {name}  {remoteAddress.Get()} ");
			return name;
		}
	}

}
