using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tanuj.BookStore.Models;
using Tanuj.BookStore.Repository;

namespace Tanuj.BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)    // injecting account repository
        {
            _accountRepository = accountRepository;
        }


        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }



        [Route("signup")]
        [HttpPost]
        public  async Task<IActionResult> Signup(SignUpUserModel userModel)
        {   
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);
                if (!result.Succeeded)
                {
                    foreach(var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                }

                ModelState.Clear();
            }
            return View();
        }
    }
}
