using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NavigationMesh))]
public class NavigationMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NavigationMesh navMesh = (NavigationMesh)target;
        if (GUILayout.Button("Bake navigation mesh"))
        {
            navMesh.Bake();
        }
    }
}
