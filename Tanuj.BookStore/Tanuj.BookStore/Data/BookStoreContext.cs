using Microsoft.EntityFrameworkCore;

namespace Tanuj.BookStore.Data
{
    public class BookStoreContext: DbContext
    {


        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        { }

        // this Books will be table name
        public DbSet<Books> Books
        {  get; set; }


        
    }
}
