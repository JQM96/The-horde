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

    public Pathfinder(NodeGrid grid)
    {
        this.nodeGrid = grid;
    }

    public List<Vector3> FindPath(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        nodeGrid.GetXY(startWorldPos, out int startX, out int startY);
        nodeGrid.GetXY(endWorldPos, out int endX, out int endY);

        Vector2Int startVec = new Vector2Int(startX, startY);
        Vector2Int endVec = new Vector2Int(endX, endY);

        List<Node> path = FindPath(startVec, endVec);

        if (path == null)
            return null;
        else
        {
            //Translate node to vector 3
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (Node n in path)
            {
                Vector3 nWorldPos = nodeGrid.GetWorldPosition(n.x, n.y);
                vectorPath.Add(nWorldPos + Vector3.one * nodeGrid.GetCellSize() * .5f);
            }

            return vectorPath;
        }
    }

    private List<Node> FindPath(Vector2Int start, Vector2Int end)
    {
        Node startNode = nodeGrid.GetNode(start.x, start.y);
        Node endNode = nodeGrid.GetNode(end.x, end.y);

        openNodes = new List<Node> { startNode };
        closedNodes = new List<Node>();

        for (int i = 0; i < nodeGrid.GetWorldSize().x; i++)
        {
            for (int j = 0; j < nodeGrid.GetWorldSize().y; j++)
            {
                //Get & initialize all nodes
                Node nodeToInit = nodeGrid.GetNode(i, j);
                nodeToInit.Initialize();
            }
        }

        //Calculating costs for start node
        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();

        while (openNodes.Count > 0)
        {
            Node currentNode = GetLowestFCostNode(openNodes);

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

                if (tentativeGCost < neighbour.gCost) //We have a better path! Obtain it!
                {
                    neighbour.previousNode = currentNode;
                    neighbour.gCost = tentativeGCost;
                    neighbour.hCost = CalculateDistance(neighbour, endNode);
                    neighbour.CalculateFCost();

                    //Add it to the open Nodes list
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

            if (n.y + 1 < nodeGrid.GetWorldSize().y)
                neighbours.Add(nodeGrid.GetNode(n.x - 1, n.y + 1)); //Left-Up

            if (n.y - 1 >= 0)
                neighbours.Add(nodeGrid.GetNode(n.x - 1, n.y - 1)); //Left-Down
        }

        if (n.x + 1 < nodeGrid.GetWorldSize().x)
        {
            //Right position is valid. Add it and check diagonals.
            neighbours.Add(nodeGrid.GetNode(n.x + 1, n.y)); //Right

            if (n.y + 1 < nodeGrid.GetWorldSize().y)
                neighbours.Add(nodeGrid.GetNode(n.x + 1, n.y + 1)); //Right-Up

            if (n.y - 1 >= 0)
                neighbours.Add(nodeGrid.GetNode(n.x + 1, n.y - 1)); //Right-Down
        }

        if (n.y + 1 < nodeGrid.GetWorldSize().y) //Up
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
            path.Add(current.previousNode);

            current = current.previousNode;
        }

        path.Reverse();
        return path;
    }

    private Node GetLowestFCostNode(List<Node> nodeList)
    {
        Node lowestFCostNode = nodeList[0];

        foreach (Node n in nodeList)
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
