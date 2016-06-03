// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Microsoft.AspNet.Identity
{
    internal static class Crypto
    {
        private const int PBKDF2IterCount = 1000; // default for Rfc2898DeriveBytes
        private const int PBKDF2SubkeyLength = 256 / 8; // 256 bits
        private const int SaltSize = 128 / 8; // 128 bits

        /* =======================
         * HASHED PASSWORD FORMATS
         * =======================
         * 
         * Version 0:
         * PBKDF2 with HMAC-SHA1, 128-bit salt, 256-bit subkey, 1000 iterations.
         * (See also: SDL crypto guidelines v5.1, Part III)
         * Format: { 0x00, salt, subkey }
         */

        public static string HashPassword(string password, string salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (salt == null)
            {
                throw new ArgumentNullException("salt");
            }
            var npassword = DESEncrypt.Encrypt(password, salt);
            return npassword;
        }

        // hashedPassword must be of the format of HashWithPassword (salt + Hash(salt+input)
        public static bool VerifyHashedPassword(string hashedPassword, string password, string salt)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (salt == null)
            {
                throw new ArgumentNullException("salt");
            }
            var npassword = DESEncrypt.Encrypt(password, salt);
            return hashedPassword == npassword;
        }

    }
}