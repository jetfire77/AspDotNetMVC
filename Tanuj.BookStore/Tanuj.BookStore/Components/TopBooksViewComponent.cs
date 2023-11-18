using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tanuj.BookStore.Repository;

namespace Tanuj.BookStore.Components
{
    public class TopBooksViewComponent : ViewComponent
    {

        public readonly BookRepository _bookRepository;
        public TopBooksViewComponent(BookRepository bookRepository)
        {
           _bookRepository = bookRepository;
        }

       

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopBooksAsync(count);
            return View(books);

        }
    }
}
