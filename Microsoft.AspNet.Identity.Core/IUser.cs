// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Identity
{
    /// <summary>
    ///     Minimal interface for a user with id and username
    /// </summary>
    public interface IUser : IUser<string>
    {
    }

    /// <summary>
    ///     Minimal interface for a user with id and username
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IUser<out TKey>
    {
        /// <summary>
        ///     Unique key for the user
        /// </summary>
        TKey id { get; }

        /// <summary>
        ///     Unique username
        /// </summary>
        string user_name { get; set; }
    }
}