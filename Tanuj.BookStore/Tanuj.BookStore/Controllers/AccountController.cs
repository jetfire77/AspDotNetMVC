﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Login(SignInModel signInModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {

              var result =  await _accountRepository.PasswordSignInAsync(signInModel);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);   // return to the page on which you were going to before login in
                    }

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


        [Route("change-password")]
        public  IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
           if(ModelState.IsValid) {

                var result =await  _accountRepository.changePasswordAsync(model);
                if (result.Succeeded)
                {

                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
              
           
           return View(model);
        }



    }
}
