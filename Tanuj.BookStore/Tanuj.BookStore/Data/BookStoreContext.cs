﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Tanuj.BookStore.Data
{
    public class BookStoreContext: DbContext
    {


        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        { }

        // this Books will be table name
        public DbSet<Books> Books     // addded reference of the books table into context class
        {  get; set; }


        public DbSet<BookGallery> BookGallery     // addded reference of the BookGallery table into context class
        { get; set; }

        public DbSet<Language> Language { get; set; } // addded reference of the Language table into context class


        
    }
}
