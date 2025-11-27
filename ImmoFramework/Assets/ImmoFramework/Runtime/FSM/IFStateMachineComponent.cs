
using System;
using System.Collections.Generic;

using UnityEngine;


namespace ImmoFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/Component/ImmoFramework State Machine")]
    public sealed class IFStateMachineComponent : IFComponent
    {
        private IFStateMachineModule m_StateMachineModule;


        /// <summary>
        /// Gets the number of state machines.
        /// </summary>
        public int Count => m_StateMachineModule.Count;


        #region Unity Callbacks
        protected override void Awake()
        {
            base.Awake();

            m_StateMachineModule = IFModuleEntry.GetModule<IFStateMachineModule>();
            if (m_StateMachineModule == null)
            {
                Debug.LogError("Invalid state machine module.");
                return;
            }
        }

        private void Start()
        {

        }
        #endregion


        public IFStateMachine<T> CreateStateMachine<T>(string name, T owner, params IFState<T>[] states) where T : class
        {
            return m_StateMachineModule.CreateStateMachine(name, owner, states);
        }


        public IFStateMachine<T> GetStateMachine<T>(string name) where T : class
        {
            return m_StateMachineModule.GetStateMachine<T>(name);
        }


        public IFStateMachineBase[] GetAllStateMachines()
        {
            return m_StateMachineModule.GetAllStateMachines();
        }


        public bool HasStateMachine(string name)
        {
            return m_StateMachineModule.HasStateMachine(name);
        }


        public bool DestroyStateMachine(string name)
        {
            return m_StateMachineModule.DestroyStateMachine(name);
        }
    }
}