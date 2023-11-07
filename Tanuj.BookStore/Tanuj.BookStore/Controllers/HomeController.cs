using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net;
using Tanuj.BookStore.Models;

namespace Tanuj.BookStore.Controllers
{
    public class HomeController : Controller
    {

        public ViewResult Index()
        {
            ViewBag.Title = "Named Book Wanderer";
            dynamic data = new ExpandoObject();
           data.id = 1;
            data.name = "test";
            ViewBag.Data = data;

            ViewBag.Type = new BookModel()
            {
                Id = 5,
                Author = "tanuj",


            };
                
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
