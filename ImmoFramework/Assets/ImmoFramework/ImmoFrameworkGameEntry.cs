using System;
using UnityEngine;



namespace Immo.Framework
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/ImmoFramework Game Entry")]
    public sealed partial class ImmoFrameworkGameEntry : MonoBehaviour
    {
        private void Start()
        {
            // Component initialization has to be done in Start() to ensure all components are properly registered.
            // Components register themselves in their Awake() methods, which are called before Start().
            InitializeBuiltinComponents();
        }
    }
}