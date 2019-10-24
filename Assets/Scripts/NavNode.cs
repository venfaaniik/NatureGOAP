using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//kNode koska Unity ei tykänny pelkästä Node:sta
public class NavNode : IHeapItem<NavNode>
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int movementPenalty;

    public int gCost;
    public int hCost;
    public NavNode parent;
    public Vector4 vertices;
    int heapIndex;

    public NavNode(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, int _penalty, Vector4 _vertices)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        movementPenalty = _penalty;
        vertices = _vertices;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(NavNode nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return -compare;
    }
}
