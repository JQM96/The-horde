using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI magazineText;

    [SerializeField] private Shooting shootingComponent;

    private float currentAmmo;
    private float currentMag;

    private void Update()
    {
        currentAmmo = shootingComponent.GetCurrentWeapon().ammo;
        currentMag = shootingComponent.GetCurrentWeapon().currentMag;

        if (shootingComponent.GetCurrentWeapon().infiniteAmmo == true)
            ammoText.text = "\u221E";
        else if(ammoText.text == "\u221E")
            ammoText.text = 0.ToString();
        else if (int.Parse(ammoText.text) != currentAmmo)
            ammoText.text = currentAmmo.ToString();

        if (int.Parse(magazineText.text) != currentMag)
            magazineText.text = currentMag.ToString();
    }
}
