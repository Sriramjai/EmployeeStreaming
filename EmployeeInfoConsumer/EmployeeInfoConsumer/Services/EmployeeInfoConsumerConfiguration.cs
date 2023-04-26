using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInfoConsumer.Services
{
    public class EmployeeInfoConsumerConfiguration
    {
        public string? topic { get; set; }
        public string? bootstrapServers { get; set; }
    }
}
