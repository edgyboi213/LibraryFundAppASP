using System;
using System.Collections.Generic;

namespace LibraryFundAppASP.Models;

public partial class Visitor
{
    public int IdVisitor { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Class { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
