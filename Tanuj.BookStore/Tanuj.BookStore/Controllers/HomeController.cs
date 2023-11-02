using Microsoft.AspNetCore.Mvc;

namespace Tanuj.BookStore.Controllers
{
    public class HomeController : Controller
    {

        public string Index()
        {
            return "Tanuj ki BookStore";
        }
    }
}
