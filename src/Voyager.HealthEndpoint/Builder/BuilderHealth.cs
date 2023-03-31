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
					await SourceNameCallAsync(endpointRoute, http);
				}
				catch (Exception ex)
				{
					await ProcessExceptionAsync(http, ex);
				}

			});
			return endpointRoute;
		}

		public static IEndpointRouteBuilder MapReadiness(this IEndpointRouteBuilder endpointRoute, string path = "/health/readiness")
		{
			endpointRoute.MapGet(path, async (http) =>
			{
				try
				{
					await IntegrationTestAsync(endpointRoute, http);
				}
				catch (Exception ex)
				{
					await ProcessExceptionAsync(http, ex);
				}
			});
			return endpointRoute;
		}

		private static Task ProcessExceptionAsync(HttpContext http, Exception ex)
		{
			http.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
			return http.Response.WriteAsync(ex.Message);
		}

		private static async Task IntegrationTestAsync(IEndpointRouteBuilder endpointRoute, HttpContext http)
		{
			using var scope = endpointRoute.ServiceProvider.CreateScope();
			var actionService = scope.ServiceProvider.GetService<HealthActions>();
			await http.Response.WriteAsync(await actionService!.GetIntegrationTestAsync());
		}

		private static async Task SourceNameCallAsync(IEndpointRouteBuilder endpointRoute, HttpContext http)
		{
			using var scope = endpointRoute.ServiceProvider.CreateScope();
			var actionService = scope.ServiceProvider.GetService<HealthActions>();
			await http.Response.WriteAsync(await actionService!.GetSourceNameAsync());
		}


	}
}
