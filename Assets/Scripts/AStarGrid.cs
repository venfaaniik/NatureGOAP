using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    public bool displayGizmos;
    public LayerMask unwalkableMask;
    public TerrainData data;
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
        nodeDiameter = data.size.x / gridSizeX;
        nodeRadius = nodeDiameter / 2;
        grid = new AStarNode[gridSizeX, gridSizeY];

        Debug.Log(nodeRadius);
        Debug.Log(nodeDiameter);

        Vector3 worldPos;
        float worldX, worldZ;
        float[,] heightMap = new float[gridSizeX , gridSizeY];

        heightMap = data.GetHeights(0, 0, gridSizeX, gridSizeY);

        Debug.Log(data.size.x);

        for (int y = 0; y < gridSizeY; ++y)
        {
            for (int x = 0; x < gridSizeX; ++x)
            {
                worldX = nodeRadius + x * nodeDiameter;
                worldZ = nodeRadius + y * nodeDiameter;
                worldPos = new Vector3(worldX, heightMap[y, x] * data.heightmapScale.y, worldZ);
                grid[x, y] = new AStarNode(true, worldPos, x, y, 0);
                //Debug.Log(heightMap[x, y]);
                //Debug.Log("xCoord: " + x + " zCoord: " + y + " yCoord: " + data.GetHeight((int)worldX,(int) worldZ));
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (displayGizmos)
        {
            Gizmos.color = Color.red;
            for (int y = 0; y < gridSizeY; ++y)
            {
                for (int x = 0; x < gridSizeX; ++x)
                {
                    if (grid[x, y] != null)
                    {
                        Gizmos.DrawCube(grid[x, y].worldPosition, new Vector3(nodeRadius, nodeRadius, nodeRadius));
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