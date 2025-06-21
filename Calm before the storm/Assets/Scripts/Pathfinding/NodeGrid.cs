using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    [SerializeField] Vector2 worldSize;
    [SerializeField] private float cellSize;

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

                /*GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = GetWorldPosition(i, j) + new Vector3(cellSize, cellSize) * 0.5f;
                sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);*/
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i < worldSize.x; i++)
        {
            for (int j = 0; j < worldSize.y; j++)
            {
                Gizmos.DrawWireSphere(GetWorldPosition(i, j) + new Vector3(cellSize, cellSize) * 0.5f, 0.1f);
            }
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
