using Confluent.Kafka;
using EmployeeInfoProducer.Controllers;
using EmployeeInfoProducer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EmployeeInfoProducer.Services
{
    public class EmployeeProducerService : IEmployeeProducerService
    {
        private readonly ILogger<EmployeeProducerService> _logger;
        public EmployeeProducerService(ILogger<EmployeeProducerService> logger)
        {
            _logger = logger;
        }
        public async Task<bool> SendMessage(EmployeeInfo message, string topic, ProducerConfig _configuration)
        {
            string employeeData = JsonConvert.SerializeObject(message);
            using (var producer = new ProducerBuilder<Null, string>(_configuration).Build())
            {
                try
                {
                    // Sends message to topic
                    var request = await producer.ProduceAsync(topic, new Message<Null, string> { Value = employeeData });

                    if (request.Message.Value != null)
                    {
                        _logger.LogInformation($"Inserted data into broker {request.Message.Value}");
                    }
                    else
                    {
                        _logger.LogInformation($"No data inserted");
                    }

                    producer.Flush(TimeSpan.FromSeconds(10));
                    return true;
                }
                catch (ProduceException<Null, string> ex)
                {
                    Console.WriteLine(ex.Message);
                    _logger.LogError("Error in producer : ", ex.Message);
                    return false;

                }

            }
        }
    }
}
