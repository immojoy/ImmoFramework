using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ImmoFramework.Runtime;

public class SampleEntityLogic : IFEntityLogic
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (GUILayout.Button("HHHHHHHHHHHHHHHHHHHHide Entity"))
        {
            IFGameEntry.EntityComponent.HideEntity(Entity, null);
        }
    }
}
