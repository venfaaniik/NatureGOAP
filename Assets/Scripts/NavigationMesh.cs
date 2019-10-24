using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMesh : MonoBehaviour
{
    public bool displayGizmos;
    public Vector2 gridSize; //this has to be same as your meshWidth and MeshHeight
    public LayerMask unwalkableMask;
    public Mesh mesh;
    public float nodeRadius;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    NavNode[,] grid;

    public void Bake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridSize.y / nodeDiameter);

        int verticesPerLine = (int)Mathf.Sqrt(mesh.vertexCount);

        int nodesPerLine = Mathf.RoundToInt(gridSize.x / verticesPerLine);

        Debug.Log("gridSizeX: " + gridSizeX);
        Debug.Log("nodes per line: " + nodesPerLine); //Debug

    }

}