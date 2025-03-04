using Voyager.HealthEndpoint.Interface;
using Voyager.UnitTestLogger;

namespace Voyager.HealthEndpoint.Test
{


	internal class HealthActionsLoggerTest : AppStatus, RemoteAddress
	{
		HealthActionsLogger service;
		SpyLog<HealthActionsLogger> logger;

		[SetUp]
		public void Setup()
		{
			this.logger = new SpyLog<HealthActionsLogger>();
			service = new HealthActionsLogger(this, logger, this);
		}

		[Test]
		public void GetSomething()
		{
			var output = service.GetSomething();
			Assert.That(output, Is.EqualTo("Ok"));
			Assert.That(logger.GetSpyData().First().LogLevel, Is.EqualTo(LogLevel.Debug));
		}

		[Test]
		public async Task CallIntergrationTest()
		{
			var output = await service.GetIntegrationTestAsync();
			Assert.That(output, Is.EqualTo("Ok"));
			Assert.That(logger.GetSpyData().First().LogLevel, Is.EqualTo(LogLevel.Debug));
		}

		[Test]
		public async Task CheckStoreName()
		{
			var output = await service.GetSourceNameAsync();
			Assert.That(output, Is.EqualTo("Test flat file"));
			Assert.That(logger.GetSpyData().First().LogLevel, Is.EqualTo(LogLevel.Debug));
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
