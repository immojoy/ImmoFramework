using System;
using UnityEngine;

using Immo.Framework;
using Immo.Framework.Core;
using Immo.Framework.Core.Event;

public class GreetingEvent : ImmoFrameworkEvent
{
    public string Message { get; private set; }

    public GreetingEvent(object source, string message)
        : base(source)
    {
        Message = message;
    }
}

public class GreetingEventHandler : ImmoFrameworkEventHandler<GreetingEvent>
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
            ImmoFrameworkGameEntry.EventComponent.RegisterEventHandler(new GreetingEventHandler());
        }
    }
}