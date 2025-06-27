using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(WeaponManager))]
public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private ParticleSystem muzzleFlashParticles;
    [SerializeField] private Light2D muzzleFlashLight;
    [SerializeField] private GameObject traceBulltetPrefab;

    private WeaponManager weaponManager;
    private float fireTimer;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        //Fire!
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Fire(weaponManager.GetCurrentWeapon());
        }

        if (muzzleFlashParticles.isPlaying == false)
            muzzleFlashLight.gameObject.SetActive(false);
    }

    private void Fire(Weapon weapon)
    {
        if (weapon == null)
            return;

        if (fireTimer < 1 / weapon.fireRate)
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

            muzzleFlashParticles.Play();

            muzzleFlashLight.gameObject.SetActive(true);

            AudioManager.PlaySound(weapon.fireSound, true);

            fireTimer = 0;

            Instantiate(traceBulltetPrefab, shootingPoint.position, shootingPoint.rotation);
        }
    }
}
