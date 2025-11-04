using System;
using UnityEngine;



namespace Immo.Framework
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/Immo Game Entry")]
    public sealed partial class ImmoGameEntry : MonoBehaviour
    {
        private void Start()
        {
            // Component initialization has to be done in Start() to ensure all components are properly registered.
            // Components register themselves in their Awake() methods, which are called before Start().
            InitializeBuiltinComponents();
        }
    }
}