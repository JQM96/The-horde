using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Weapon currentWeapon;

    private bool canFire = true;
    private float nextShotTime;
    private float fireTimer;
    private int currentMag;
    private bool reloading;
    private float reloadTimer;

    private void Start()
    {
        currentMag = currentWeapon.magSize;
    }

    private void Update()
    {
        if (canFire == false)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= nextShotTime)
            {
                canFire = true;
            }
        }

        if (reloading == true)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= currentWeapon.reloadTime)
            {
                currentMag = currentWeapon.magSize;
                reloading = false;
            }
        }

        if (reloading == false && canFire == true && Input.GetKey(KeyCode.Mouse0))
        {
            if (currentMag <= 0)
            {
                reloading = true;
                reloadTimer = 0;
            }
            else
            {
                GameObject obj = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                obj.GetComponent<Bullet>().SetBulletProperties(currentWeapon.bulletSpeed, currentWeapon.bulletDamge, currentWeapon.bulletPierce);

                canFire = false;
                nextShotTime = 1 / currentWeapon.fireRate;
                fireTimer = 0;
                currentMag -= 1;
            }
        }
    }
}
