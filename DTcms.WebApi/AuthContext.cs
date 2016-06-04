using Microsoft.AspNet.Identity.JustEF;

namespace DTcms.WebApi
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext() : base("AuthContext")
        {

        }
    }
}