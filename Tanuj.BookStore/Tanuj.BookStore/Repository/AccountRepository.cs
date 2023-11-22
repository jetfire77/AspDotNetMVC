using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tanuj.BookStore.Models;
using Tanuj.BookStore.Service;

namespace Tanuj.BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager
            , IUserService userService, IEmailService emailService, IConfiguration configuration)  // injecting usermanager of  identity framework  // injecting SignInManager of  identity framework to login  // injecting  userService to get detail of user
        {
            _userManager = userManager;
            _signInManager = SignInManager;
            _userService = userService;
           _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {

            var user = new ApplicationUser()
            { 
                FirstName = userModel.FirstName,             /// assigning values from view to database
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email
            };
           var result = await _userManager.CreateAsync(user, userModel.Password);
            if(result.Succeeded)
            {//user added to database successfully it is time to generate tokken

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if(!string.IsNullOrEmpty(token)) {
                    await SendEmailConfirmationEmail(user, token);
                        }
            }       
            return result;
        }


        public async Task<SignInResult>  PasswordSignInAsync(SignInModel SignInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(SignInModel.Email, SignInModel.Password, SignInModel.RememberMe, false);

            return result;
                
        }


        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> changePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user, string token)
        {

            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>()
                {
                   user.Email
                },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain + confirmationLink, user.Id, token ))
                }
            };
            await _emailService.SendEmailForEmailConfirmation(options);

        }
    }
}
