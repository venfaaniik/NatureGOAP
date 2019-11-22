using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AStarGrid))]
public class NavigationMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AStarGrid navMesh = (AStarGrid)target;
        if (GUILayout.Button("Bake navigation mesh"))
        {
            navMesh.Bake();
        }
    }
}
