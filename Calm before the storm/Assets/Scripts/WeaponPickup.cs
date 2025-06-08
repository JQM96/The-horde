using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private AudioClip pickupSound;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = weapon.pickUpSprite;

        int randomZ = Random.Range(-90, 90);

        transform.eulerAngles = new Vector3(0, 0, randomZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponManager weaponManager = collision.GetComponent<WeaponManager>();

        if (weaponManager != null)
        {
            weaponManager.AddWeapon(weapon);

            AudioManager.PlaySound(pickupSound, true);
            Destroy(gameObject);
        }
    }
}
