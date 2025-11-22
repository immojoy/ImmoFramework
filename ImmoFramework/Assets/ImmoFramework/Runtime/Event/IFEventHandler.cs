
using System;


namespace ImmoFramework.Runtime
{
    /// <summary>
    /// Defines the priority levels for event handlers.
    /// </summary>
    public enum IEventHandlerPriority
    {
        Lowest = 0,
        Low = 1,
        Normal = 2,
        High = 3,
        Highest = 4,
    }


    internal interface IIFEventHandler
    {
        IEventHandlerPriority Priority { get; }
        Type EventType { get; }
        public void HandleEvent(IFEvent e);
    }


    internal interface IIFEventHandler<T> : IIFEventHandler where T : IFEvent
    {
        public void HandleEvent(T e);
    }


    public abstract class IFEventHandler<T> : IIFEventHandler<T> where T : IFEvent
    {
        public virtual IEventHandlerPriority Priority { get; } = IEventHandlerPriority.Normal;
        public Type EventType => typeof(T);

        public abstract void HandleEvent(T e);

        public void HandleEvent(IFEvent e)
        {
            if (e is T typedEvent)
            {
                HandleEvent(typedEvent);
            }
        }
    }
}