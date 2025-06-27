using System;
using System.Collections.Generic;

namespace LibraryFundAppASP.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int IdVisitor { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual Visitor IdVisitorNavigation { get; set; } = null!;

    public virtual ICollection<OrderHasBook> OrderHasBooks { get; set; } = new List<OrderHasBook>();
}
