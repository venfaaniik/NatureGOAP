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
        Bake();
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

        Vector3 worldPos;
        float worldX, worldZ;
        float height;


        float percent = data.heightmapWidth / gridSizeX;

        for (int y = 0; y < gridSizeY; ++y)
        {
            for (int x = 0; x < gridSizeX; ++x)
            {
                worldX = nodeRadius + x * nodeDiameter;
                worldZ = nodeRadius + y * nodeDiameter;
                height = data.GetHeight(Mathf.FloorToInt(x * percent), Mathf.FloorToInt(y * percent));
                worldPos = new Vector3(worldX, height, worldZ);
                grid[x, y] = new AStarNode(true, worldPos, x, y, 0);
                //Debug.Log(heightMap[x, y]);
                //Debug.Log("xCoord: " + x + " zCoord: " + y + " yCoord: " + data.GetHeight((int)worldX,(int) worldZ));
            }
        }
    }

    public AStarNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        Debug.Log(worldPosition.x +  ":"   + worldPosition.z);
        float percentX = worldPosition.x / data.size.x;
        float percentY = worldPosition.z / data.size.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    public List<AStarNode> GetNeighbours(AStarNode node)
    {
        List<AStarNode> neighbours = new List<AStarNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
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
                    if (grid != null)
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