using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Movement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        dir.Normalize();

        rb.velocity = dir * speed;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
