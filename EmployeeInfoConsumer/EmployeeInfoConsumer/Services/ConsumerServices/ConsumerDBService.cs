using EmployeeInfoConsumer.Data;
using EmployeeInfoConsumer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInfoConsumer.Services.ConsumerServices
{
    public class ConsumerDBService : IConsumerDBService
    {
        private readonly EmployeeDBContext _dbContext;
        public ConsumerDBService(EmployeeDBContext dbContext) 
        { 
            _dbContext = dbContext;
        }  
        public int? GetOne(int empNum)
        {
            var emp = _dbContext.EmployeeDbs.FirstOrDefault(e => e.EmployeeNumber == empNum);
            
            return (emp != null ? emp.EmployeeNumber : null);
        }

        public async Task<string> AddEmployee(EmployeeDb employee)
        {
            var emp = new EmployeeDb()
            {
                EmployeeNumber = employee.EmployeeNumber,
                EmployeeName = employee.EmployeeName,
                HourlyRate = employee.HourlyRate,
                HoursWorked = employee.HoursWorked,
                TotalPay = employee.HourlyRate * employee.HoursWorked
            };

            await _dbContext.EmployeeDbs.AddAsync(emp);
            _dbContext.SaveChanges();

            return "Inserted";
        }

        public async Task<string> UpdateEmployee(int id,  EmployeeDb employee)
        {
            var emp = await _dbContext.EmployeeDbs.FindAsync(id);

            if(emp != null)
            {
                emp.EmployeeNumber = id;
                emp.EmployeeName = employee.EmployeeName;
                emp.HourlyRate = employee.HourlyRate;
                emp.HoursWorked = employee.HoursWorked;
                emp.TotalPay = employee.HourlyRate * employee.HoursWorked;

                 _dbContext.SaveChanges();

                return "Updated";

            }
            return "Not Found";
        }
    }
}
