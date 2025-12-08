using learn_api.Models;
using learn_api.ViewModels;

namespace learn_api.Mappers
{
    public class BookMP
    {
        public static Book AffecteBookVMToBook(BookVM bookvm)
        {
            return new Book
            {
                Title = bookvm.Title,
                Author = bookvm.Author,
                YearPublished = bookvm.YearPublished
            };
        }
        public static Book AffecteBookVMToBook(BookVM bookvm, int id)
        {
            return new Book
            {
                Id = id,
                Title = bookvm.Title,
                Author = bookvm.Author,
                YearPublished = bookvm.YearPublished
            };
        }
    }
}
