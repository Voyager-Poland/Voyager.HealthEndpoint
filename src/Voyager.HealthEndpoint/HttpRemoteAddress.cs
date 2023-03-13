namespace Voyager.HealthEndpoint
{
  public class HttpRemoteAddress : Interface.RemoteAddress
  {
    private readonly IHttpContextAccessor httpContextAccessor;

    public HttpRemoteAddress(IHttpContextAccessor httpContextAccessor)
    {
      this.httpContextAccessor = httpContextAccessor;
    }
    public string Get()
    {
      return httpContextAccessor!.HttpContext!.Connection.RemoteIpAddress.ToString(); 
    }
  }
}
