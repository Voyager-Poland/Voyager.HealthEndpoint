using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voyager.HealthEndpoint.Interface
{
  public interface AppStatus
  {
    Task Read();
    Task<string> StoreName();
  }
}
