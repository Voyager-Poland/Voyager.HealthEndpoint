namespace Microsoft.Extensions.DependencyInjection
{
	/*
	 * 
	 * */

	public class HealthServiceBuilder
	{
		internal HealthServiceBuilder(IServiceCollection services)
		{
			this.Services = services;
		}
		public IServiceCollection Services { get; }
	}
}
