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

        //for (int i = 0; i < targets.Length; i++)
        //{
        //    (target as ToGridPos).gameObject.GetComponent<ToGridPos>().AdjustPosition();
        //}

        if (GUILayout.Button("Adjust Position"))
        {
            myToGridPos.AdjustPosition();
        }
    }
}
