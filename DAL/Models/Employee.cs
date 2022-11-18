using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Employee
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long EmployeeArea { get; set; }

    public virtual Area EmployeeAreaNavigation { get; set; } = null!;
}
