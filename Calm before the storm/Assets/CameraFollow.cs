using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float zOffset = -10f;

    private void FixedUpdate()
    {
        Vector3 playerPos = new Vector3(target.position.x, target.position.y, zOffset);
        transform.position = playerPos;
    }
}
