using System;
using UnityEngine;

using ImmoFramework.Runtime;

public class GreetingEvent : IFEvent
{
    public string Message { get; private set; }

    public GreetingEvent(object source, string message)
        : base(source)
    {
        Message = message;
    }
}

public class GreetingEventHandler : IFEventHandler<GreetingEvent>
{
    public override void HandleEvent(GreetingEvent e)
    {
        Debug.Log($"GreetingEvent received with message: {e.Message}, from source: {e.Source}");
    }
}


public class EventParticipatorA : MonoBehaviour
{
    private void Start()
    {
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Register Greeting Event"))
        {
            IFGameEntry.EventComponent.RegisterHandler(new GreetingEventHandler());
        }
    }
}