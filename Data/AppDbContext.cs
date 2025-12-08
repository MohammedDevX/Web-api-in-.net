using learn_api.Models;
using Microsoft.EntityFrameworkCore;

namespace learn_api.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}

        public DbSet<Book> Books { get; set; }
    }
}
