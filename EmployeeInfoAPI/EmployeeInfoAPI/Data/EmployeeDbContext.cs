using EmployeeInfoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfoAPI.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
            :base(options) 
        {

        }

        public DbSet<EmployeeDb> EmployeeDbs { get; set; }

    }
}
