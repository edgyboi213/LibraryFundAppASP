using System;
using System.Collections.Generic;

namespace LibraryFundAppASP.Models;

public partial class Author
{
    public int IdAuthor { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
