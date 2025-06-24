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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = weapons[0];
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
}
