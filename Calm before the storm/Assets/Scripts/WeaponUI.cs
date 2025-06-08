using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI magazineText;
    [SerializeField] private Slider reloadBar;

    [SerializeField] private Shooting shootingComponent;

    private float currentAmmo;
    private float currentMag;
    private float reloadTime;
    private float reloadProgress;

    private void Update()
    {
        //currentAmmo = shootingComponent.GetCurrentWeapon().ammo;
        //currentMag = shootingComponent.GetCurrentWeapon().currentMag;
        //reloadTime = shootingComponent.GetCurrentWeapon().reloadTime;
        //reloadProgress = shootingComponent.GetReloadTimer();

        //if (shootingComponent.GetCurrentWeapon().infiniteAmmo == true)
            //ammoText.text = "\u221E";
        /*else*/ if (ammoText.text == "\u221E")
            ammoText.text = 0.ToString();
        else if (int.Parse(ammoText.text) != currentAmmo)
            ammoText.text = currentAmmo.ToString();

        if (int.Parse(magazineText.text) != currentMag)
            magazineText.text = currentMag.ToString();

        //if (shootingComponent.IsReloading())
        {
            reloadBar.gameObject.SetActive(true);

            if (reloadBar.maxValue != reloadTime)
                reloadBar.maxValue = reloadTime;

            if (reloadBar.value != reloadProgress)
                reloadBar.value = reloadProgress;

        }
        //else
            reloadBar.gameObject.SetActive(false);
    }
}
