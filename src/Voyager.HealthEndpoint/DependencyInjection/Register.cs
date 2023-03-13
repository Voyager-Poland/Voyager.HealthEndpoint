using Voyager.HealthEndpoint;
using Voyager.HealthEndpoint.Interface;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class Register
  {
    public static IServiceCollection AddHealthServicesSilient(this IServiceCollection services)
    {
      services.AddTransient<AppStatus, DefaultDatastore>();
      services.AddTransient<HealthActions>();
      return services;
    }

    public static IServiceCollection AddHealthServices(this IServiceCollection services)
    {
      services.AddHealthServicesSilient();
      services.AddTransient<HealthActions, HealthActionsLogger>();
      services.AddTransient<RemoteAddress, HttpRemoteAddress>();
      services.AddHttpContextAccessor();
      return services;
    }

  }
}
