using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomCreation))]
public class LandGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //Reference to the map generator
        RandomCreation randGen = (RandomCreation)target;
        //If any value was changed, generate map again
        if (DrawDefaultInspector())
        {
            if (randGen.autoUpdate)
            {
                randGen.DrawInEditor();
            }
        }
        //create the button and generate map whenever we press it
        if (GUILayout.Button("Generate"))
        {
            randGen.DrawInEditor();
        }
    }
}