using System;
using System.Collections.Generic;

namespace Immo.Framework.Core.Event
{
    internal sealed class ImmoFrameworkEventModule : ImmoFrameworkModule
    {
        private readonly Dictionary<Type, List<IImmoFrameworkEventHandler>> m_EventHandlers = new Dictionary<Type, List<IImmoFrameworkEventHandler>>();
        private readonly Queue<ImmoFrameworkEvent> m_EventQueue = new Queue<ImmoFrameworkEvent>();
        private readonly object m_Lock = new object();



        public void RegisterHandler<T>(ImmoFrameworkEventHandler<T> handler) where T : ImmoFrameworkEvent
        {
            Type eventType = typeof(T);
            if (!m_EventHandlers.ContainsKey(eventType))
            {
                m_EventHandlers[eventType] = new List<IImmoFrameworkEventHandler>();
            }

            if (!m_EventHandlers[eventType].Contains(handler))
            {
                m_EventHandlers[eventType].Add(handler);
                m_EventHandlers[eventType].Sort((a, b) => b.Priority.CompareTo(a.Priority));
            }
        }

        public void UnregisterHandler<T>(ImmoFrameworkEventHandler<T> handler) where T : ImmoFrameworkEvent
        {
            Type eventType = typeof(T);
            if (m_EventHandlers.ContainsKey(eventType))
            {
                m_EventHandlers[eventType].Remove(handler);
            }
        }

        public void TriggerEvent<T>(T e) where T : ImmoFrameworkEvent
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

        public void TriggerEventImmediately<T>(T e) where T : ImmoFrameworkEvent
        {
            if (e == null || e.IsCancelled)
            {
                return;
            }

            ProcessEvent(e);
        }

        public override void Update()
        {
            while (true)
            {
                ImmoFrameworkEvent e = null;
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



        private void ProcessEvent(ImmoFrameworkEvent e)
        {
            Type eventType = e.GetType();
            if (m_EventHandlers.ContainsKey(eventType))
            {
                List<IImmoFrameworkEventHandler> handlers = new List<IImmoFrameworkEventHandler>();
                handlers.AddRange(m_EventHandlers[eventType]);

                foreach (IImmoFrameworkEventHandler handler in handlers)
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