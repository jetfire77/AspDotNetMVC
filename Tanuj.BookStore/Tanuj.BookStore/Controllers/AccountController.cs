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


        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }


        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {

              var result =  await _accountRepository.PasswordSignInAsync(signInModel);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home" ); // if result is successfull than redirect to idex page of home controller

                }

                ModelState.AddModelError("", "Invalid credentials");

            }
            return View();
        }


        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }



    }
}
