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
			var output = await service.GetIntegrationTest();
			Assert.That(output, Is.EqualTo("Ok"));
		}

		[Test]
		public async Task CheckStoreName()
		{
			var output = await service.GetSourceName();
			Assert.That(output, Is.EqualTo("Test flat file"));
		}


		public Task<string> StoreName()
		{
			return Task.FromResult("Test flat file");
		}

		public string Get()
		{
			return "10.45.32.22";
		}

		public Task Read()
		{
			return Task.CompletedTask;
		}
	}





}
