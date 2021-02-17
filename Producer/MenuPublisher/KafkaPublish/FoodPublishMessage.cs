namespace MenuPublisher
{
    using Confluent.Kafka;
    using SolTechnology.Avro.Kafka.Serialization;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class FoodPublishMessage:IFoodPublishMessage
    {
        private IAppSettings _configuration;
        public FoodPublishMessage(IAppSettings appSettings)=>this._configuration = appSettings;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        static string GetSchemaAvro(out string schema)
        {
            string summaryQuery = string.Empty;
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\AvroSchema\\Food.json";
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                schema = streamReader.ReadToEnd();
            }
            return schema;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configValuePairs"></param>
        /// <param name="food"></param>
        /// <param name="schema"></param>
        static void CreateProducer(Dictionary<string, object> configValuePairs, Food food, string schema)
        {
            try
            {
                var producer = new ProducerBuilder<string, Food>(
                new ProducerConfig { BootstrapServers = (string)configValuePairs[KafkaPropNames.BootstrapServers] })
                      .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(new AvroConvertSerializer<Food>(schema))
                .Build();

                producer.Produce((string)configValuePairs[KafkaPropNames.Topic], new Message<string, Food>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = food
                });
                producer.Flush();
            }
            catch (ProduceException<string, Food> ex)
            {
                ex.ToString();
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="food"></param>
        /// <returns></returns>

        public  async Task WriteMessage(Food food)
        {
            Dictionary<string, object> keyValuePairs = _configuration.GetConfigValue();
            if (!string.IsNullOrWhiteSpace(GetSchemaAvro(out string schema)))
            {
                if (keyValuePairs != null)
                {
                    await Task.Run(() =>
                    {
                        CreateProducer(keyValuePairs, food, schema);
                    });
                }
            }
        }
    }
}
