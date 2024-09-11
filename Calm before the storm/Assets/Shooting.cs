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
    private int currentMag;
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
            currentMag = currentWeapon.magSize;
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
                    currentMag = currentWeapon.magSize;
                    reloading = false;
                }
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
                if (currentWeapon != null)
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

    public void ChangeCurrentWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        currentMag = currentWeapon.magSize;
        canFire = true;
        reloading = false;
    }

    public void AddWeapon(Weapon newWeapon)
    {
        weapons.Add(newWeapon);
    }
}
