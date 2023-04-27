namespace Voyager.HealthEndpoint.Test
{
	internal abstract class ClientCall
	{
		private HttpClient client;

		[SetUp]
		public void ClietnSetup()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri(GetUrl());
		}

		[TearDown]
		public void ClietnDown()
		{
			client.Dispose();
		}

		[Test]
		public async Task CheckHelth()
		{
			string contentTxt = await ClientReqest("/health");
			Assert.That(contentTxt, Is.EqualTo("Ok"));
		}

		[Test]
		public virtual async Task CheckIsReady()
		{
			string contentTxt = await ClientReqest("/health/readiness");
			Assert.That(contentTxt, Is.EqualTo("Ok"));
		}

		[Test]
		public virtual async Task SqlName()
		{
			string contentTxt = await ClientReqest("/sqlname");
			Assert.That(contentTxt, Is.EqualTo("There is no datastore!"));
		}

		[Test]
		public virtual async Task AppName()
		{
			string contentTxt = await ClientReqest("/");
			Assert.That(contentTxt, Is.EqualTo("AppName"));
		}


		protected async Task<string> ClientReqest(string path, int expectedCode = 200)
		{
			var response = await client.GetAsync(path);
			var contentTxt = await response.Content.ReadAsStringAsync();
			Assert.That((int)response.StatusCode, Is.EqualTo(expectedCode));
			return contentTxt;
		}

		protected virtual string GetUrl()
		{
			return "http://localhost:4500";
		}
	}
}
