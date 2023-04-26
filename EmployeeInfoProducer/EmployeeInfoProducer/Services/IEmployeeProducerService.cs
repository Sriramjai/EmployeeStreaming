using Confluent.Kafka;
using EmployeeInfoProducer.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInfoProducer.Services
{
    public interface IEmployeeProducerService
    {
        Task<bool> SendMessage(EmployeeInfo message, string topic, ProducerConfig config);
    }
}
