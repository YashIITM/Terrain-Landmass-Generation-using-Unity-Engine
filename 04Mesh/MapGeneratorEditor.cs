using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//We want to generate the map within the editor so we use the UnityEditor to modify the editor controls to call MapGenerator
//Extend from editor
//add following line to eneble the Unity engine to access and include the custom editor
[CustomEditor(typeof(MapGenerator))]

public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {   //We start off by getting a reference to out MapGenerator
        //target = object that this custom editor is inspecting
        //We just want to cast the object  to a MapGenerator
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap();
            }
        }
            //lets add in a button and add functionality to the button
            if (GUILayout.Button("Generate"))
            {
                mapGen.GenerateMap();
            }

    }
}
