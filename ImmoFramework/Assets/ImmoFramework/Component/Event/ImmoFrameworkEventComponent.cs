using System;
using UnityEngine;

using Immo.Framework.Core;
using Immo.Framework.Core.Event;


namespace Immo.Framework.Component.Event
{
    [DisallowMultipleComponent]
    [AddComponentMenu("ImmoFramework/Component/ImmoFramework Event")]
    public sealed class ImmoFrameworkEventComponent : ImmoFrameworkComponent
    {
        private IImmoFrameworkEventModule m_EventModule;


        #region Unity Callbacks
        protected override void Awake()
        {
            base.Awake();

            m_EventModule = ImmoFrameworkEntry.GetModule<IImmoFrameworkEventModule>();
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
        /// <param name="handler">Event handler to register.</param>
        public void RegisterEventHandler<T>(ImmoFrameworkEventHandler<T> handler) where T : ImmoFrameworkEvent
        {
            m_EventModule.RegisterHandler(handler);
        }

        /// <summary>
        /// Unregisters an event handler for a specific event type.
        /// </summary>
        /// <param name="handler">Event handler to unregister.</param>
        public void UnregisterEventHandler<T>(ImmoFrameworkEventHandler<T> handler) where T : ImmoFrameworkEvent
        {
            m_EventModule.UnregisterHandler(handler);
        }

        /// <summary>
        /// Triggers an event of a specific type. This method is thread-safe.
        /// </summary>
        /// <param name="e">Event to trigger.</param>
        public void TriggerEvent<T>(T e) where T : ImmoFrameworkEvent
        {
            m_EventModule.TriggerEvent(e);
        }
        
        /// <summary>
        /// Triggers an event of a specific type immediately. This method is not thread-safe.
        /// </summary>
        /// <param name="e">Event to trigger.</param>
        public void TriggerEventImmediately<T>(T e) where T : ImmoFrameworkEvent
        {
            m_EventModule.TriggerEventImmediately(e);
        }
    }
}