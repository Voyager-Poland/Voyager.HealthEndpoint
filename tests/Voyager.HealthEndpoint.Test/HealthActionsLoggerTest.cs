using Voyager.HealthEndpoint.Interface;

namespace Voyager.HealthEndpoint.Test
{
	internal class HealthActionsLoggerTest : AppStatus, RemoteAddress
	{
		HealthActionsLogger service;

		[SetUp]
		public void Setup()
		{
			service = new HealthActionsLogger(this, new Voyager.UnitTestLogger.ConsoleLogger<HealthActionsLogger>(), this);
		}

		[Test]
		public void GetSomething()
		{
			var output = service.GetSomething();
			Assert.That(output, Is.EqualTo("Ok"));
		}

		[Test]
		public async Task CallIntergrationTest()
		{
			var output = await service.GetIntegrationTestAsync();
			Assert.That(output, Is.EqualTo("Ok"));
		}

		[Test]
		public async Task CheckStoreName()
		{
			var output = await service.GetSourceNameAsync();
			Assert.That(output, Is.EqualTo("Test flat file"));
		}


		public Task<string> StoreNameAsync()
		{
			return Task.FromResult("Test flat file");
		}

		public string Get()
		{
			return "10.45.32.22";
		}

		public Task ReadAsync()
		{
			return Task.CompletedTask;
		}
	}





}
