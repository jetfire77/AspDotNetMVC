using Microsoft.AspNetCore.Mvc;

namespace Tanuj.BookStore.Controllers
{
    public class HomeController : Controller
    {

        public ViewResult Index()
        {

            var obj = new { id = 1, Name="tanuj" };
            return  View(obj);
        }

        public ViewResult AboutUs() {
        
        return View();
        }

        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
