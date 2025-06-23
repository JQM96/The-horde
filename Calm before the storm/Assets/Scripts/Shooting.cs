using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponManager))]
public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private ParticleSystem muzzleFlash;

    private WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        //Fire!
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire(weaponManager.GetCurrentWeapon());
        }
    }

    private void Fire(Weapon weapon)
    {
        if (weapon == null)
            return;

        if (weapon.currentMag > 0)
        {
            //Set origin and direction
            Vector2 origin = new Vector2(shootingPoint.position.x, shootingPoint.position.y);
            Vector2 direction = shootingPoint.right;

            //Raycast!
            RaycastHit2D hit = Physics2D.Raycast(origin, direction);

            if (hit)
            {
                //Check if it has health & reduce it
                Health health = hit.transform.gameObject.GetComponent<Health>();

                if (health != null)
                    health.TakeDamage(weapon.bulletDamge);
            }

            //Decrease currentmag
            weapon.currentMag -= 1;

            muzzleFlash.Play();
        }
    }
}
