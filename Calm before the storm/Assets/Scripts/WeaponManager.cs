using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    List<Weapon> weapons; // This will change when we add the inventory!
    Weapon currentWeapon;

    private void Awake()
    {
        weapons = new List<Weapon>();
    }

    private void Update()
    {
        //This will change when we add the inventory!
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.Count >= 1)
        {
            currentWeapon = weapons[0];
            MessageBox.instance?.SpawnMessage("Equiped " + currentWeapon.weaponName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count >= 2)
        {
            currentWeapon = weapons[1];
            MessageBox.instance?.SpawnMessage("Equiped " + currentWeapon.weaponName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Count >= 3)
        {
            currentWeapon = weapons[2];
            MessageBox.instance?.SpawnMessage("Equiped " + currentWeapon.weaponName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Count >= 4)
        {
            currentWeapon = weapons[3];
            MessageBox.instance?.SpawnMessage("Equiped " + currentWeapon.weaponName);
        }
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void AddWeapon(Weapon newWeapon)
    {
        weapons.Add(newWeapon);
    }

    public Weapon GetRandomWeapon()
    {
        if (weapons.Count > 0)
        {
            return weapons[Random.Range(0, weapons.Count)];
        }
        else
        {
            return null;
        }
        
    }
}
