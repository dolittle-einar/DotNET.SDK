﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Globalization;
using System.Reflection;
using doLittle.Assemblies.Configuration;
using doLittle.Execution;
using doLittle.DependencyInversion;
using doLittle.DependencyInversion.Conventions;
using doLittle.Runtime.Tenancy;
using doLittle.Runtime.Execution;

namespace doLittle.Configuration
{
    /// <summary>
    /// Defines the configuration for doLittle
    /// </summary>
    public interface IConfigure 
    {
        /// <summary>
        /// Gets the container that is used
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// Gets the entry assembly for the application
        /// </summary>
        Assembly EntryAssembly { get; }

        /// <summary>
        /// Gets the configuration for commands
        /// </summary>
        ICommandsConfiguration Commands { get; }

        /// <summary>
        /// Gets the configuration for <see cref="doLittle.Tasks.Task">Tasks</see>
        /// Supports specific storage
        /// </summary>
        ITasksConfiguration Tasks { get; }

        /// <summary>
        /// Gets the convention manager for bindings
        /// </summary>
        IBindingConventionManager ConventionManager { get; }

       
        /// <summary>
        /// Gets the configureation for the applications default storage
        /// </summary>
        IDefaultStorageConfiguration DefaultStorage { get; }

        /// <summary>
        /// Gets the configuration for the frontend part of the application
        /// </summary>
        IFrontendConfiguration Frontend { get; }

        /// <summary>
        /// Gets the configuration for <see cref="ICallContext"/>
        /// </summary>
        ICallContextConfiguration CallContext { get; }

        /// <summary>
        /// Gets the configuration for the <see cref="IExecutionContext"/>
        /// </summary>
        IExecutionContextConfiguration ExecutionContext { get; }

        /// <summary>
        /// Gets the configuration for security
        /// </summary>
        ISecurityConfiguration Security { get; }

        /// <summary>
        /// Gets the configuration for assemblies and how they are treated
        /// </summary>
        AssembliesConfiguration Assemblies { get; }
        
        /// <summary>
        /// Gets or sets the <see cref="CultureInfo">culture</see> to use in doLittle
        /// </summary>
        CultureInfo Culture { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CultureInfo">UI culture</see> to use in doLittle
        /// </summary>
        CultureInfo UICulture { get; set; }

        /// <summary>
        /// Initializes doLittle after configuration
        /// </summary>
        void Initialize();
    }
}