using System;
using System.Collections.Generic;

namespace TestSqlGilmi.Data;

public partial class User
{
    public int IdUser { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Descipition { get; set; }

    public int RolesId { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual Role Roles { get; set; } 


}
