using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;

    private void Start()
    {
        int randomZ = Random.Range(-90, 90);

        transform.eulerAngles = new Vector3(0, 0, randomZ);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponManager weaponManager = collision.GetComponent<WeaponManager>();

        if (weaponManager != null)
        {
            Weapon w = weaponManager.GetRandomWeapon();

            if (w != null)
            {
                MessageBox.instance?.SpawnMessage("Found " + w.weaponName + " ammo");

                AudioManager.PlaySound(pickupSound);

                Destroy(gameObject);
            }
        }
    }
}
