
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ImmoFramework.Runtime
{
    public sealed partial class IFEntityComponent : IFComponent
    {
        [Serializable]
        private sealed class IFEntityGroupData
        {
            [SerializeField]
            private string m_Name = null;

            [SerializeField]
            private int m_InstanceCapacity = 50;


            public string Name => m_Name;
            public int InstanceCapacity => m_InstanceCapacity;
        }
    }
}