using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfoAPI.Models;

[Keyless]
[Table("EmployeeDB")]
public partial class EmployeeDb
{
    public int EmployeeNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string EmployeeName { get; set; } = null!;

    [Column(TypeName = "decimal(18, 0)")]
    public decimal HourlyRate { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal HoursWorked { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? TotalPay { get; set; }
}
