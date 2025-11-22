
using System;
using UnityEngine;


namespace ImmoFramework.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/Component/ImmoFramework Event")]
    public sealed class IFEventComponent : IFComponent
    {
        private IFEventModule m_EventModule;


        #region Unity Callbacks
        protected override void Awake()
        {
            base.Awake();

            m_EventModule = IFModuleEntry.GetModule<IFEventModule>();
            if (m_EventModule == null)
            {
                Debug.LogError("Invalid event module.");
                return;
            }
        }

        private void Start()
        {

        }
        #endregion


        /// <summary>
        /// Registers an event handler for a specific event type.
        /// </summary>
        public void RegisterHandler<T>(IFEventHandler<T> handler) where T : IFEvent
        {
            m_EventModule.RegisterHandler(handler);
        }


        /// <summary>
        /// Unregisters an event handler for a specific event type.
        /// </summary>
        public void UnregisterHandler<T>(IFEventHandler<T> handler) where T : IFEvent
        {
            m_EventModule.UnregisterHandler(handler);
        }


        /// <summary>
        /// Triggers an event of a specific type. This method is thread-safe.
        /// </summary>
        public void TriggerEvent<T>(T e) where T : IFEvent
        {
            m_EventModule.TriggerEvent(e);
        }


        /// <summary>
        /// Triggers an event of a specific type immediately.
        /// </summary>
        public void TriggerEventImmediately<T>(T e) where T : IFEvent
        {
            m_EventModule.TriggerEventImmediately(e);
        }
    }
}