// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Microsoft.AspNet.Identity.JustEF
{
    /// <summary>
    /// Default IdentityDbContext that uses the default entity types for ASP.NET Identity Users, Roles, Claims, Logins. 
    /// Use this overload to add your own entity types.
    /// </summary>
    public class IdentityDbContext :
        IdentityDbContext<IdentityUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        /// <summary>
        ///     Default constructor which uses the DefaultConnection
        /// </summary>
        public IdentityDbContext()
            : this("DefaultConnection")
        {
        }

        /// <summary>
        ///     Constructor which takes the connection string to use
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using the existing connection to connect to a database, and initializes it from
        ///     the given model.  The connection will not be disposed when the context is disposed if contextOwnsConnection is
        ///     false.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="model">The model that will back this context.</param>
        /// <param name="contextOwnsConnection">
        ///     Constructs a new context instance using the existing connection to connect to a
        ///     database, and initializes it from the given model.  The connection will not be disposed when the context is
        ///     disposed if contextOwnsConnection is false.
        /// </param>
        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using conventions to create the name of
        ///     the database to which a connection will be made, and initializes it from
        ///     the given model.  The by-convention name is the full name (namespace + class
        ///     name) of the derived context class.  See the class remarks for how this is
        ///     used to create a connection.
        /// </summary>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using the existing connection to connect
        ///     to a database.  The connection will not be disposed when the context is disposed
        ///     if contextOwnsConnection is false.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="contextOwnsConnection">If set to true the connection is disposed when the context is disposed, otherwise
        ///     the caller must dispose the connection.
        /// </param>
        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using the given string as the name or connection
        ///     string for the database to which a connection will be made, and initializes
        ///     it from the given model.  See the class remarks for how this is used to create
        ///     a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }
    }

    /// <summary>
    ///     DbContext which uses a custom user entity with a string primary key
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class IdentityDbContext<TUser> :
        IdentityDbContext<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
        where TUser : IdentityUser
    {
        /// <summary>
        ///     Default constructor which uses the DefaultConnection
        /// </summary>
        public IdentityDbContext()
            : this("DefaultConnection")
        {
        }

        /// <summary>
        ///     Constructor which takes the connection string to use
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public IdentityDbContext(string nameOrConnectionString)
            : this(nameOrConnectionString, true)
        {
        }

        /// <summary>
        ///     Constructor which takes the connection string to use
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        /// <param name="throwIfV1Schema">Will throw an exception if the schema matches that of Identity 1.0.0</param>
        public IdentityDbContext(string nameOrConnectionString, bool throwIfV1Schema)
            : base(nameOrConnectionString)
        {
            if (throwIfV1Schema && IsIdentityV1Schema(this))
            {
                throw new InvalidOperationException(IdentityResources.IdentityV1SchemaError);
            }
        }

        /// <summary>
        ///     Constructs a new context instance using the existing connection to connect to a database, and initializes it from
        ///     the given model.  The connection will not be disposed when the context is disposed if contextOwnsConnection is
        ///     false.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="model">The model that will back this context.</param>
        /// <param name="contextOwnsConnection">
        ///     Constructs a new context instance using the existing connection to connect to a
        ///     database, and initializes it from the given model.  The connection will not be disposed when the context is
        ///     disposed if contextOwnsConnection is false.
        /// </param>
        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using conventions to create the name of
        ///     the database to which a connection will be made, and initializes it from
        ///     the given model.  The by-convention name is the full name (namespace + class
        ///     name) of the derived context class.  See the class remarks for how this is
        ///     used to create a connection.
        /// </summary>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using the existing connection to connect
        ///     to a database.  The connection will not be disposed when the context is disposed
        ///     if contextOwnsConnection is false.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="contextOwnsConnection">If set to true the connection is disposed when the context is disposed, otherwise
        ///     the caller must dispose the connection.
        /// </param>
        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using the given string as the name or connection
        ///     string for the database to which a connection will be made, and initializes
        ///     it from the given model.  See the class remarks for how this is used to create
        ///     a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }
        //todo:改动验证表名称
        internal static bool IsIdentityV1Schema(DbContext db)
        {
            var originalConnection = db.Database.Connection as SqlConnection;
            // Give up and assume its ok if its not a sql connection
            if (originalConnection == null)
            {
                return false;
            }

            if (db.Database.Exists())
            {
                using (var tempConnection = new SqlConnection(originalConnection.ConnectionString))
                {
                    tempConnection.Open();
                    return
                        VerifyColumns(tempConnection, "dt_users", "id", "user_name", "password", "salt",
                            "mobile", "amount") &&
                        VerifyColumns(tempConnection, "dt_roles", "id", "name") &&
                        VerifyColumns(tempConnection, "dt_users_roles", "user_id", "role_id") &&
                        VerifyColumns(tempConnection, "dt_user_claims", "id", "claim_type", "claim_value", "user_id") &&
                        VerifyColumns(tempConnection, "dt_user_logins", "user_id", "provider_key", "login_provider");
                }
            }

            return false;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities",
            Justification = "Reviewed")]
        internal static bool VerifyColumns(SqlConnection conn, string table, params string[] columns)
        {
            var tableColumns = new List<string>();
            using (
                var command =
                    new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@Table", conn))
            {
                command.Parameters.Add(new SqlParameter("Table", table));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Add all the columns from the table
                        tableColumns.Add(reader.GetString(0));
                    }
                }
            }
            // Make sure that we find all the expected columns
            return columns.All(tableColumns.Contains);
        }
    }

    /// <summary>
    /// Generic IdentityDbContext base that can be customized with entity types that extend from the base IdentityUserXXX types.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TUserLogin"></typeparam>
    /// <typeparam name="TUserRole"></typeparam>
    /// <typeparam name="TUserClaim"></typeparam>
    public class IdentityDbContext<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> : DbContext
        where TUser : IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : IdentityRole<TKey, TUserRole>
        where TUserLogin : IdentityUserLogin<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
    {
        /// <summary>
        ///     Default constructor which uses the "DefaultConnection" connectionString
        /// </summary>
        public IdentityDbContext()
            : this("DefaultConnection")
        {
        }

        /// <summary>
        ///     Constructor which takes the connection string to use
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using the existing connection to connect to a database, and initializes it from
        ///     the given model.  The connection will not be disposed when the context is disposed if contextOwnsConnection is
        ///     false.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="model">The model that will back this context.</param>
        /// <param name="contextOwnsConnection">
        ///     Constructs a new context instance using the existing connection to connect to a
        ///     database, and initializes it from the given model.  The connection will not be disposed when the context is
        ///     disposed if contextOwnsConnection is false.
        /// </param>
        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using conventions to create the name of
        ///     the database to which a connection will be made, and initializes it from
        ///     the given model.  The by-convention name is the full name (namespace + class
        ///     name) of the derived context class.  See the class remarks for how this is
        ///     used to create a connection.
        /// </summary>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(DbCompiledModel model)
            : base(model)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using the existing connection to connect
        ///     to a database.  The connection will not be disposed when the context is disposed
        ///     if contextOwnsConnection is false.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="contextOwnsConnection">If set to true the connection is disposed when the context is disposed, otherwise
        ///     the caller must dispose the connection.
        /// </param>
        public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        ///     Constructs a new context instance using the given string as the name or connection
        ///     string for the database to which a connection will be made, and initializes
        ///     it from the given model.  See the class remarks for how this is used to create
        ///     a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        /// <param name="model">The model that will back this context.</param>
        public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        /// <summary>
        ///     IDbSet of Users
        /// </summary>
        public virtual IDbSet<TUser> Users { get; set; }

        /// <summary>
        ///     IDbSet of Roles
        /// </summary>
        public virtual IDbSet<TRole> Roles { get; set; }

        /// <summary>
        ///  是否验证邮箱重复
        /// </summary>
        public bool RequireUniqueEmail { get; set; }
        /// <summary>
        /// 是否验证手机号重复
        /// </summary>
        public bool RequireUniqueMobile { get; set; }
        //todo:修改
        /// <summary>
        ///     Maps table names, and sets up relationships between the various user entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            // Needed to ensure subclasses share the same table
            var user = modelBuilder.Entity<TUser>()
                .ToTable("dt_users");
            user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.user_id);
            user.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.user_id);
            user.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.user_id);
            user.Property(u => u.user_name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));

            // CONSIDER: u.Email is Required if set on options?
            user.Property(u => u.email).HasMaxLength(50);

            modelBuilder.Entity<TUserRole>()
                .HasKey(r => new { r.user_id, r.role_id })
                .ToTable("dt_users_roles");

            modelBuilder.Entity<TUserLogin>()
                .HasKey(l => new { l.login_provider, l.provider_key, l.user_id })
                .ToTable("dt_user_logins");

            modelBuilder.Entity<TUserClaim>()
                .ToTable("dt_user_claims");

            var role = modelBuilder.Entity<TRole>()
                .ToTable("dt_roles");
            role.Property(r => r.name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.role_id);
        }

        /// <summary>
        ///     Validates that UserNames are unique and case insenstive
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry,
            IDictionary<object, object> items)
        {
            if (entityEntry != null && entityEntry.State == EntityState.Added)
            {
                var errors = new List<DbValidationError>();
                var user = entityEntry.Entity as TUser;
                //check for uniqueness of user name and email
                if (user != null)
                {
                    if (Users.Any(u => String.Equals(u.user_name, user.user_name)))
                    {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateUserName, user.user_name)));
                    }
                    if (RequireUniqueEmail && Users.Any(u => String.Equals(u.email, user.email)))
                    {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateEmail, user.email)));
                    }
                    if (RequireUniqueMobile && Users.Any(u => string.Equals(u.mobile, user.mobile)))
                    {
                        errors.Add(new DbValidationError("User",
                               String.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateMobile, user.mobile)));
                    }
                }
                else
                {
                    var role = entityEntry.Entity as TRole;
                    //check for uniqueness of role name
                    if (role != null && Roles.Any(r => String.Equals(r.name, role.name)))
                    {
                        errors.Add(new DbValidationError("Role",
                            String.Format(CultureInfo.CurrentCulture, IdentityResources.RoleAlreadyExists, role.name)));
                    }
                }
                if (errors.Any())
                {
                    return new DbEntityValidationResult(entityEntry, errors);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }
    }
}