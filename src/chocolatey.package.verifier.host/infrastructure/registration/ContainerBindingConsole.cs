﻿// <copyright company="RealDimensions Software, LLC" file="ContainerBindingConsole.cs">
//   Copyright 2015 - Present RealDimensions Software, LLC
// </copyright>
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// 
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace chocolatey.package.verifier.Host.Infrastructure.Registration
{
    using System.Collections.Generic;
    using SimpleInjector;
    using infrastructure.app.tasks;
    using infrastructure.configuration;
    using infrastructure.filesystem;
    using infrastructure.tasks;

    /// <summary>
    ///   The inversion container binding for the application.
    ///   This is client project specific - contains items that are only available in the client project.
    ///   Look for the broader application container in the core project.
    /// </summary>
    public class ContainerBindingConsole
    {
        /// <summary>
        ///   Loads the module into the kernel.
        /// </summary>
        /// <param name="container">The container.</param>
        public void RegisterComponents(Container container)
        {
            var configuration = Config.GetConfigurationSettings();

            container.Register<IFileSystem, DotNetFileSystem>(Lifestyle.Singleton);

            container.Register<IEnumerable<ITask>>(
                () =>
                    {
                        var list = new List<ITask>
                                       {
                                           new StartupTask(),
                                           new ShutdownAfterWorkCompletedTask(),
                                           new CreateGistTask()
                                       };

                        return list.AsReadOnly();
                    },
                Lifestyle.Singleton);
        }
    }
}