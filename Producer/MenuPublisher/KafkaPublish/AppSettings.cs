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
    public class AppSettings : IAppSettings
    {
        private static AppSettings _instance;
        private static readonly object ObjLocked = new object();
        private IConfiguration _configuration;
       public void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static AppSettings Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (ObjLocked)
                    {
                        if (null == _instance)
                            _instance = new AppSettings();
                    }
                }
                return _instance;
            }
        }
        public T Get<T>(string key = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                return _configuration.Get<T>();
            else
                return _configuration.GetSection(key).Get<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetConfigValue()
        {
            return new Dictionary<string, object>
            {
                { KafkaPropNames.BootstrapServers, AppSettings.Instance.Get<string>("KafkaconnectionStrings:BootstrapServers:kafkaPropNames.BootstrapServers")},
                { KafkaPropNames.SchemaRegistryUrl, AppSettings.Instance.Get<string>("KafkaconnectionStrings:SchemaRegistryUrl:KafkaPropNames.SchemaRegistryUrl")},
                { KafkaPropNames.Topic, AppSettings.Instance.Get<string>("KafkaconnectionStrings:Topic:KafkaPropNames.Topic") },
                { KafkaPropNames.GroupId, AppSettings.Instance.Get<string>("KafkaconnectionStrings:GroupId:KafkaPropNames.GroupId") },
                { KafkaPropNames.Partition,AppSettings.Instance.Get<string>("KafkaconnectionStrings:Partition:KafkaPropNames.Partition")},
                { KafkaPropNames.Offset, AppSettings.Instance.Get<string>("KafkaconnectionStrings:Offset:KafkaPropNames.Offset")},
            };

        }
    }
}
