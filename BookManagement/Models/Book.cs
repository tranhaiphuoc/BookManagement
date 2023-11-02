using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookManagement.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Title { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

	    [ForeignKey("BookId")]
        public ICollection<BookAuthor>? Authors { get; set; }

        [ForeignKey("BookId")]
        public ICollection<BookCategory>? Categories { get; set; }
    }
}
