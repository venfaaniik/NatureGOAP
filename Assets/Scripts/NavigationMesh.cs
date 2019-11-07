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

    private void Start()
    {
        CreateGrid();       
    }

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

        /*Debug.Log("Nodes per QuadX " + nodesXCountPerQuad);
        Debug.Log("Node diameter: " + nodeDiameter);
        Debug.Log("Grid Dimension: " + gridDimension);
        Debug.Log("Quads per line: " + quadsPerLine);
        Debug.Log("Quad Length: " + quadLength);*/

        float height;

        int verticalCount = 0;
        int horizontalCount = 0;

        //way 2 -- probably better
        for (int i = 0; i < quads; ++i)
        {
            if (horizontalCount == quadsPerLine)
            {
                ++verticalCount;
                horizontalCount = 0;
            }

            //Debug.Log(i + " " + horizontalCount + " " + verticalCount);

            int referenceIndex = i + verticalCount;
            int lowerLeft = referenceIndex;
            int lowerRight = referenceIndex + 1;
            int upperLeft = referenceIndex + quadsPerLine;
            int upperRight = referenceIndex + quadsPerLine + 1;


            float firstDifferenceX, firstDifferenceY, secondDifferenceX, secondDifferenceY;

            if (upperRight < mesh.vertexCount)
            {
                //Calculate height differences
                firstDifferenceX = mesh.vertices[lowerRight].y - mesh.vertices[lowerLeft].y;
                firstDifferenceY = mesh.vertices[upperLeft].y - mesh.vertices[lowerLeft].y;
                secondDifferenceX = mesh.vertices[upperRight].y - mesh.vertices[upperLeft].y;
                secondDifferenceY = mesh.vertices[upperRight].y - mesh.vertices[lowerRight].y;
            }
            else
            {
                Debug.LogError("Index was out of bounds: " + upperRight);
                return;
            }


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
                        height = ((firstDifferenceX * percentX + mesh.vertices[lowerLeft].y) + (firstDifferenceY * percentY + mesh.vertices[lowerLeft].y)) / 2;
                    }
                    else //Upper Triangle
                    {
                        height = ((secondDifferenceX * percentX + mesh.vertices[upperLeft].y) + (secondDifferenceY * percentY + mesh.vertices[lowerRight].y)) / 2;
                    }
                    currX = horizontalCount * nodesXCountPerQuad + x;
                    currY = verticalCount * nodesXCountPerQuad + y;
                    Vector3 worldPos = new Vector3(currX * nodeDiameter + nodeRadius, height, currY * nodeDiameter + nodeRadius); //DOUBLE CHECK
                    if (currX < gridSizeX && currY < gridSizeY)
                        grid[currX, currY] = new AStarNode(true, worldPos, currX, currY, 0);

                    //Debug.Log("curr X: " + currX + " currY: " + currY);
                }
            }

            ++horizontalCount;
        }


    }

    private void OnDrawGizmos()
    {
        if (displayGizmos)
        {
            Gizmos.color = Color.red;
            for (int y = 0; y < gridSizeY; ++y)
            {
                for (int x = 0; x < gridSizeX; x++)
                {
                    if (grid[x, y] != null)
                    {
                        Gizmos.DrawCube(grid[x, y].worldPosition, new Vector3(nodeDiameter, nodeDiameter, nodeDiameter));
                        //Debug.Log(grid[x, y].worldPosition.x);
                    }
                }
                if (Gizmos.color == Color.red)
                {
                    Gizmos.color = Color.black;

                }
                else
                {
                    Gizmos.color = Color.red;

                }
            }
        }
    }


}