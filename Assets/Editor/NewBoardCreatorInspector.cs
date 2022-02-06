using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NewBoardCreator))]
public class NewBoardCreatorInspector : Editor
{
    public NewBoardCreator current
    {
        get
        {
            return (NewBoardCreator)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("SetTile"))
            current.SetTile();
        if (GUILayout.Button("ResetTile"))
            current.ResetLevel();

        if (GUI.changed)
            current.UpdateMarker();
    }
}
