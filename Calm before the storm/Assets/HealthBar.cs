using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealthComponent;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    [SerializeField] private float lerpSpeed = 0.05f;

    private void Update()
    {
        if (playerHealthComponent != null)
        {
            if (healthSlider.value != playerHealthComponent.GetCurrentHealth())
                healthSlider.value = playerHealthComponent.GetCurrentHealth();

            if (healthSlider.value != easeHealthSlider.value)
                easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }
}
