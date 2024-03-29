﻿namespace Voyager.HealthEndpoint.Test
{
	internal class StoreNotRaedyStartSilinet : WebStart
	{
		protected override void AddMyServicess(WebApplicationBuilder builder)
		{
			builder.Services.AddHealthServicesSilient().AddAppStatus<AppNotReady>();
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
			public override Task ReadAsync()
			{
				throw new Exception("AppNotReady");
			}

			public override Task<string> StoreNameAsync()
			{
				throw new Exception("AppNotReady");
			}
		}
	}
}
