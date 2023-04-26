using System;
using System.Collections.Generic;
using EmployeeInfoConsumer.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfoConsumer.Data;

public partial class EmployeeDBContext : DbContext
{
    public EmployeeDBContext()
    {
    }

    public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
        : base(options)
    {
    }

    public DbSet<EmployeeDb> EmployeeDbs { get; set; }

}
