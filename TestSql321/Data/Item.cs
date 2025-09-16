using System;
using System.Collections.Generic;

namespace TestSqlGilmi.Data;

public partial class Item
{
    public int IdItems { get; set; }

    public string? Name { get; set; }

    public string? Price { get; set; }

    public string? Opisanie { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();
}
