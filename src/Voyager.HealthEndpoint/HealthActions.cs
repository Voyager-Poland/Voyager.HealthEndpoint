﻿using Voyager.HealthEndpoint.Interface;

namespace Voyager.HealthEndpoint
{
	public class HealthActions
	{
		private readonly AppStatus appStatus;

		public HealthActions(AppStatus appStatus)
		{
			this.appStatus = appStatus;
		}

		public virtual string GetSomething()
		{
			return "Ok";
		}

		public virtual async Task<string> GetIntegrationTestAsync()
		{
			await appStatus.ReadAsync().ConfigureAwait(false);
			return "Ok";
		}

		public virtual Task<string> GetSourceNameAsync()
		{
			return appStatus.StoreNameAsync();
		}
	}

}
