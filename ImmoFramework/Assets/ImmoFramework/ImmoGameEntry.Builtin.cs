using System;
using UnityEngine;

using Immo.Framework.Component;
using Immo.Framework.Component.Event;

namespace Immo.Framework
{
    public sealed partial class ImmoGameEntry : MonoBehaviour
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
            BaseComponent = ImmoFrameworkGameEntry.GetComponent<ImmoFrameworkBaseComponent>();
            EventComponent = ImmoFrameworkGameEntry.GetComponent<ImmoFrameworkEventComponent>();
        }
    }
}