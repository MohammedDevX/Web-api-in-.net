using learn_api.Data;
using learn_api.Mappers;
using learn_api.Models;
using learn_api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace learn_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksEFController : ControllerBase
    {
        private AppDbContext context;
        public BooksEFController(AppDbContext context)
        {
            this.context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<Book>> GetBooks()
        {
            List<Book> book = await context.Books.ToListAsync();
            return Ok(book);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            Book book = await context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(BookVM bookvm)
        {
            if (!ModelState.IsValid || bookvm.YearPublished < 1000)
            {
                return BadRequest();
            }
            Book book = BookMP.AffecteBookVMToBook(bookvm);
            await context.AddAsync(book);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBooks), new { Id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Book book = await context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, BookVM bookvm)
        {
            // Here for security, we must check id we have a book with this id, so we are going to use AsNoTracking 
            // not to load the object in the memory, because if we load them in the memory we are going to have 
            // two objects with the same id book and bookCheck
            Book bookCheck = await context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
            if (bookCheck == null)
            {
                return NotFound();
            }
            Book book = BookMP.AffecteBookVMToBook(bookvm, id);
            context.Books.Update(book);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
