using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Enemy[] enemies;

    private void Start()
    {
        enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID);
    }
}
