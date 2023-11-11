using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net;
using Tanuj.BookStore.Models;

namespace Tanuj.BookStore.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]  // decoritating with viewdata attribute
        public string CustomProperty { get; set; }


        [ViewData]
        public string Title { get; set; }

        [ViewData]
        public BookModel book { get; set; }
        public ViewResult Index()
        {

            /* ViewBag.Title = "Named Book Wanderer";
               dynamic data = new ExpandoObject();
               data.id = 1;
               data.name = "test";
               ViewBag.Data = data;

               ViewBag.Type = new BookModel()
               {
                   Id = 5,
                   Author = "tanuj",


               };

               */


            ViewData["property1"] = "nitish";

            ViewData["book"] = new BookModel() { Author = "Tanuj", Id = 1 };



            CustomProperty = "Custom value";
            Title = "Home Page";

            book = new BookModel() { Id = 7, Title="Harry potter", Author="j k rollings" };


            var obj = new { id = 1, Name="tanuj" };
            return  View(obj);
        }

        public ViewResult AboutUs() {

            Title = "About us";

            return View();
        }

        public ViewResult ContactUs()
        {
            Title = "Contact us";

            return View();
        }
    }
}
