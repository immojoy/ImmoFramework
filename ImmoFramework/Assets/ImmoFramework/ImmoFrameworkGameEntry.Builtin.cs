using System;
using UnityEngine;

using Immo.Framework.Component;
using Immo.Framework.Component.Event;

namespace Immo.Framework
{
    public sealed partial class ImmoFrameworkGameEntry : MonoBehaviour
    {
        public static ImmoFrameworkBaseComponent BaseComponent
        {
            get;
            private set;
        }

        public static ImmoFrameworkEventComponent EventComponent
        {
            get;
            private set;
        }

        private void InitializeBuiltinComponents()
        {
            BaseComponent = ImmoFrameworkComponentEntry.GetComponent<ImmoFrameworkBaseComponent>();
            EventComponent = ImmoFrameworkComponentEntry.GetComponent<ImmoFrameworkEventComponent>();
        }
    }
}