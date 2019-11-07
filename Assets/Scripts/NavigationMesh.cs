using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMesh : MonoBehaviour
{
    public bool displayGizmos;
    public Vector2 gridDimension; //this has to be same as your meshWidth and MeshHeight
    public LayerMask unwalkableMask;
    public Mesh mesh;
    public int gridSizeX, gridSizeY;

    float nodeRadius;
    float nodeDiameter;

    AStarNode[,] grid;

    public void Bake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        nodeDiameter = gridDimension.x / gridSizeX;
        nodeRadius = nodeDiameter / 2;
        grid = new AStarNode[gridSizeX, gridSizeY];

        int quads = mesh.triangles.Length / 6;
        int quadsPerLine = Mathf.RoundToInt(Mathf.Sqrt(quads));
        float quadLength = gridDimension.x / quadsPerLine;
        int nodesXCountPerQuad = (int)(quadLength / nodeDiameter);

        Debug.Log(nodesXCountPerQuad);
        Debug.Log("Node diameter: " + nodeDiameter);
        Debug.Log("Grid Dimension: " + gridDimension);
        Debug.Log("Quads per line: " + quadsPerLine);
        Debug.Log("Quad Length: " + quadLength);

        float height;

        int verticalCount = 0;
        int horizontalCount = 0;

        //way 2 -- probably better
        for (int i = 0; i < quads; ++i)
        {
            if (horizontalCount == quadsPerLine - 1)
            {
                ++verticalCount;
                horizontalCount = 0;
            }

            int referencePoint = i + verticalCount;
            Vector4 vertices = new Vector4(referencePoint, referencePoint + 1, referencePoint + quadsPerLine, referencePoint + quadsPerLine + 1);

            //Calculate height differences
            float firstDifferenceX = vertices.y - vertices.x;
            float firstDifferenceY = vertices.z - vertices.x;
            float secondDifferenceX = vertices.w - vertices.z;
            float secondDifferenceY = vertices.w - vertices.y;

            float percentX, percentY;
            int currX, currY;

            for (int y = 0; y < nodesXCountPerQuad; ++y)
            {
                for (int x = 0; x < nodesXCountPerQuad; ++x)
                {
                    percentX = (nodeRadius + x * nodeDiameter) / quadLength;
                    percentY = (nodeRadius + y * nodeDiameter) / quadLength;
                    if (x + y < nodesXCountPerQuad) //Lower Triangle
                    {
                        height = ((firstDifferenceX * percentX + mesh.vertices[(int)vertices.x].y) + (firstDifferenceY * percentY + mesh.vertices[(int)vertices.x].y)) / 2;
                    }
                    else //Upper Triangle
                    {
                        height = ((secondDifferenceX * percentX + mesh.vertices[(int)vertices.z].y) + (secondDifferenceY * percentY + mesh.vertices[(int)vertices.y].y)) / 2;
                    }
                    Vector3 worldPos = new Vector3(horizontalCount * nodesXCountPerQuad + x * nodeDiameter + nodeRadius, height, i * verticalCount + y * nodeDiameter + nodeRadius); //DOUBLE CHECK
                    currX = horizontalCount * nodesXCountPerQuad + x;
                    currY = verticalCount * nodesXCountPerQuad + y;
                    if (currX < gridSizeX && currY < gridSizeY)
                        grid[currX, currY] = new AStarNode(true, worldPos, currX, currY, 0);

                    Debug.Log("curr X: " + currX + " currY: " + currY);
                }
            }

            ++horizontalCount;
        }


    }


}