// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.AspNet.Identity.JustEF
{
    /// <summary>
    ///     Default EntityFramework IUser implementation
    /// </summary>
    public class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser
    {
        /// <summary>
        ///     Constructor which creates a new Guid for the Id
        /// </summary>
        public IdentityUser()
        {
            id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor that takes a userName
        /// </summary>
        /// <param name="userName"></param>
        public IdentityUser(string userName)
            : this()
        {
            user_name = userName;
        }
    }

    /// <summary>
    ///     Default EntityFramework IUser implementation
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TLogin"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TClaim"></typeparam>
    public class IdentityUser<TKey, TLogin, TRole, TClaim> : IUser<TKey>
        where TLogin : IdentityUserLogin<TKey>
        where TRole : IdentityUserRole<TKey>
        where TClaim : IdentityUserClaim<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public IdentityUser()
        {
            Claims = new List<TClaim>();
            Roles = new List<TRole>();
            Logins = new List<TLogin>();
        }
        public int group_id { get; set; }
        public DateTime group_start_time { get; set; }
        public DateTime group_end_time { get; set; }
        public string salt { get; set; }
        public string password { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public string nick_name { get; set; }
        public string sex { get; set; }
        public DateTime birthday { get; set; }
        public string telphone { get; set; }
        public string area { get; set; }
        public string address { get; set; }
        public string qq { get; set; }
        public string msn { get; set; }
        public decimal amount { get; set; }
        public int point { get; set; }
        public int exp { get; set; }
        public int status { get; set; }
        public DateTime reg_time { get; set; }
        public string reg_ip { get; set; }
        public int email_confirm { get; set; }
        public int mobile_confirm { get; set; }

        public bool two_factor_enable { get; set; }

        public DateTime? lockout_end_date_utc { get; set; }
        public bool lockout_enabled { get; set; }
        public int access_failed_count { get; set; }

        public string security_stamp { get; set; }

        /// <summary>
        ///     Navigation property for user roles
        /// </summary>
        public virtual ICollection<TRole> Roles { get; private set; }

        /// <summary>
        ///     Navigation property for user claims
        /// </summary>
        public virtual ICollection<TClaim> Claims { get; private set; }

        /// <summary>
        ///     Navigation property for user logins
        /// </summary>
        public virtual ICollection<TLogin> Logins { get; private set; }

        /// <summary>
        ///     User ID (Primary Key)
        /// </summary>
        public virtual TKey id { get; set; }

        /// <summary>
        ///     User name
        /// </summary>
        public virtual string user_name { get; set; }
    }
}