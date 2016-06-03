using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
    public interface IUserSaltStore<TUser> : IUserSaltStore<TUser, string>
        where TUser : class, IUser<string>
    { }

    public interface IUserSaltStore<TUser, in TKey> : IUserStore<TUser, TKey>
        where TUser : class, IUser<TKey>
    {
        /// <summary>
        ///     Set the user password hash
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        Task SetSaltAsync(TUser user, string salt);

        /// <summary>
        ///     Get the user salt
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> GetSaltAsync(TUser user);
        /// <summary>
        /// get a random string for salt
        /// </summary>
        /// <returns></returns>
        Task<string> GetSaltAsync();

        /// <summary>
        ///     Returns true if a user has a password set
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> HasSaltAsync(TUser user);
    }
  
}