// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Identity
{
    /// <summary>
    ///     Mimimal set of data needed to persist role information
    /// </summary>
    public interface IRole : IRole<string>
    {
    }

    /// <summary>
    ///     Mimimal set of data needed to persist role information
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IRole<out TKey>
    {
        /// <summary>
        ///     Id of the role
        /// </summary>
        TKey id { get; }

        /// <summary>
        ///     Name of the role
        /// </summary>
        string name { get; set; }
    }
}