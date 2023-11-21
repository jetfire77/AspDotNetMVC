using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Tanuj.BookStore.Models;

namespace Tanuj.BookStore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);

        Task<SignInResult> PasswordSignInAsync(SignInModel SignInModel);

        Task SignOutAsync();

    }

}