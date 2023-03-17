using System.Net;
using Voyager.HealthEndpoint;

namespace Microsoft.AspNetCore.Builder
{
	public static class BuilderHealth
	{
		public static IEndpointRouteBuilder MapVoyHealth(this IEndpointRouteBuilder endpointRoute)
		{
			endpointRoute.MapHealth();
			endpointRoute.MapReadiness();
			endpointRoute.MapSourceName();
			return endpointRoute;
		}

		public static IEndpointRouteBuilder MapHealth(this IEndpointRouteBuilder endpointRoute, string path = "/health")
		{
			endpointRoute.MapGet(path, () =>
			{
				using var scope = endpointRoute.ServiceProvider.CreateScope();
				var akcja = scope.ServiceProvider.GetService<HealthActions>();
				return akcja!.GetSomething();
			});
			return endpointRoute;
		}

		public static IEndpointRouteBuilder MapSourceName(this IEndpointRouteBuilder endpointRoute, string path = "/sqlname")
		{
			endpointRoute.MapGet(path, async (http) =>
			{
				try
				{
					await SourceNameCall(endpointRoute, http).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					await ProcessException(http, ex);
				}
			});
			return endpointRoute;
		}

		public static IEndpointRouteBuilder MapReadiness(this IEndpointRouteBuilder endpointRoute, string path = "/health/readiness")
		{
			endpointRoute.MapGet(path, (http) =>
			{
				try
				{
					Task task = IntegrationTest(endpointRoute, http);
					task.Wait();
					return task;
				}
				catch (Exception ex)
				{
					Task task = ProcessException(http, ex);
					task.Wait();
					return task;
				}
			});
			return endpointRoute;
		}

		private static async Task ProcessException(HttpContext http, Exception ex)
		{
			http.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
			await http.Response.WriteAsync(ex.Message);
		}

		private static async Task IntegrationTest(IEndpointRouteBuilder endpointRoute, HttpContext http)
		{
			using var scope = endpointRoute.ServiceProvider.CreateScope();
			var akcja = scope.ServiceProvider.GetService<HealthActions>();
			await http.Response.WriteAsync(await akcja!.GetIntegrationTest());
		}

		private static async Task SourceNameCall(IEndpointRouteBuilder endpointRoute, HttpContext http)
		{
			using var scope = endpointRoute.ServiceProvider.CreateScope();
			var akcja = scope.ServiceProvider.GetService<HealthActions>();
			await http.Response.WriteAsync(await akcja!.GetSourceName());
		}


	}
}
