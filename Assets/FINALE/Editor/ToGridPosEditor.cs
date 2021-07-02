using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ToGridPos)), CanEditMultipleObjects]

public class ToGridPosEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ToGridPos myToGridPos = (ToGridPos)target;


        if (GUILayout.Button("Adjust Position"))
        {
            myToGridPos.AdjustPosition();
        }
    }
}
