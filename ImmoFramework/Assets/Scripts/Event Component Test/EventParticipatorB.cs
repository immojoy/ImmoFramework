using System;
using UnityEngine;


using Immo.Framework;

public class EventParticipatorB : MonoBehaviour
{
    private void Start()
    {
    }

    private void OnGUI()
    {
        if (GUILayout.Button("XXXXXXXXXXXXXXX"))
        {
            // ImmoGameEntry.EventComponent.TriggerEvent(new GreetingEvent(this, "Hello from EventParticipatorB!"));
        }

        if (GUILayout.Button("Trigger Greeting Event"))
        {
            ImmoFrameworkGameEntry.EventComponent.TriggerEvent(new GreetingEvent(this, "Hello from EventParticipatorB!"));
        }
    }
}