using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x { get; private set; }
    public int y { get; private set; }

    public int gCost; //Walking cost from the start node
    public int hCost; //Heuristic cost to reach end node
    public int fCost; //F = G + H

    bool isWalkable;

    public Node previousNode;

    public Node(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public void Initialize()
    {
        gCost = int.MaxValue;
        previousNode = null;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
