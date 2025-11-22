
using System;
using System.Collections.Generic;


namespace ImmoFramework.Runtime
{
    public sealed class IFEventModule : IFModule
    {
        private readonly Dictionary<Type, List<IIFEventHandler>> m_EventHandlers = new();
        private readonly Queue<IFEvent> m_EventQueue = new();
        private readonly object m_Lock = new();



        /// <summary>
        /// Registers an event handler for a specific event type.
        /// </summary>
        /// <param name="handler">The event handler to register.</param>
        public void RegisterHandler<T>(IFEventHandler<T> handler) where T : IFEvent
        {
            Type eventType = typeof(T);
            if (!m_EventHandlers.ContainsKey(eventType))
            {
                m_EventHandlers[eventType] = new List<IIFEventHandler>();
            }

            if (!m_EventHandlers[eventType].Contains(handler))
            {
                m_EventHandlers[eventType].Add(handler);
                m_EventHandlers[eventType].Sort((a, b) => b.Priority.CompareTo(a.Priority));
            }
        }


        /// <summary>
        /// Unregisters an event handler for a specific event type.
        /// </summary>
        /// <param name="handler">The event handler to unregister.</param>
        public void UnregisterHandler<T>(IFEventHandler<T> handler) where T : IFEvent
        {
            Type eventType = typeof(T);
            if (m_EventHandlers.ContainsKey(eventType))
            {
                m_EventHandlers[eventType].Remove(handler);
                if (m_EventHandlers[eventType].Count == 0)
                {
                    m_EventHandlers.Remove(eventType);
                }
            }
        }


        /// <summary>
        /// Triggers an event to be processed.</br>
        /// This method queues the event for processing in the next update cycle.
        /// </summary>
        /// <param name="e">The event to trigger.</param>
        public void TriggerEvent<T>(T e) where T : IFEvent
        {
            if (e == null || e.IsCancelled)
            {
                return;
            }

            lock (m_Lock)
            {
                m_EventQueue.Enqueue(e);
            }
        }


        /// <summary>
        /// Triggers an event to be processed immediately.
        /// </summary>
        /// <param name="e">The event to trigger.</param>
        public void TriggerEventImmediately<T>(T e) where T : IFEvent
        {
            if (e == null || e.IsCancelled)
            {
                return;
            }

            ProcessEvent(e);
        }


        /// <summary>
        /// Updates the event module, processing all queued events.
        /// </summary>
        /// <param name="elapsedSeconds">The elapsed time in seconds since the last update.</param>
        /// <param name="realElapsedSeconds">The real elapsed time in seconds since the last update.</param>
        public override void Update(float elapsedSeconds, float realElapsedSeconds)
        {
            while (true)
            {
                IFEvent e = null;
                lock (m_Lock)
                {
                    if (m_EventQueue.Count > 0)
                    {
                        e = m_EventQueue.Dequeue();
                    }
                }

                if (e == null)
                {
                    break;
                }

                ProcessEvent(e);
            }
        }
        

        /// <summary>
        /// Shuts down the event module, clearing all queued events and handlers.
        /// </summary>
        public override void Shutdown()
        {
            lock (m_Lock)
            {
                m_EventQueue.Clear();
                m_EventHandlers.Clear();
            }
        }



        private void ProcessEvent(IFEvent e)
        {
            Type eventType = e.GetType();
            if (m_EventHandlers.ContainsKey(eventType))
            {
                List<IIFEventHandler> handlers = new List<IIFEventHandler>();
                handlers.AddRange(m_EventHandlers[eventType]);

                foreach (IIFEventHandler handler in handlers)
                {
                    if (e.IsCancelled)
                    {
                        break;
                    }
                    handler.HandleEvent(e);
                }
            }
        }
    }
}