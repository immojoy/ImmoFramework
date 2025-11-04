
using System;
using System.Collections.Generic;

using Immo.Framework.Component;


namespace Immo.Framework.Component
{
    /// <summary>
    /// Global access point to all game components.
    /// </summary>
    public static class ImmoFrameworkGameEntry
    {
        private static readonly List<ImmoFrameworkComponent> s_GameComponents = new List<ImmoFrameworkComponent>();


        public static T GetComponent<T>() where T : ImmoFrameworkComponent
        {
            return GetComponent(typeof(T)) as T;
        }


        public static void RegisterComponent(ImmoFrameworkComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            Type type = component.GetType();
            for (int i = 0; i < s_GameComponents.Count; i++)
            {
                if (s_GameComponents[i].GetType() == type)
                {
                    throw new Exception($"Component of type {type} is already registered.");
                }
            }
            s_GameComponents.Add(component);
        }


        private static ImmoFrameworkComponent GetComponent(Type type)
        {
            foreach (ImmoFrameworkComponent component in s_GameComponents)
            {
                if (component.GetType() == type)
                {
                    return component;
                }
            }

            return null;
        }
    }
}