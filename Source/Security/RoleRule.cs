﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace doLittle.Security
{
    /// <summary>
    /// Represents a specific <see cref="ISecurityRule"/> for roles
    /// </summary>
    public class RoleRule : ISecurityRule
    {
        IUserSecurityActor _userToAuthorize;

        /// <summary>
        /// Initializes a new instance of <see cref="RoleRule"/>
        /// </summary>
        /// <param name="userToAuthorize">The <see cref="UserSecurityActor" /> to check the role against.</param>
        /// <param name="role">The role to check for</param>
        public RoleRule(IUserSecurityActor userToAuthorize, string role)
        {
            _userToAuthorize = userToAuthorize;
            Role = role;
        }

        /// <summary>
        /// Gets the role for the rule
        /// </summary>
        public string Role { get; private set; }

#pragma warning disable 1591 // Xml Comments
        public bool IsAuthorized(object securable)
        {
            return string.IsNullOrWhiteSpace(Role) || _userToAuthorize.IsInRole(Role);
        }
        
        public const string DescriptionFormat = @"RequiredRole_{{{0}}}";
        public string Description
        {
            get { return string.Format(DescriptionFormat, Role); }
        }
#pragma warning restore 1591 // Xml Comments
    }
}
