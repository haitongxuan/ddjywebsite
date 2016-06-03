// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.JustEF
{
    /// <summary>
    ///     EntityFramework based user store implementation that supports IUserStore, IUserLoginStore, IUserClaimStore and
    ///     IUserRoleStore
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class UserStore<TUser> :
        UserStore<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>,
        IUserStore<TUser> where TUser : IdentityUser
    {
        /// <summary>
        ///     Default constuctor which uses a new instance of a default EntityyDbContext
        /// </summary>
        public UserStore()
            : this(new IdentityDbContext())
        {
            DisposeContext = true;
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserStore(DbContext context)
            : base(context)
        {
        }
    }

    /// <summary>
    ///     EntityFramework based user store implementation that supports IUserStore, IUserLoginStore, IUserClaimStore and
    ///     IUserRoleStore
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserLogin"></typeparam>
    /// <typeparam name="TUserRole"></typeparam>
    /// <typeparam name="TUserClaim"></typeparam>
    public class UserStore<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> :
        IUserLoginStore<TUser, TKey>,
        IUserClaimStore<TUser, TKey>,
        IUserRoleStore<TUser, TKey>,
        IUserPasswordStore<TUser, TKey>,
        IUserSecurityStampStore<TUser, TKey>,
        IQueryableUserStore<TUser, TKey>,
        IUserEmailStore<TUser, TKey>,
        IUserPhoneNumberStore<TUser, TKey>,
        IUserTwoFactorStore<TUser, TKey>,
        IUserLockoutStore<TUser, TKey>,
        IUserSaltStore<TUser, TKey>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<TKey, TUserRole>
        where TUserLogin : IdentityUserLogin<TKey>, new()
        where TUserRole : IdentityUserRole<TKey>, new()
        where TUserClaim : IdentityUserClaim<TKey>, new()
    {
        private readonly IDbSet<TUserLogin> _logins;
        private readonly EntityStore<TRole> _roleStore;
        private readonly IDbSet<TUserClaim> _userClaims;
        private readonly IDbSet<TUserRole> _userRoles;
        private bool _disposed;
        private EntityStore<TUser> _userStore;

        /// <summary>
        ///     Constructor which takes a db context and wires up the stores with default instances using the context
        /// </summary>
        /// <param name="context"></param>
        public UserStore(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            Context = context;
            AutoSaveChanges = true;
            _userStore = new EntityStore<TUser>(context);
            _roleStore = new EntityStore<TRole>(context);
            _logins = Context.Set<TUserLogin>();
            _userClaims = Context.Set<TUserClaim>();
            _userRoles = Context.Set<TUserRole>();
        }

        /// <summary>
        ///     Context for the store
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        ///     If true will call dispose on the DbContext during Dispose
        /// </summary>
        public bool DisposeContext { get; set; }

        /// <summary>
        ///     If true will call SaveChanges after Create/Update/Delete
        /// </summary>
        public bool AutoSaveChanges { get; set; }

        /// <summary>
        ///     Returns an IQueryable of users
        /// </summary>
        public IQueryable<TUser> Users
        {
            get { return _userStore.EntitySet; }
        }

        /// <summary>
        ///     Return the claims for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            await EnsureClaimsLoaded(user).WithCurrentCulture();
            return user.Claims.Select(c => new Claim(c.claim_type, c.claim_value)).ToList();
        }

        /// <summary>
        ///     Add a claim to a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            _userClaims.Add(new TUserClaim { user_id = user.id, claim_type = claim.Type, claim_value = claim.Value });
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Remove a claim from a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public virtual async Task RemoveClaimAsync(TUser user, Claim claim)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            IEnumerable<TUserClaim> claims;
            var claimValue = claim.Value;
            var claimType = claim.Type;
            if (AreClaimsLoaded(user))
            {
                claims = user.Claims.Where(uc => uc.claim_value == claimValue && uc.claim_type == claimType).ToList();
            }
            else
            {
                var userId = user.id;
                claims = await _userClaims.Where(uc => uc.claim_value == claimValue && uc.claim_type == claimType && uc.user_id.Equals(userId)).ToListAsync().WithCurrentCulture();
            }
            foreach (var c in claims)
            {
                _userClaims.Remove(c);
            }
        }

        /// <summary>
        ///     Returns whether the user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.email_confirm == 1);
        }
        /// <summary>
        /// 返回用户是否进行过手机号确认
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetMobileConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.mobile_confirm == 1);
        }

        /// <summary>
        ///     Set email IsConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.email_confirm = confirmed ? 1 : 0;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Set mobile IsConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public virtual Task SetMobileConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.mobile_confirm = confirmed ? 1 : 0;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Set the user email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual Task SetEmailAsync(TUser user, string email)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.email = email;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get the user's email
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetEmailAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.email);
        }

        /// <summary>
        ///     Set the user mobile
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual Task SetMobileAsync(TUser user, string mobile)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.mobile = mobile;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get the user's mobile
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetMobileAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.mobile);
        }

        /// <summary>
        ///     Find a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual Task<TUser> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.email.ToUpper() == email.ToUpper());
        }

        /// <summary>
        ///     Find a user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual Task<TUser> FindByMobileAsync(string mobile)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.mobile.ToUpper() == mobile.ToUpper());
        }

        /// <summary>
        ///     Find a user by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual Task<TUser> FindByIdAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.id.Equals(userId));
        }

        /// <summary>
        ///     Find a user by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            return GetUserAggregateAsync(u => u.user_name.ToUpper() == userName.ToUpper());
        }

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="user"></param>
        public virtual async Task CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _userStore.Create(user);
            await SaveChanges().WithCurrentCulture();
        }

        /// <summary>
        ///     Mark an entity for deletion
        /// </summary>
        /// <param name="user"></param>
        public virtual async Task DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _userStore.Delete(user);
            await SaveChanges().WithCurrentCulture();
        }

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="user"></param>
        public virtual async Task UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _userStore.Update(user);
            await SaveChanges().WithCurrentCulture();
        }

        /// <summary>
        ///     Dispose the store
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // IUserLogin implementation

        /// <summary>
        ///     Returns the user associated with this login
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TUser> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var provider = login.login_provider;
            var key = login.provider_key;
            var userLogin =
                await _logins.FirstOrDefaultAsync(l => l.login_provider == provider && l.provider_key == key).WithCurrentCulture();
            if (userLogin != null)
            {
                var userId = userLogin.user_id;
                return await GetUserAggregateAsync(u => u.id.Equals(userId)).WithCurrentCulture();
            }
            return null;
        }

        /// <summary>
        ///     Add a login to the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public virtual Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            _logins.Add(new TUserLogin
            {
                user_id = user.id,
                provider_key = login.provider_key,
                login_provider = login.login_provider
            });
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Remove a login from a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public virtual async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            TUserLogin entry;
            var provider = login.login_provider;
            var key = login.provider_key;
            if (AreLoginsLoaded(user))
            {
                entry = user.Logins.SingleOrDefault(ul => ul.login_provider == provider && ul.provider_key == key);
            }
            else
            {
                var userId = user.id;
                entry = await _logins.SingleOrDefaultAsync(ul => ul.login_provider == provider && ul.provider_key == key && ul.user_id.Equals(userId)).WithCurrentCulture();
            }
            if (entry != null)
            {
                _logins.Remove(entry);
            }
        }

        /// <summary>
        ///     Get the logins for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            await EnsureLoginsLoaded(user).WithCurrentCulture();
            return user.Logins.Select(l => new UserLoginInfo(l.login_provider, l.provider_key)).ToList();
        }
        

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.password = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.password);
        }

        /// <summary>
        ///     Returns true if the user has a password set
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.password != null);
        }

        /// <summary>
        ///     Set the user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public virtual Task SetTelephoneAsync(TUser user, string phoneNumber)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.telphone = phoneNumber;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get a user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetTelephoneAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.telphone);
        }


        /// <summary>
        ///     Add a user to a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task AddToRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.name.ToUpper() == roleName.ToUpper()).WithCurrentCulture();
            if (roleEntity == null)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                    IdentityResources.RoleNotFound, roleName));
            }

            var ur = new TUserRole { user_id = user.id, role_id = roleEntity.id };
            _userRoles.Add(ur);
        }

        /// <summary>
        ///     Remove a user from a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }
            var roleEntity = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.name.ToUpper() == roleName.ToUpper()).WithCurrentCulture();
            if (roleEntity != null)
            {
                var roleId = roleEntity.id;
                var userId = user.id;
                var userRole = await _userRoles.FirstOrDefaultAsync(r => roleId.Equals(r.role_id) && r.user_id.Equals(userId)).WithCurrentCulture();
                if (userRole != null)
                {
                    _userRoles.Remove(userRole);
                }
            }
        }

        /// <summary>
        ///     Get the names of the roles a user is a member of
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<IList<string>> GetRolesAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            var userId = user.id;
            var query = from userRole in _userRoles
                        where userRole.user_id.Equals(userId)
                        join role in _roleStore.DbEntitySet on userRole.role_id equals role.id
                        select role.name;
            return await query.ToListAsync().WithCurrentCulture();
        }

        /// <summary>
        ///     Returns true if the user is in the named role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (String.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(IdentityResources.ValueCannotBeNullOrEmpty, "roleName");
            }
            var role = await _roleStore.DbEntitySet.SingleOrDefaultAsync(r => r.name.ToUpper() == roleName.ToUpper()).WithCurrentCulture();
            if (role != null)
            {
                var userId = user.id;
                var roleId = role.id;
                return await _userRoles.AnyAsync(ur => ur.role_id.Equals(roleId) && ur.user_id.Equals(userId)).WithCurrentCulture();
            }
            return false;
        }

        /// <summary>
        ///     Set the security stamp for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        public virtual Task SetSecurityStampAsync(TUser user, string stamp)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.security_stamp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get the security stamp for a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetSecurityStampAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.security_stamp);
        }

        /// <summary>
        ///     Set whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public virtual Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.two_factor_enable = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Gets whether two factor authentication is enabled for the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.two_factor_enable);
        }

        // Only call save changes if AutoSaveChanges is true
        private async Task SaveChanges()
        {
            if (AutoSaveChanges)
            {
                await Context.SaveChangesAsync().WithCurrentCulture();
            }
        }

        private bool AreClaimsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Claims).IsLoaded;
        }

        private async Task EnsureClaimsLoaded(TUser user)
        {
            if (!AreClaimsLoaded(user))
            {
                var userId = user.id;
                await _userClaims.Where(uc => uc.user_id.Equals(userId)).LoadAsync().WithCurrentCulture();
                Context.Entry(user).Collection(u => u.Claims).IsLoaded = true;
            }
        }

        private async Task EnsureRolesLoaded(TUser user)
        {
            if (!Context.Entry(user).Collection(u => u.Roles).IsLoaded)
            {
                var userId = user.id;
                await _userRoles.Where(uc => uc.user_id.Equals(userId)).LoadAsync().WithCurrentCulture();
                Context.Entry(user).Collection(u => u.Roles).IsLoaded = true;
            }
        }

        private bool AreLoginsLoaded(TUser user)
        {
            return Context.Entry(user).Collection(u => u.Logins).IsLoaded;
        }

        private async Task EnsureLoginsLoaded(TUser user)
        {
            if (!AreLoginsLoaded(user))
            {
                var userId = user.id;
                await _logins.Where(uc => uc.user_id.Equals(userId)).LoadAsync().WithCurrentCulture();
                Context.Entry(user).Collection(u => u.Logins).IsLoaded = true;
            }
        }

        /// <summary>
        /// Used to attach child entities to the User aggregate, i.e. Roles, Logins, and Claims
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected virtual async Task<TUser> GetUserAggregateAsync(Expression<Func<TUser, bool>> filter)
        {
            TKey id;
            TUser user;
            if (FindByIdFilterParser.TryMatchAndGetId(filter, out id))
            {
                user = await _userStore.GetByIdAsync(id).WithCurrentCulture();
            }
            else
            {
                user = await Users.FirstOrDefaultAsync(filter).WithCurrentCulture();
            }
            if (user != null)
            {
                await EnsureClaimsLoaded(user).WithCurrentCulture();
                await EnsureLoginsLoaded(user).WithCurrentCulture();
                await EnsureRolesLoaded(user).WithCurrentCulture();
            }
            return user;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        ///     If disposing, calls dispose on the Context.  Always nulls out the Context
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (DisposeContext && disposing && Context != null)
            {
                Context.Dispose();
            }
            _disposed = true;
            Context = null;
            _userStore = null;
        }

        // We want to use FindAsync() when looking for an User.Id instead of LINQ to avoid extra 
        // database roundtrips. This class cracks open the filter expression passed by 
        // UserStore.FindByIdAsync() to obtain the value of the id we are looking for 
        private static class FindByIdFilterParser
        {
            // expression pattern we need to match
            private static readonly Expression<Func<TUser, bool>> Predicate = u => u.id.Equals(default(TKey));
            // method we need to match: Object.Equals() 
            private static readonly MethodInfo EqualsMethodInfo = ((MethodCallExpression)Predicate.Body).Method;
            // property access we need to match: User.Id 
            private static readonly MemberInfo UserIdMemberInfo = ((MemberExpression)((MethodCallExpression)Predicate.Body).Object).Member;

            internal static bool TryMatchAndGetId(Expression<Func<TUser, bool>> filter, out TKey id)
            {
                // default value in case we can’t obtain it 
                id = default(TKey);

                // lambda body should be a call 
                if (filter.Body.NodeType != ExpressionType.Call)
                {
                    return false;
                }

                // actually a call to object.Equals(object)
                var callExpression = (MethodCallExpression)filter.Body;
                if (callExpression.Method != EqualsMethodInfo)
                {
                    return false;
                }
                // left side of Equals() should be an access to User.Id
                if (callExpression.Object == null
                    || callExpression.Object.NodeType != ExpressionType.MemberAccess
                    || ((MemberExpression)callExpression.Object).Member != UserIdMemberInfo)
                {
                    return false;
                }

                // There should be only one argument for Equals()
                if (callExpression.Arguments.Count != 1)
                {
                    return false;
                }

                MemberExpression fieldAccess;
                if (callExpression.Arguments[0].NodeType == ExpressionType.Convert)
                {
                    // convert node should have an member access access node
                    // This is for cases when primary key is a value type
                    var convert = (UnaryExpression)callExpression.Arguments[0];
                    if (convert.Operand.NodeType != ExpressionType.MemberAccess)
                    {
                        return false;
                    }
                    fieldAccess = (MemberExpression)convert.Operand;
                }
                else if (callExpression.Arguments[0].NodeType == ExpressionType.MemberAccess)
                {
                    // Get field member for when key is reference type
                    fieldAccess = (MemberExpression)callExpression.Arguments[0];
                }
                else
                {
                    return false;
                }

                // and member access should be a field access to a variable captured in a closure
                if (fieldAccess.Member.MemberType != MemberTypes.Field
                    || fieldAccess.Expression.NodeType != ExpressionType.Constant)
                {
                    return false;
                }

                // expression tree matched so we can now just get the value of the id 
                var fieldInfo = (FieldInfo)fieldAccess.Member;
                var closure = ((ConstantExpression)fieldAccess.Expression).Value;

                id = (TKey)fieldInfo.GetValue(closure);
                return true;
            }
        }

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.mobile = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.mobile);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.mobile_confirm == 1);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.mobile_confirm = confirmed ? 1 : 0;
            return Task.FromResult(0);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return
                Task.FromResult(user.lockout_end_date_utc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.lockout_end_date_utc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset());
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.lockout_end_date_utc = lockoutEnd == DateTimeOffset.MinValue ? (DateTime?)null : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.access_failed_count++;
            return Task.FromResult(user.access_failed_count);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.access_failed_count = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.access_failed_count);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.lockout_enabled);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.lockout_enabled = enabled;
            return Task.FromResult(0);
        }

        public Task SetSaltAsync(TUser user, string salt)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.salt = salt;
            return Task.FromResult(0);
        }

        public Task<string> GetSaltAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(user.salt);
        }

        public Task<string> GetSaltAsync()
        {
            string salt = GetCheckCode(6);
            return Task.FromResult(salt);
        }

        public Task<bool> HasSaltAsync(TUser user)
        {
            return Task.FromResult(user.salt != null);
        }

        /// <summary>
        /// 生成随机字母字符串(数字字母混和)
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        private string GetCheckCode(int codeCount)
        {
            string str = string.Empty;
            int rep = 0;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
    }
}