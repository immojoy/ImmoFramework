using System;
using UnityEngine;



namespace ImmoFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/ImmoFramework Game Entry")]
    public sealed partial class IFGameEntry : MonoBehaviour
    {
        private void Start()
        {
            // Component initialization has to be done in Start() to ensure all components are properly registered.
            // Components register themselves in their Awake() methods, which are called before Start().
            InitializeBuiltinComponents();
        }
    }
}