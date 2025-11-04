using System;
using System.Collections.Generic;

using Immo.Framework.Core;

namespace Immo.Framework.Core.Event
{
    public interface IImmoFrameworkEventModule : IImmoFrameworkModule
    {
        /// <summary>
        /// Registers an event handler for a specific event type.
        /// </summary>
        /// <param name="handler">Event handler to register.</param>
        /// <typeparam name="T">Type of event being handled.</typeparam>
        public void RegisterHandler<T>(ImmoFrameworkEventHandler<T> handler) where T : ImmoFrameworkEvent;

        /// <summary>
        /// Unregisters an event handler for a specific event type.
        /// </summary>
        /// <param name="handler">Event handler to unregister.</param>
        /// <typeparam name="T">Type of event being handled.</typeparam>
        public void UnregisterHandler<T>(ImmoFrameworkEventHandler<T> handler) where T : ImmoFrameworkEvent;

        /// <summary>
        /// Triggers an event.
        /// </summary>
        /// <param name="e">Event to trigger.</param>
        public void TriggerEvent<T>(T e) where T : ImmoFrameworkEvent;

        /// <summary>
        /// Triggers an event immediately.
        /// </summary>
        /// <param name="e">Event to trigger.</param>
        public void TriggerEventImmediately<T>(T e) where T : ImmoFrameworkEvent;
    }
}