using learn_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace learn_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // Here we have a static List of books 
        static private List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                YearPublished = 1925
            },
            new Book
            {
                Id = 2,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                YearPublished = 1960
            },
            new Book
            {
                Id = 3,
                Title = "1984",
                Author = "George Orwell",
                YearPublished = 1949
            },
            new Book
            {
                Id = 4,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                YearPublished = 1813
            },
            new Book
            {
                Id = 5,
                Title = "Moby-Dick",
                Author = "Herman Melville",
                YearPublished = 1851
            }
        };

        // This is the method with GET request method, allowing us to list the data 
        [HttpGet] 
        // Return List of Book
        public ActionResult<List<Book>> GetBooks()
        {
            return Ok(books); // 200 status code
        }

        // This is also a GET method, but she accepte one param (id), to list only the book with that id
        // and she return a NotFound() status code => 404, if thats id doesnt existe
        [HttpGet("{id}")]
        // Retrun only one Book
        public ActionResult<Book> GetBooks(int id)
        {
            Book book = books.SingleOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(); // 404 status code
            }

            return Ok(book);
        }

        // This method handles a POST request, allowing us to send data to the server.
        // If the Id already exists, or the YearPublished is invalid, it returns a BadRequest (400 status code).
        // Otherwise, it returns CreatedAtAction (201 status code), which references the GET action
        // and includes the Id of the created object.
        [HttpPost]
        public ActionResult<Book> SendBook(Book book)
        {
            if (book.YearPublished < 1000 || book.YearPublished > DateTime.Now.Year)
            {
                return BadRequest("Date format is invalid !"); // 400 status code
            } 
            else if (books.SingleOrDefault(b => b.Id == book.Id) != null)
            {
                return BadRequest("This id is already exist !");
            }

            books.Add(book);
            return CreatedAtAction(nameof(GetBooks), new {Id = book.Id}, book); // 201 status code
        }

        // This is an action with PUT request, allowing us to update a book, he accepted 2 params, the id, and 
        // the new object
        // If the object id doesnt existe in the Liste he return NotFound (404 status code)
        // else he affecte the data in the new object to the old one and we return NoContent (204 status code)
        // thats meen the reqeuest is successfuly passed, but we return no content
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            if (books.SingleOrDefault(b => b.Id == id) == null)
            {
                return NotFound("This book doesnt existe !");
            }

            Book book = books.SingleOrDefault(b => b.Id == id);

            //book.Id = updatedBook.Id;
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.YearPublished = updatedBook.YearPublished;
            return NoContent(); // 204 status code
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            if (books.SingleOrDefault(b => b.Id == id) == null)
            {
                return NotFound();
            }

            books.Remove(books.SingleOrDefault(b => b.Id == id));
            return NoContent();
        }
    }
}
