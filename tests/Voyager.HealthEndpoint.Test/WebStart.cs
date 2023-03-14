namespace Voyager.HealthEndpoint.Test
{
	internal class WebStart : ClientCall
	{

		// Task webServicetask;
		WebApplication app;

		[SetUp]
		public void AppSetup()
		{
			var builder = WebApplication.CreateBuilder();
			AddMyServicess(builder);
			app = builder.Build();
			app.MapVoyHealth();
			app.MapGet("/", () => "Is running");
			app.RunAsync(GetUrl());
		}



		[TearDown]
		public async Task AppClose()
		{
			await app.DisposeAsync();
		}

		protected virtual void AddMyServicess(WebApplicationBuilder builder)
		{
			builder.Services.AddHealthServices();
		}
	}
}
