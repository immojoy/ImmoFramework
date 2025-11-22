using System;
using UnityEngine;


namespace ImmoFramework.Runtime
{
    public sealed partial class IFGameEntry : MonoBehaviour
    {
        public static IFBaseComponent BaseComponent
        {
            get;
            private set;
        }

        public static IFEventComponent EventComponent
        {
            get;
            private set;
        }

        private static void InitializeBuiltinComponents()
        {
            BaseComponent = IFComponentEntry.GetComponent<IFBaseComponent>();
            EventComponent = IFComponentEntry.GetComponent<IFEventComponent>();
        }
    }
}