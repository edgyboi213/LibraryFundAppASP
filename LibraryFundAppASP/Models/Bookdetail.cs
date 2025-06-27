using System;
using System.Collections.Generic;

namespace LibraryFundAppASP.Models;

public partial class Bookdetail
{
    public int IdBook { get; set; }

    public string Title { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    /// <summary>
    /// [0 - 9999.99]
    /// </summary>
    public string Genre { get; set; } = null!;

    public ushort PageCount { get; set; }
}
