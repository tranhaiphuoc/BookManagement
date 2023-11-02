using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookManagement.Models
{
    public class BookAuthor
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int AuthorId { get; set; }

        [NotMapped]
        public virtual ICollection<SelectListItem>? Authors { get; set; }
    }
}
