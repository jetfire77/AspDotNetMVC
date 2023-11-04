using System.Collections.Generic;
using System.Linq;
using Tanuj.BookStore.Models;

namespace Tanuj.BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookById(int id) {

            // linq query
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }


        public List<BookModel> SearchBook(string title, string authorName)
        {   
            // linq query
            return DataSource().Where(x=> x.Title.Contains(title)  && x.Author.Contains(authorName)).ToList();
        
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel() {Id =  1, Title="Cosmos", Author=" Carl Sagan "},
                new BookModel() {Id =  2, Title="A Brief History of Time (1988)", Author="Stephen Hawking "},
                new BookModel() {Id =  3, Title="The Origin of Species (1859)", Author="Charles Darwin "},
                new BookModel() {Id =  4, Title="Philosophiae Naturalis Principia Mathematica (1687)", Author="Isaac Newton "},
                new BookModel() {Id = 5, Title="Relativity: The Special and General Theory  (1916)", Author=" Albert Einstein"},


            };
        }
    }
}
