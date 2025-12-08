using System.ComponentModel.DataAnnotations;

namespace learn_api.ViewModels
{
    public class BookVM
    {
        [Required(ErrorMessage = "The title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "The year published is required")]
        public int YearPublished { get; set; }
    }
}
