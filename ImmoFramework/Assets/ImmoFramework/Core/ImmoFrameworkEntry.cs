using System;
using System.Collections.Generic;



namespace Immo.Framework.Core
{
    /// <summary>
    /// Global access point to all framework modules.
    /// </summary>
    public static class ImmoFrameworkEntry
    {
        private static readonly List<ImmoFrameworkModule> s_ImmoFrameworkModules = new List<ImmoFrameworkModule>();


        public static void Tick()
        {
            foreach (ImmoFrameworkModule module in s_ImmoFrameworkModules)
            {
                module.Tick();
            }
        }


        public static T GetModule<T>() where T : class
        {
            Type type = typeof(T);
            if (!type.IsInterface)
            {
                throw new Exception($"Type {type} is not an interface. Modules can only be retrieved by their interface types.");
            }

            if (!type.FullName.StartsWith("Immo.Framework.", StringComparison.Ordinal))
            {
                throw new Exception($"Type {type} is not part of ImmoFramework. Cannot retrieve module.");
            }

            string moduleName = string.Format("{0}.{1}", type.Namespace, type.Name.Substring(1));
            Type moduleType = Type.GetType(moduleName);
            if (moduleType == null)
            {
                throw new Exception($"Module type {moduleName} not found.");
            }

            foreach (ImmoFrameworkModule module in s_ImmoFrameworkModules)
            {
                if (module.GetType() == moduleType)
                {
                    return module as T;
                }
            }

            return CreateModule(moduleType) as T;
        }


        private static ImmoFrameworkModule CreateModule(Type moduleType)
        {
            ImmoFrameworkModule module = Activator.CreateInstance(moduleType) as ImmoFrameworkModule;
            if (module == null)
            {
                throw new Exception($"Can not create module instance of type {moduleType}.");
            }

            s_ImmoFrameworkModules.Add(module);
            return module;
        }
    }

    
}