using System.ComponentModel.DataAnnotations;

namespace BookManagement.ViewModel
{
    public class BookAuthorViewModel
    {
        public int BookAuthorId { get; set; }

        [Display(Name = "Author's Name")]
        public string? AuthorName { get; set; }
    }
}
