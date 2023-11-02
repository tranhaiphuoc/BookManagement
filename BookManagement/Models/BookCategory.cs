using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagement.Models
{
    public class BookCategory
    {
        public int Id { get; set; }

	    public int BookId { get; set; }

        public int CategoryId { get; set; }

        [NotMapped]
	    public virtual ICollection<SelectListItem>? Categories { get; set; }
    }
}
