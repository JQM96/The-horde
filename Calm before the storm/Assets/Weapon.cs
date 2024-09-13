using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon")]
public class Weapon : ScriptableObject
{
    public float bulletSpeed;
    public float bulletDamge;
    public bool bulletPierce;

    public float fireRate;
    public float reloadTime;
    public int magSize;
    public int ammo;
    public bool infiniteAmmo;
    public float currentMag;

    public int bulletsPerShot;
}
