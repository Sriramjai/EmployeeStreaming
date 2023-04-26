using Confluent.Kafka;
using EmployeeInfoConsumer.Models;
using EmployeeInfoConsumer.Services.ConsumerServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInfoConsumer.Services
{
    public class EmployeeInfoConsumerService : IEmployeeInfoConsumerService
    {

        private readonly ILogger<EmployeeInfoConsumerService> _logger;
        private readonly EmployeeInfoConsumerConfiguration _employeeInfoConsumerConfiguration;
        private readonly ConsumerDBService _consumerDBService;

        public EmployeeInfoConsumerService(IOptions<EmployeeInfoConsumerConfiguration> employeeInfoConsumerConfiguration, ConsumerDBService consumerDBService, ILogger<EmployeeInfoConsumerService> logger)
        {
            _employeeInfoConsumerConfiguration = employeeInfoConsumerConfiguration.Value ?? throw new ArgumentNullException(nameof(employeeInfoConsumerConfiguration)); 
            _consumerDBService = consumerDBService ?? throw new ArgumentNullException(nameof(consumerDBService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;

        }
        public async void ConsumeMessage() 
        {

            using (var consumer = new ConsumerBuilder<Null, string>(GetConsumerConfig()).Build())
            {
                consumer.Subscribe(_employeeInfoConsumerConfiguration.topic);

                CancellationTokenSource token = new();
                try
                {
                    while (true)
                    {
                        var response = consumer.Consume(token.Token);
                        if (response.Message != null)
                        {
                            var employeeDetails = JsonConvert.DeserializeObject<EmployeeDb>(response.Message.Value);

                            Console.WriteLine($"Employee Number : {employeeDetails.EmployeeNumber} " +
                                $"Employee Name : {employeeDetails.EmployeeName} " +
                                $"Hourly Rate : {employeeDetails.HourlyRate} " +
                                $"Hours Worked : {employeeDetails.HoursWorked}");

                            // If employee already exists then update, else add employe to database
                            if (employeeDetails.EmployeeNumber > 0)
                            {

                                var empNum = _consumerDBService.GetOne(employeeDetails.EmployeeNumber);

                                if(empNum > 0 )
                                {
                                    var result = await _consumerDBService.UpdateEmployee(employeeDetails.EmployeeNumber, employeeDetails);
                                    if (result == "Updated")
                                        Console.WriteLine($"Employee {employeeDetails.EmployeeNumber} has been updated in database");
                                }
                                else
                                {
                                   var result =  await _consumerDBService.AddEmployee(employeeDetails);
                                    if(result == "Inserted")
                                        Console.WriteLine($"Employee {employeeDetails.EmployeeNumber} has been inserted into database");
                                }
                            }
                           

                        }

                    }

                }
                catch (ConsumeException ex)
                {
                    Console.WriteLine(ex.Message);
                    _logger.LogError("Error in consumer : ", ex.Message);
                }

            }


        }

        ConsumerConfig GetConsumerConfig()
        {
            return new ConsumerConfig
            {
                BootstrapServers = _employeeInfoConsumerConfiguration.bootstrapServers,
                GroupId = "gid-consumers",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }
    }
}
