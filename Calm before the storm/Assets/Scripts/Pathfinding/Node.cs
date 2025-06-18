using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x { get; private set; }
    public int y { get; private set; }

    int gCost; //Walking cost from the start node
    int hCost; //Heuristic cost to reach end node
    int fCost; //F = G + H

    bool isWalkable;

    Node previousNode;

    public Node(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
