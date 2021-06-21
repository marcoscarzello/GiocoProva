using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ToSemiGridPos)), CanEditMultipleObjects]

public class ToSemiGridPosEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ToSemiGridPos myToSemiGridPos = (ToSemiGridPos)target;

        //for (int i = 0; i < targets.Length; i++)
        //{
        //    (target as ToSemiGridPos).gameObject.GetComponent<ToSemiGridPos>().AdjustPosition();
        //}

        if (GUILayout.Button("Adjust Position"))
        { 
            myToSemiGridPos.AdjustPosition();
        }
    }

}
