using Voyager.HealthEndpoint.Interface;

namespace Voyager.HealthEndpoint.Test
{
	internal class StoreNotRaedyStartSilinet : WebStart
	{
		protected override void AddMyServicess(WebApplicationBuilder builder)
		{
			builder.Services.AddHealthServicesSilient();
			builder.Services.AddTransient<AppStatus, AppNotReady>();
		}

		[Test]
		public override async Task CheckIsReady()
		{
			string contentTxt = await ClientReqest("/health/readiness", 503);
			Assert.IsTrue(contentTxt.Contains("AppNotReady"));
		}

		[Test]
		public override async Task SqlName()
		{
			string contentTxt = await ClientReqest("/sqlname", 503);
			Assert.That(contentTxt, Is.EqualTo("AppNotReady"));
		}

		public class AppNotReady : DefaultDatastore
		{
			public override Task Read()
			{
				throw new Exception("AppNotReady");
			}

			public override Task<string> StoreName()
			{
				throw new Exception("AppNotReady");
			}
		}
	}
}
