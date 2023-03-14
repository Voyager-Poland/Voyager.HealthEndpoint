namespace Voyager.HealthEndpoint.Test
{
	internal class WebStartSilinet : WebStart
	{
		protected override void AddMyServicess(WebApplicationBuilder builder)
		{
			builder.Services.AddHealthServicesSilient();
		}
	}
}
