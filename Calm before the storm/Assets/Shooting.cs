using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Weapon startingWeapon;

    private Weapon currentWeapon;
    private int currentWeaponIndex = 0;
    private List<Weapon> weapons;

    private bool canFire = true;
    private float nextShotTime;
    private float fireTimer;
    private bool reloading;
    private float reloadTimer;

    private void Start()
    {
        weapons = new List<Weapon>();

        if (startingWeapon != null)
        {
            weapons.Add(startingWeapon);
            currentWeapon = weapons[currentWeaponIndex];
        }

        if (currentWeapon != null)
            currentWeapon.currentMag = currentWeapon.magSize;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeaponIndex -= 1;
            if (currentWeaponIndex < 0)
                currentWeaponIndex = weapons.Count - 1;
            ChangeCurrentWeapon(weapons[currentWeaponIndex]);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentWeaponIndex += 1;
            if (currentWeaponIndex >= weapons.Count)
                currentWeaponIndex = 0;

            ChangeCurrentWeapon(weapons[currentWeaponIndex]);
        }

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
            if (currentWeapon != null)
            {
                reloadTimer += Time.deltaTime;
                if (reloadTimer >= currentWeapon.reloadTime)
                {
                    if (currentWeapon.infiniteAmmo)
                    {
                        currentWeapon.currentMag = currentWeapon.magSize;
                    }
                    else if (currentWeapon.ammo < currentWeapon.magSize)
                    {
                        currentWeapon.currentMag = currentWeapon.ammo;
                        currentWeapon.ammo = 0;
                    }
                    else
                    {
                        currentWeapon.currentMag = currentWeapon.magSize;
                        currentWeapon.ammo -= currentWeapon.magSize;
                    }


                    reloading = false;
                }
            }
        }

        if (reloading == false && canFire == true && Input.GetKey(KeyCode.Mouse0))
        {
            if (currentWeapon.currentMag <= 0)
            {
                reloading = true;
                reloadTimer = 0;
            }
            else
            {
                if (currentWeapon != null)
                {
                    Vector2 dir = bulletSpawnPoint.rotation * Vector2.up;

                    if (currentWeapon.bulletsPerShot <= 1)
                    {
                        GameObject obj = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                        obj.GetComponent<Bullet>().SetBulletProperties(currentWeapon.bulletSpeed, currentWeapon.bulletDamge, currentWeapon.bulletPierce);

                        obj.GetComponent<Bullet>().SetVelocity(dir * currentWeapon.bulletSpeed);
                    }
                    else
                    {
                        float bulletSpread = 0.4f;

                        for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
                        {
                            GameObject obj = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                            obj.GetComponent<Bullet>().SetBulletProperties(currentWeapon.bulletSpeed, currentWeapon.bulletDamge, currentWeapon.bulletPierce);

                            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-bulletSpread, bulletSpread);

                            obj.GetComponent<Bullet>().SetVelocity((dir + pdir) * currentWeapon.bulletSpeed);
                        }
                    }

                    canFire = false;
                    nextShotTime = 1 / currentWeapon.fireRate;
                    fireTimer = 0;
                    currentWeapon.currentMag -= 1;
                }
            }
        }
    }

    public void ChangeCurrentWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;

        canFire = true;
        reloading = false;
    }

    public void AddWeapon(Weapon newWeapon)
    {
        bool foundMatch = false;

        foreach (Weapon w in weapons)
        {
            if (w.name == newWeapon.name)
            {
                AddAmmoToWeapon(w.magSize / 4, w);
                Debug.Log("Added " + w.magSize / 4 + " bullets to " + w.name);
                foundMatch = true;
            }
        }

        if (foundMatch == false)
        {
            newWeapon.currentMag = newWeapon.magSize;
            newWeapon.ammo = 0;

            weapons.Add(newWeapon);
        }
    }

    public void AddMagsizeToAmmoCurrentWeapon()
    {
        if (currentWeapon.infiniteAmmo == true)
        {
            if (currentWeaponIndex + 1 < weapons.Count)
            {
                weapons[currentWeaponIndex + 1].ammo += weapons[currentWeaponIndex + 1].magSize;
            }
        }
        else
            currentWeapon.ammo += currentWeapon.magSize;
    }

    public void AddAmmoToWeapon(int ammoToAdd, Weapon weapon)
    {
        weapon.ammo += ammoToAdd;
    }
}
