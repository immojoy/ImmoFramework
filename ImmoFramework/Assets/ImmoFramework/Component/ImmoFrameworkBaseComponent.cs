using System;
using UnityEngine;

using Immo.Framework.Core;

namespace Immo.Framework.Component
{
    public sealed class ImmoFrameworkBaseComponent : ImmoFrameworkComponent
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
            ImmoFrameworkEntry.Update();
        }
    }
}