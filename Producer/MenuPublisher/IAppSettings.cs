using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPublisher
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppSettings
    {
        void SetConfiguration(IConfiguration configuration);
        Dictionary<string, object> GetConfigValue();
    }
}
