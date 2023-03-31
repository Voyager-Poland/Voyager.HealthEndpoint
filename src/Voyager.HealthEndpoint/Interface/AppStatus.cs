namespace Voyager.HealthEndpoint.Interface
{
	public interface AppStatus
	{
		Task ReadAsync();
		Task<string> StoreNameAsync();
	}
}
