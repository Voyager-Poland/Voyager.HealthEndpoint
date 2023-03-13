using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace Voyager.HealthEndpoint.Test
{
  public class Tests
  {
    HttpRemoteAddress remoteAddress;

    [SetUp]
    public void Setup()
    {
      remoteAddress = new HttpRemoteAddress(GetAccessor());
    }

    [Test]
    public void GetAddress()
    {
      var result = remoteAddress.Get();
      Assert.That(result, Is.EqualTo("10.0.200.13"));
    }

    private static HttpContextAccessor GetAccessor()
    {
      var context = new DefaultHttpContext();
      context.Features.Set<IHttpConnectionFeature>(new RemoteTestFeature());
      return new HttpContextAccessor()
      {
        HttpContext = context
      };
    }


    class RemoteTestFeature : HttpConnectionFeature
    {
      public RemoteTestFeature()
      {
        this.RemoteIpAddress = IPAddress.Parse("10.0.200.13");
      }

    }
  }
}