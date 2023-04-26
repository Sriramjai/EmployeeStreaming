using Confluent.Kafka;
using EmployeeInfoProducer.Models;
using EmployeeInfoProducer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace EmployeeInfoProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeInfoController : ControllerBase
    {
        private ProducerConfig _configuration;
        private readonly IConfiguration _config;
        private readonly ILogger<EmployeeInfoController> _logger;
        private readonly IEmployeeProducerService _employeeProducerService;

        public EmployeeInfoController(ProducerConfig configuration, IConfiguration config, IEmployeeProducerService employeeProducerService, ILogger<EmployeeInfoController> logger)
        {
            _configuration = configuration;
            _config = config;
            _logger = logger;
            _employeeProducerService = employeeProducerService;
        }

        [HttpPost("sendEmployeeDetails")]
        public async Task<ActionResult> Get([FromBody] EmployeeInfo employee)
        {
            string employeeData = JsonConvert.SerializeObject(employee);
            var topic = _config.GetSection("TopicName").Value;

            // Sending message for processing
            var result =  await _employeeProducerService.SendMessage(employee, topic, _configuration);

            return (result ? Ok(true) : Ok(false));
            
        }

        
    }
}