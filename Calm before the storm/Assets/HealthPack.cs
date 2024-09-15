using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private AudioClip pickUpSound;

    private int randomHeal;

    private void Start()
    {
        randomHeal = Random.Range(50, 75);

        int randomZ = Random.Range(-90, 90);

        transform.eulerAngles = new Vector3(0, 0, randomZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movement movement = collision.GetComponent<Movement>();

        if (movement != null)
        {
            collision.GetComponent<Health>().Heal(randomHeal);

            MessageBox.instance.SpawnMessage("HEALED!");

            AudioManager.PlaySound(pickUpSound);
            Destroy(gameObject);
        }
    }
}
