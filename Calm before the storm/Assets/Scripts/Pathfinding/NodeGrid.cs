using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;
using Color = UnityEngine.Color;

public class NodeGrid : MonoBehaviour
{
    [SerializeField] Vector2 worldSize;
    [SerializeField] private float cellSize;
    [SerializeField] private LayerMask obstacleLayer;

    private List<Node> _nodes;

    private void Awake()
    {
        _nodes = new List<Node>();

        for (int i = 0; i < worldSize.x; i++)
        {
            for (int j = 0; j < worldSize.y; j++)
            {
                Node n = new Node(i, j);
                _nodes.Add(n);

                //Check if there is an obstacle
                RaycastHit2D hit = Physics2D.BoxCast(GetWorldPosition(i, j),
                                                     new Vector2(cellSize / 2, cellSize / 2),
                                                     0,
                                                     Vector2.right,
                                                     cellSize / 2,
                                                     obstacleLayer);

                if (hit == true)
                {
                    n.SetWalkable(false);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;

        foreach (Node n in _nodes)
        {
            if (n.isWalkable == true)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawWireSphere(GetWorldPosition(n.x, n.y) + new Vector3(cellSize, cellSize) * 0.5f, 0.1f);
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
    public void GetXY(Vector3 startWorldPos, out int x, out int y)
    {
        y = Mathf.FloorToInt(startWorldPos.y / cellSize);
        x = Mathf.FloorToInt(startWorldPos.x / cellSize);
    }

    public Node GetNode(int x, int y)
    {
        foreach (Node n in _nodes)
        {
            if (n.x == x && n.y == y)
            {
                return n;
            }
        }

        Debug.Log("[" + x + "," + y + "] not found...");
        return null;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector2 GetWorldSize()
    {
        return worldSize;
    }
}
