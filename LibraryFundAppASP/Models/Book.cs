using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryFundAppASP.Models
{
    public partial class Book
    {
        public int IdBook { get; set; }

        [Required(ErrorMessage = "Автор обязателен")]
        public int IdAuthor { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, ErrorMessage = "Название не может быть длиннее 100 символов")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Жанр обязателен")]
        [StringLength(50, ErrorMessage = "Жанр не может быть длиннее 50 символов")]
        public string Genre { get; set; } = null!;

        [Range(1, 65535, ErrorMessage = "Количество страниц должно быть от 1 до 65535")]
        public ushort PageCount { get; set; }

        [Range(0, 9999, ErrorMessage = "Год выпуска должен быть от 0 до 9999")]
        public int? ReleaseYear { get; set; }

        [ForeignKey("IdAuthor")]
        public virtual Author? IdAuthorNavigation { get; set; }

        public virtual ICollection<OrderHasBook> OrderHasBooks { get; set; } = new List<OrderHasBook>();
    }
}
