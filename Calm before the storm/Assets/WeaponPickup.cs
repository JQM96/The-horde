using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private List<Weapon> allWeapons;

    private void Start()
    {
        if (weapon == null)
        {
            int randomIndex = Random.Range(0, allWeapons.Count);
            weapon = allWeapons[randomIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shooting shootingComponent = collision.GetComponent<Shooting>();

        if (shootingComponent != null)
        {
            shootingComponent.AddWeapon(weapon);
            Destroy(gameObject);
        }
    }
}
