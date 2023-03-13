using Voyager.HealthEndpoint.Interface;

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
      string contentTxt = await PytanieKlienta("/health");
      Assert.That(contentTxt, Is.EqualTo("Ok"));
    }

    [Test]
    public virtual async Task CheckIsReady()
    {
      string contentTxt = await PytanieKlienta("/health/readiness");
      Assert.That(contentTxt, Is.EqualTo("Ok"));
    }


    [Test]
    public virtual async Task SqlName()
    {
      string contentTxt = await PytanieKlienta("/sqlname");
      Assert.That(contentTxt, Is.EqualTo("There is no datastore!"));
    }


    protected async Task<string> PytanieKlienta(string path, int expectedCode = 200)
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

  internal class WebStart : ClientCall
  {

   // Task webServicetask;
    WebApplication app;

    [SetUp]
    public void AppSetup()
    {
      var builder = WebApplication.CreateBuilder();
      AddMyServicess(builder);
      app = builder.Build();
      app.MapVoyHealth();
      app.MapGet("/", () => "Is running");
      app.RunAsync(GetUrl());
    }



    [TearDown]
    public async Task AppClose()
    {
      await app.DisposeAsync();
    }

    protected virtual void AddMyServicess(WebApplicationBuilder builder)
    {
      builder.Services.AddHealthServices();
    }
  }



  internal class WebStartSilinet : WebStart
  {
    protected override void AddMyServicess(WebApplicationBuilder builder)
    {
      builder.Services.AddHealthServicesSilient();
    }
  }

  internal class StoreNotRaedyStartSilinet : WebStart
  {
    protected override void AddMyServicess(WebApplicationBuilder builder)
    {
      builder.Services.AddHealthServicesSilient();
      builder.Services.AddTransient<AppStatus, AppNotReady>();
    }

    [Test]
    public override async Task CheckIsReady()
    {
      string contentTxt = await PytanieKlienta("/health/readiness", 503);
      Assert.That(contentTxt, Is.EqualTo("AppNotReady"));
    }

    [Test]
    public override async Task SqlName()
    {
      string contentTxt = await PytanieKlienta("/sqlname", 503);
      Assert.That(contentTxt, Is.EqualTo("AppNotReady"));
    }

    public class AppNotReady : DefaultDatastore
    {
      public override Task Read()
      {
        throw new Exception("AppNotReady");
      }

      public override Task<string> StoreName()
      {
        throw new Exception("AppNotReady");
      }
    }
  }
}
