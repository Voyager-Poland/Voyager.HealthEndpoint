<h1><img src="./img/voyager-nugets-ikona-32x32.png" style="vertical-align:bottom;margin:0px 10px">Voyager.HealthEndpoint</h1>

---

<p> The extension for AspNetCore provides endpoints for sampling the health of the hosted application. 
</p>

## 📝 Table of Contents

- [About](#about)
- [Getting Started](#getting_started)
- [How configure health check](#health)
- [How to test readiness](#readiness)
- [How check configuration](#checkconfig)
- [Authors](#authors)
- [Acknowledgments](#acknowledgement)

## 🤨 About <a name = "about"></a>

Just after registering services and mapping the endpoint the service for a health check is ready. After simply adding support specific to the application, it allows testing the readiness of the application for the needs of traffic management services. The interface can be used by Kubernetes, Supervisor, or any solution that probes over HTTP. It could be used in any kind of .Net Core application that provides an HTTP interface.

The library is very light. It doesn't include any new dependencies except those abstract declarations that already have been included in projects supporting HTTP interfaces. Registration is carried out based on framework receipt so the new interface will have been seen by other standard tools like Swagger. 

The library doesn't require developers any specific for probing knowledge. It's just an extension that can be used by any kind of health check policy. Depending on an override of one method it can check the connection to any database, could test access to a filesystem, or an external connection, and check any kind of rules validating that the application is ready to work.

## 🏁 Getting Started <a name ="getting_started"></a>

### Prerequisites

The library coperate with the WebApplicationBuilder or HostApplicationBuilder. By the default, the hosts contain the required Dependency Injection.

## 🔧 How configure test health check  <a name = "#health"></a>

Adding the NuGet to a project:

```.NET CLI 
dotnet add package Voyager.HealthEndpoint
```

The default services are installed to the serivce catalog:

```C# 
// use the namespace
using Microsoft.Extensions.DependencyInjection;
...
    // in the configuration method
    public void ConfigureServices(IServiceCollection services)
    {
      ...
      // Add the line
      services.AddHealthServices();
    }
    ...
```
Is requered adding the endpoint mapping to the pipline:

```
// use the namespace
using  Microsoft.AspNetCore.Builder
...
  // in the method
  public void Configure(IApplicationBuilder app)
  {
    ...
    app.UseEndpoints(endpoints =>
    {
      // add this 
      endpoints.MapHealth("/health");
      ...
    });
  }
```

After running the application it is possible to start probing:

```cmd 
curl http://localhost:5200/health
```
## 🔧 How to test readiness <a name = "readiness"></a>

For testing, readiness is required to implement the Voyager.HealthEndpoint.Interface.AppStatus interface. The class has to call a procedure that processes normal routine or in case of any problems it has to throw an exception.
  
```
public class HealthProbe : Voyager.HealthEndpoint.Interface.AppStatus
{
	// It's a class with the logic used to check if is an available connection to this data store
  private readonly ServerNameStory serverNameStory;

  public HealthProbe(ServerNameStory serverNameStory)
  {
    this.serverNameStory = serverNameStory;
  }

  public async Task Read()
  {
    await serverNameStory.Name().ConfigureAwait(false);
  }

  public Task<string> StoreName()
  {
    return serverNameStory.Name();
  }
}
```

The new class have to be registred in DI:

```
using Microsoft.Extensions.DependencyInjection;
...
    public void ConfigureServices(IServiceCollection services)
    {
      ...
      services.AddHealthServices();
      services.AddTransient<Voyager.HealthEndpoint.Interface.AppStatus, HealthProbe>();
    }
    ...
```

Is required to add the new mapping:
```
using  Microsoft.AspNetCore.Builder
...
  // in the method
  public void Configure(IApplicationBuilder app)
  {
    ...
    app.UseEndpoints(endpoints =>
    {
      // add this 
      endpoints.MapReadiness("/health/readiness");
      ...
    });
  }
```

In case the class return an exception the service will return the HTTP code = 503.

## 🔧 How check configuration <a name = "checkconfig"></a>

There is another method that from practice is very useful. This is the method that returns the name of the data store. In an environment, the connection to the storage depends on a configuration, for example, a config map in Kubernatess, and sometimes environment variables, it's good to have the possibility to check that everything is ok and that the application uses the desired data storage. 

The implementation is in the class like above. It is only required to add the map for the new method.

```
using  Microsoft.AspNetCore.Builder
...
  // in the method
  public void Configure(IApplicationBuilder app)
  {
    ...
    app.UseEndpoints(endpoints =>
    {
      // add this 
      endpoints.MapSourceName("/sqlname");
      ...
    });
  }
```

## ✍️ Authors <a name = "authors"></a>

- [@andrzejswistowski](https://github.com/AndrzejSwistowski) - Idea & work. Please let me know if you find out an error or suggestions.

[contributors](https://github.com/Voyager-Poland).

## 🎉 Acknowledgements <a name = "acknowledgement"></a>

- Przemysław Wróbel - for the icon.
