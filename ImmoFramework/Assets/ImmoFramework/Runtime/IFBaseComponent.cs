using System;
using UnityEngine;

namespace ImmoFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/Component/ImmoFramework Base")]
    public sealed class IFBaseComponent : IFComponent
    {
        protected override void Awake()
        {
            base.Awake();


        }

        private void Start()
        {
        }

        private void Update()
        {
            IFModuleEntry.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}