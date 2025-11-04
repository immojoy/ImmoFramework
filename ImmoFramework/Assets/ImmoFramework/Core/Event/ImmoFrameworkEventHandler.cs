using System;
using System.ComponentModel;


namespace Immo.Framework.Core.Event
{
    public enum ImmoFrameworkEventHandlerPriority
    {
        Lowest = 0,
        Low = 1,
        Normal = 2,
        High = 3,
        Highest = 4,
        Monitor = 5
    }


    /// <summary>
    /// Base event handler interface.
    /// </summary>
    internal interface IImmoFrameworkEventHandler
    {
        public ImmoFrameworkEventHandlerPriority Priority { get; }
        public Type EventType { get; }
        public void HandleEvent(ImmoFrameworkEvent e);
    }


    /// <summary>
    /// Generic event handler interface for events of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of event being handled.</typeparam>
    internal interface IImmoFrameworkEventHandler<T> : IImmoFrameworkEventHandler where T : ImmoFrameworkEvent
    {
        public void HandleEvent(T e);
    }


    /// <summary>
    /// Base class for handling events of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of event being handled.</typeparam>
    /// <remarks>
    /// Inherit from this class to create custom event handlers for specific event types.
    /// </remarks>
    public abstract class ImmoFrameworkEventHandler<T> : IImmoFrameworkEventHandler<T> where T : ImmoFrameworkEvent
    {
        public virtual ImmoFrameworkEventHandlerPriority Priority { get; }
        public Type EventType => typeof(T);

        public abstract void HandleEvent(T e);

        public void HandleEvent(ImmoFrameworkEvent e)
        {
            if (e is T typedEvent)
            {
                HandleEvent(typedEvent);
            }
        }
    }
}