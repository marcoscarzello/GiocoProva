using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LightPlacer)), CanEditMultipleObjects]

public class LightPlacerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LightPlacer myLP = (LightPlacer)target;


        if (GUILayout.Button("Place These Lights"))
        {
            myLP.DoThePlacement();
        }
    }
}
