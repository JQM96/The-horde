using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSequence : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private CanvasGroup flashCanvasGroup;

    [SerializeField] private AudioClip fireSound;

    [SerializeField] private SceneChanger changer;

    bool isFading = false;
    bool isFlashing = false;
    float alphaVariance;

    float flashFreezeAmount = 0.1f;
    float flashFreezeTimer = 0;

    private void Start()
    {
        FadeIn(2);
    }

    private void Update()
    {
        if (isFlashing)
        {
            flashFreezeTimer += Time.deltaTime;

            if (flashFreezeTimer >= flashFreezeAmount)
            {
                flashCanvasGroup.alpha -= Time.deltaTime;
                mainCanvasGroup.alpha -= Time.deltaTime;
            }
        }

        if (isFading)
            mainCanvasGroup.alpha += alphaVariance;

        if (mainCanvasGroup.alpha >= 1 || mainCanvasGroup.alpha <= 0)
            isFading = false;

        if (isFlashing &&
            flashCanvasGroup.alpha <= 0 &&
            mainCanvasGroup.alpha <= 0)
        {
            changer.ChangeScene("House");
        }
    }

    public void FadeIn(float fadeInTime)
    {
        isFading = true;
        alphaVariance = fadeInTime * Time.deltaTime;
    }

    public void FadeOut(float fadeOutTime)
    {
        isFading = true;
        alphaVariance = (fadeOutTime * Time.deltaTime) * -1;
    }

    public void Flash()
    {
        flashCanvasGroup.alpha = 1f;
        isFlashing = true;
        AudioManager.StopMusic();
        AudioManager.PlaySound(fireSound, true);
    }
}
