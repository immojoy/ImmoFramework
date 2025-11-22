using System;
using UnityEngine;


using ImmoFramework.Runtime;

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
            IFGameEntry.EventComponent.TriggerEvent(new GreetingEvent(this, "Hello from EventParticipatorB!"));
        }
    }
}