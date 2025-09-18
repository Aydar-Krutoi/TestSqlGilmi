using System;
using System.Collections.Generic;

namespace TestSqlGilmi.Data;

public partial class Basket
{
    public int IdBasket { get; set; }

    public int UserId { get; set; }

    public int ItemsId { get; set; }

    public string? Counter { get; set; }

    public virtual Item Items { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}   
