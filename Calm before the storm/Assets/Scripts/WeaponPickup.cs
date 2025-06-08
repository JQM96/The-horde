using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private List<Weapon> allWeapons;

    [SerializeField] private AudioClip pickupSound;

    private void Start()
    {
        if (weapon == null)
        {
            int randomIndex = Random.Range(0, allWeapons.Count);
            weapon = allWeapons[randomIndex];

        }

        GetComponent<SpriteRenderer>().sprite = weapon.pickUpSprite;

        int randomZ = Random.Range(-90, 90);

        transform.eulerAngles = new Vector3(0, 0, randomZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shooting shootingComponent = collision.GetComponent<Shooting>();

        if (shootingComponent != null)
        {
            shootingComponent.AddWeapon(weapon);

            AudioManager.PlaySound(pickupSound, true);
            Destroy(gameObject);
        }
    }
}
