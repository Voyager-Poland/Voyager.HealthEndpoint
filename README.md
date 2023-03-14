<!--<p align="center">
  <a href="" rel="noopener">
 <img width=200px height=200px src="https://i.imgur.com/6wj0hh6.jpg" alt="Project logo"></a>
</p>


-->
<h1><img src="./img/voyager-nugets-ikona-32x32.png" style="vertical-align:bottom;margin:0px 5px">Voyager.HealthEndpoint</h1>

---

<p> The extension for AspNetCore provides endpoints for sampling the health of the hosted application. 
</p>

## 📝 Table of Contents

- [About](#about)
- [Getting Started](#getting_started)
- [How configure test health check](#health)
- [How to test readiness](#readiness)
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
      app.MapHealth("/health");
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

The new class have to be registred in DI

using Microsoft.Extensions.DependencyInjection;
...
    public void ConfigureServices(IServiceCollection services)
    {
      ...
      services.AddHealthServices();
    }
    ...
```
Give examples
```

### Installing

A step by step series of examples that tell you how to get a development env running.

Say what the step will be

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo.

## 🔧 Running the tests <a name = "tests"></a>

Explain how to run the automated tests for this system.

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

## 🎈 Usage <a name="usage"></a>

Add notes about how to use the system.

## 🚀 Deployment <a name = "deployment"></a>

Add additional notes about how to deploy this on a live system.

## ⛏️ Built Using <a name = "built_using"></a>

- [MongoDB](https://www.mongodb.com/) - Database
- [Express](https://expressjs.com/) - Server Framework
- [VueJs](https://vuejs.org/) - Web Framework
- [NodeJs](https://nodejs.org/en/) - Server Environment

## ✍️ Authors <a name = "authors"></a>

- [@kylelobo](https://github.com/kylelobo) - Idea & Initial work

See also the list of [contributors](https://github.com/kylelobo/The-Documentation-Compendium/contributors) who participated in this project.

## 🎉 Acknowledgements <a name = "acknowledgement"></a>

- Hat tip to anyone whose code was used
- Inspiration
- References
