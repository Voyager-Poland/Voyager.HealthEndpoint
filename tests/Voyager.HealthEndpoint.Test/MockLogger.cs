using Microsoft.Extensions.Logging;

namespace Voyager.HealthEndpoint.Test
{
  class MockLogger<TCategoryName> : TestScopeProcess, ILogger<TCategoryName>
  {
    public IDisposable BeginScope<TState>(TState state)
    {
      return new LogScope(this);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
      return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
      Console.WriteLine(GetSpacess() + formatter.Invoke(state, null));
    }

    int spacesCount = 0;

    string GetSpacess()
    {
      string s = "";
      for (int i = 0; i < spacesCount; i++)
        s += " ";
      return s;

    }


    public void ScopeEnter()
    {
      spacesCount += 2; 
    }
    public void ScopeExit()
    {
      spacesCount -= 2; 
    }


    class LogScope : IDisposable
    {
      private readonly TestScopeProcess owner;

      public LogScope(TestScopeProcess owner)
      {
        this.owner = owner;
        this.owner.ScopeEnter();
      }

      public void Dispose()
      {
        this.owner.ScopeExit();
      }
    }
  }

  internal interface TestScopeProcess
  {
    void ScopeEnter();
    public void ScopeExit();
  }

}
