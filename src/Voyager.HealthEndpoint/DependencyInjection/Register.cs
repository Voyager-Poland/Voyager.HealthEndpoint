using System.Diagnostics.CodeAnalysis;
using Voyager.HealthEndpoint;
using Voyager.HealthEndpoint.Interface;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class Register
	{
		public static HealthServiceBuilder AddHealthServicesSilient(this IServiceCollection services)
		{
			services.AddTransient<AppStatus, DefaultDatastore>();
			services.AddTransient<HealthActions>();

			return new HealthServiceBuilder(services);
		}

		public static HealthServiceBuilder AddHealthServices(this IServiceCollection services)
		{
			var builder = services.AddHealthServicesSilient();
			builder.Services.AddTransient<HealthActions, HealthActionsLogger>();
			builder.Services.AddTransient<RemoteAddress, HttpRemoteAddress>();
			builder.Services.AddHttpContextAccessor();
			return builder;
		}

		public static HealthServiceBuilder AddAppStatus<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this HealthServiceBuilder builder)
							where TImplementation : AppStatus
		{
			builder.Services.AddTransient(typeof(AppStatus), typeof(TImplementation));
			return builder;
		}

		public static HealthServiceBuilder AddAppStatus(this HealthServiceBuilder builder, Func<IServiceProvider, AppStatus> implementationFactory)
		{
			builder.Services.AddTransient(typeof(AppStatus), implementationFactory);
			return builder;
		}
	}
}
