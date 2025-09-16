using System;
using System.Collections.Generic;

namespace TestSqlGilmi.Data;

public partial class Role
{
    public int IdRoles { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
