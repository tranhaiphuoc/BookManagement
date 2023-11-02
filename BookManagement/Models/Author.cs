using BookManagement.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagement.Models
{
    public class Author : IPrimaryProperties
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

	    [ForeignKey("AuthorId")]
        public ICollection<BookAuthor>? Books { get; set; }
    }
}
