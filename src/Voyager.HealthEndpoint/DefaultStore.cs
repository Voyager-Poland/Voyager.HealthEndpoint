using Voyager.HealthEndpoint.Interface;

namespace Voyager.HealthEndpoint
{
  public class DefaultDatastore : AppStatus
  {

    public virtual Task Read()
    {
      return Task.CompletedTask;
    }

    public virtual Task<string> StoreName()
    {
      return Task.FromResult("There is no datastore!");
    }
  }
}
