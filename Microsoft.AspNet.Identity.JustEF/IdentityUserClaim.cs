// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Identity.JustEF
{
    /// <summary>
    ///     EntityType that represents one specific user claim
    /// </summary>
    public class IdentityUserClaim : IdentityUserClaim<string>
    {
    }

    /// <summary>
    ///     EntityType that represents one specific user claim
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class IdentityUserClaim<TKey>
    {
        /// <summary>
        ///     Primary key
        /// </summary>
        public virtual int id { get; set; }

        /// <summary>
        ///     User Id for the user who owns this login
        /// </summary>
        public virtual TKey user_id { get; set; }

        /// <summary>
        ///     Claim type
        /// </summary>
        public virtual string claim_type { get; set; }

        /// <summary>
        ///     Claim value
        /// </summary>
        public virtual string claim_value { get; set; }
    }
}