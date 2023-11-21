using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Tanuj.BookStore.Models;

namespace Tanuj.BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager)  // injecting usermanager of  identity framework  / injecting SignInManager of  identity framework to login
        {
            _userManager = userManager;
            _signInManager = SignInManager;
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
    }
}
