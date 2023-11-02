using BookManagement.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagement.Models
{
    public class Category : IPrimaryProperties
    {
	    public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

	    [ForeignKey("CategoryId")]
        public ICollection<Book>? Books { get; set; }
    }
}
