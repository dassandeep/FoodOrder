using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace ProducerConsoleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
                      
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    while (true)
                    {
                        Random rnd = new Random();
                        int randomlatitude = rnd.Next(0, 5);
                        int randomlong = rnd.Next(0, 5);
                        Location loc = new Location();
                        loc.lat = Convert.ToString(randomlatitude);
                        loc.lng = Convert.ToString(randomlong);
                        string s = JsonConvert.SerializeObject(loc);
                        var dr = await p.ProduceAsync("sandeepFood", new Message<Null, string> { Value = s });
                        Thread.Sleep(30000);
                    }

                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
