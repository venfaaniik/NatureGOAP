using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TerrainToMeshWindow : EditorWindow
{

    string meshName = "Mesh Name";
    string path = "";
    int sampleSize = 0;
    int meshWidth = 0;
    bool lowestPointToZero = true;
    TerrainData data;

    public Mesh mesh;
    

    [MenuItem("Window/Terrain to Mesh")]
    static void Init()
    {
        TerrainToMeshWindow window = (TerrainToMeshWindow)EditorWindow.GetWindow(typeof(TerrainToMeshWindow));
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        meshName = EditorGUILayout.TextField("Mesh Name", meshName);
        //path = EditorGUILayout.TextField("Path for mesh", path);
        sampleSize = EditorGUILayout.IntField("Number of squares in row:", sampleSize);
        meshWidth = EditorGUILayout.IntField("Width of the mesh:", meshWidth);
        lowestPointToZero = EditorGUILayout.Toggle("Lowest point to 0", lowestPointToZero);
        data = (TerrainData)EditorGUILayout.ObjectField(data, typeof(TerrainData), true);


        if (GUILayout.Button("Generate Mesh"))
        {
            mesh = TerrainToMesh.CreateMeshFromTerrainData(sampleSize, meshWidth, data, lowestPointToZero);
            string assetPath = path + meshName;
            TerrainToMesh.CreateMesh(mesh, meshName);
        }
    }

}
