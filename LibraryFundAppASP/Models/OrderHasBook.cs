using System;
using System.Collections.Generic;

namespace LibraryFundAppASP.Models;

public partial class OrderHasBook
{
    public int IdOrder { get; set; }

    public int IdBook { get; set; }

    public sbyte Amount { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
