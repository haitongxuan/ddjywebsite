// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNet.Identity
{
    /// <summary>
    ///     Implements password hashing methods
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        ///     Hash a password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual string HashPassword(string password, string salt)
        {
            return Crypto.HashPassword(password, salt);
        }

        /// <summary>
        ///     Verify that a password matches the hashedPassword
        /// </summary>
        /// <param name="hashedPassword"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public virtual PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword, string salt)
        {
            if (Crypto.VerifyHashedPassword(hashedPassword, providedPassword, salt))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}