﻿#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially,
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Validation;

namespace Bifrost.Web
{
    /// <summary>
    /// Represents a HttpApplication that handles initialization of Bifrost and
    /// abstracts some of the tedious tasks needed to configure it
    /// </summary>
    public abstract class BifrostHttpApplication : HttpApplication, IApplication
    {
        public IContainer Container { get; private set; }
        public virtual void OnConfigure(IConfigure configure) { }
        public virtual void OnStarted() { }
        public virtual void OnStopped() { }
        public virtual void OnConfigureValidation() { }
        public virtual void OnContainerCreated() { }

        protected abstract IContainer CreateContainer();

        protected void Application_Start()
        {
            Container = CreateContainer();
            OnContainerCreated();

            var configure = Configure.With(Container, BindingLifecycle.Request).SpecificApplication(this);
            OnConfigure(configure);
            configure.Initialize();

            OnConfigureValidation();
            OnStarted();
        }

        protected void Application_Stop()
        {
            OnStopped();
        }
    }
}