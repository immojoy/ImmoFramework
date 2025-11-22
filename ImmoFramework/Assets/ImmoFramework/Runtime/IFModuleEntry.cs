
using System;
using System.Collections.Generic;


namespace ImmoFramework.Runtime
{
    /// <summary>
    /// Entry point for the ImmoFramework. Provides access to core modules and services.
    /// </summary>
    public static class IFModuleEntry
    {
        private static readonly LinkedList<IFModule> s_Modules = new();


        /// <summary>
        /// Updates all registered modules.
        /// </summary>
        public static void Update(float elapsedSeconds, float realElapsedSeconds)
        {
            foreach (IFModule module in s_Modules)
            {
                module.Update(elapsedSeconds, realElapsedSeconds);
            }
        }


        public static void Shutdown()
        {
            foreach (IFModule module in s_Modules)
            {
                module.Shutdown();
            }
            s_Modules.Clear();
        }



        public static T GetModule<T>() where T : IFModule
        {
            Type type = typeof(T);
            if (type == null)
            {
                throw new Exception($"Type {type} is invalid.");
            }
            return GetModule(type) as T;
        }


        private static IFModule GetModule(Type type)
        {
            foreach (IFModule module in s_Modules)
            {
                if (module.GetType() == type)
                {
                    return module;
                }
            }
            return CreateModule(type);
        }


        private static IFModule CreateModule(Type type)
        {
            IFModule module = Activator.CreateInstance(type) as IFModule;
            if (module == null)
            {
                throw new Exception($"Can not create module of type {type}.");
            }

            // Insert module based on priority
            LinkedListNode<IFModule> current = s_Modules.First;
            while (current != null && current.Value.Priority >= module.Priority)
            {
                current = current.Next;
            }
            if (current != null)
            {
                s_Modules.AddBefore(current, module);
            }
            else
            {
                s_Modules.AddLast(module);
            }

            return module;
        }
    }
}