using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Tanuj.BookStore.Models;
using Tanuj.BookStore.Repository;

namespace Tanuj.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository  _bookRepository= null;
        public BookController(BookRepository bookRepository) {

            _bookRepository = bookRepository;

        }
        public async Task<ViewResult> GetAllBooks()
        {

           var data = await _bookRepository.GetAllBooks();
            
        return View(data);

        }

        [Route("book-details/{id}" , Name ="bookDetailsRoute")]
        public  async Task<ViewResult> GetBook(int id)
        {
            
            /*
            dynamic data = new System.Dynamic.ExpandoObject();
            data.book = _bookRepository.GetBookById(id);
            data.name = "tanuj";
            return View(data);
            */

            var data= await _bookRepository.GetBookById(id);
            return View(data);
           


        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }


        public ViewResult AddNewBook(bool isSuccess = false , int bookId = 0) {

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
                return View(); }


        //  creating action method to submit form you can have method with same 
        // name or update name it is your choice
        // but we cannot hve with same name and same parameter
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel  bookModel) {
            int id = await _bookRepository.AddNewBook(bookModel);
            if (id > 0)
            {
                return RedirectToAction(nameof(AddNewBook), new {isSuccess= true, bookId  = id});
            }
                
                return View(); }
    }
}
