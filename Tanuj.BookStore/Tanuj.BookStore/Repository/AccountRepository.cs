using Microsoft.AspNetCore.Identity;
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

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInManager
            , IUserService userService)  // injecting usermanager of  identity framework  // injecting SignInManager of  identity framework to login  // injecting  userService to get detail of user
        {
            _userManager = userManager;
            _signInManager = SignInManager;
            _userService = userService;
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

        public async Task<IdentityResult> changePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }
    }
}
