using System;
using System.Threading.Tasks;
using DTcms.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.JustEF;

namespace DTcms.WebApi
{
    public class AuthRepository : IDisposable
    {
        private readonly ApplicationDbContext _ctx;

        private readonly UserManager<ApplicationUser,int> _userManager;

        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser,int>(new UserStoreIntPk(_ctx));
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new ApplicationUser
            {
                user_name = userModel.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string username, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(username, password);
            return user;
        }

    }
}