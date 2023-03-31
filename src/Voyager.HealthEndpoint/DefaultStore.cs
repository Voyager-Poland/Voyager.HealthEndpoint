using Voyager.HealthEndpoint.Interface;

namespace Voyager.HealthEndpoint
{
	public class DefaultDatastore : AppStatus
	{

		public virtual Task ReadAsync()
		{
			return Task.CompletedTask;
		}

		public virtual Task<string> StoreNameAsync()
		{
			return Task.FromResult("There is no datastore!");
		}
	}
}
