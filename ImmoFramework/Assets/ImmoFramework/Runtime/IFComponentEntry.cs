
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ImmoFramework.Runtime
{
    /// <summary>
    /// Entry point for the ImmoFramework components. Provides access to registered components.
    /// </summary>
    public static class IFComponentEntry
    {
        private static readonly LinkedList<IFComponent> s_Components = new();


        /// <summary>
        /// Retrieves a component by its type.
        /// </summary>
        public static T GetComponent<T>() where T : IFComponent
        {
            Type type = typeof(T);
            return GetComponent(type) as T;
        }


        /// <summary>
        /// Retrieves a component by its type.
        /// </summary> 
        /// <param name="type">Component type.</param>
        public static IFComponent GetComponent(Type type)
        {
            if (type == null)
            {
                throw new Exception("Type is invalid.");
            }

            foreach (IFComponent component in s_Components)
            {
                if (component.GetType() == type)
                {
                    return component;
                }
            }

            return null;
        }


        /// <summary>
        /// Retrieves a component by its full type name.
        /// </summary>
        /// <param name="type">Component type name.</param>
        public static IFComponent GetComponent(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new Exception("Type is invalid.");
            }

            foreach (IFComponent component in s_Components)
            {
                if (component.GetType().FullName == type)
                {
                    return component;
                }
            }

            return null;
        }


        /// <summary>
        /// Registers a new component in the framework.
        /// </summary>
        /// <param name="component">The component to register.</param>
        internal static void RegisterComponent(IFComponent component)
        {
            if (component == null)
            {
                throw new Exception("Component is invalid.");
            }

            Type type = component.GetType();
            foreach (IFComponent registeredComponent in s_Components)
            {
                if (registeredComponent.GetType() == type)
                {
                    Debug.LogError($"Component '{type.FullName}' is already registered.");
                }
            }
            s_Components.AddLast(component);
        }
    }
}