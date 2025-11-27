using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ImmoFramework.Runtime;

public class EntityComponentTest : MonoBehaviour
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
        if (GUILayout.Button("Show Entity"))
        {
            IFGameEntry.EntityComponent.ShowEntity(1, typeof(SampleEntityLogic), "TestCube", "TestGroup", null);
        }


    }
}
