﻿using System.Net;
using Voyager.HealthEndpoint;

namespace Microsoft.AspNetCore.Builder
{
	public static class BuilderHealth
	{
		public static IEndpointRouteBuilder MapVoyHealth(this IEndpointRouteBuilder endpointRoute)
		{
			endpointRoute.MapHealth();
			endpointRoute.MapRediness();
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

		private static IEndpointRouteBuilder MapRediness(this IEndpointRouteBuilder endpointRoute, string path = "/health/readiness")
		{
			endpointRoute.MapGet(path, async (http) =>
			{
				try
				{
					await IntegrationTest(endpointRoute, http).ConfigureAwait(false);
				}
				catch (Exception ex)
				{
					await ProcessException(http, ex);
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