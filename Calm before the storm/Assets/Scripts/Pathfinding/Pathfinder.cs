using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Pathfinder
{
    const int DIAGONAL_COST = 14;
    const int STRAIGHT_COST = 10;


    NodeGrid nodeGrid;

    List<Node> openNodes; //Nodes queued up for searching
    List<Node> closedNodes; //Nodes that have already been searched

    Node currentNode;

    private List<Node> FindPath(Vector2Int start, Vector2Int end)
    {
        Node startNode = nodeGrid.GetNode(start.x, start.y);
        Node endNode = nodeGrid.GetNode(end.x, end.y);

        openNodes = new List<Node> { startNode };
        closedNodes = new List<Node>();

        for (int i = 0; i < nodeGrid.worldSize.x; i++)
        {
            for (int j = 0; j < nodeGrid.worldSize.x; j++)
            {
                //Get & initialize current node
                currentNode = nodeGrid.GetNode(i, j);
                currentNode.Initialize();

                currentNode.CalculateFCost();
            }
        }

        //Calculating costs for start node
        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while (openNodes.Count > 0)
        {
            currentNode = GetLowestFCostNode();

            if (currentNode == endNode) //Reached final node!
                return CalculatePath(endNode);

            //Current node has already been searched. Send it to the closed list.
            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            //Get current node neighbours and search them.
            foreach (Node neighbour in GetNeighbourList(currentNode))
            {
                if (closedNodes.Contains(neighbour)) continue; //Already searched this neighbour.

                //Calculate tentaive G cost. Use this to check against currentNode.gCost.
                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighbour);

                if (tentativeGCost < currentNode.gCost) //We have a better path! Obtain it!
                {
                    neighbour.previousNode = currentNode;
                    neighbour.gCost = tentativeGCost;
                    neighbour.hCost = CalculateDistance(neighbour, endNode);
                    neighbour.CalculateFCost();

                    //If for some reason its not on the open list add it
                    if (!openNodes.Contains(neighbour))
                        openNodes.Add(neighbour);
                }
            }
        }

        //We are out of nodes to search!
        return null;
    }

    private List<Node> GetNeighbourList(Node n)
    {
        List<Node> neighbours = new List<Node>();

        if (n.x - 1 >= 0)
        {
            //Left position is valid. Add it and check diagonals.
            neighbours.Add(nodeGrid.GetNode(n.x - 1, n.y)); //Left

            if (n.y + 1 < nodeGrid.worldSize.y)
                neighbours.Add(nodeGrid.GetNode(n.x - 1, n.y + 1)); //Left-Up

            if (n.y - 1 >= 0)
                neighbours.Add(nodeGrid.GetNode(n.x - 1, n.y - 1)); //Left-Down
        }

        if (n.x + 1 < nodeGrid.worldSize.x)
        {
            //Right position is valid. Add it and check diagonals.
            neighbours.Add(nodeGrid.GetNode(n.x + 1, n.y)); //Right

            if (n.y + 1 < nodeGrid.worldSize.y)
                neighbours.Add(nodeGrid.GetNode(n.x + 1, n.y + 1)); //Right-Up

            if (n.y - 1 >= 0)
                neighbours.Add(nodeGrid.GetNode(n.x - 1, n.y - 1)); //Right-Down
        }

        if (n.y + 1 < nodeGrid.worldSize.y) //Up
        {
            neighbours.Add(nodeGrid.GetNode(n.x, n.y + 1));
        }

        if (n.y - 1 >= 0) //Down
        {
            neighbours.Add(nodeGrid.GetNode(n.x, n.y - 1));
        }

        return neighbours;
    }

    private List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);

        //Loop until we reach a node without a previousNode.
        Node current = endNode;
        while (current.previousNode != null)
        {
            current = current.previousNode;
        }

        path.Reverse();
        return path;
    }

    private Node GetLowestFCostNode()
    {
        Node lowestFCostNode = openNodes[0];

        foreach (Node n in openNodes)
        {
            if (n.fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = n;
            }
        }

        return lowestFCostNode;
    }

    private int CalculateDistance(Node start, Node end)
    {
        //Move all thath you can diagonally, then move straight

        int xDistance = (int)MathF.Abs(start.x - end.x);
        int yDistance = (int)MathF.Abs(start.y - end.y);
        int remaining = (int)MathF.Abs(xDistance - yDistance);

        return (int)(DIAGONAL_COST * MathF.Min(xDistance, yDistance) + STRAIGHT_COST * remaining);
    }
}
