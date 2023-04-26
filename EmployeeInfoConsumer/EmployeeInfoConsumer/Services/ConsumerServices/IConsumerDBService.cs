using EmployeeInfoConsumer.Models;

namespace EmployeeInfoConsumer.Services.ConsumerServices
{
    public interface IConsumerDBService
    {
        int? GetOne(int employeeNum);

        Task<string> AddEmployee(EmployeeDb employee);

        Task<string> UpdateEmployee(int id, EmployeeDb employee);
    }
}
