// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Identity.JustEF
{
    /// <summary>
    ///     EntityType that represents a user belonging to a role
    /// </summary>
    public class IdentityUserRole : IdentityUserRole<string>
    {
    }

    /// <summary>
    ///     EntityType that represents a user belonging to a role
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class IdentityUserRole<TKey>
    {
        /// <summary>
        ///     UserId for the user that is in the role
        /// </summary>
        public virtual TKey user_id { get; set; }

        /// <summary>
        ///     RoleId for the role
        /// </summary>
        public virtual TKey role_id { get; set; }
    }
}