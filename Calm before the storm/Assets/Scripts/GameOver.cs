using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Health playerHealthComponent;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        playerHealthComponent.OnHealthReachZero += PlayerHealthComponent_OnHealthReachZero;
    }

    private void Update()
    {
        if (gameOverCanvasGroup.interactable == true && gameOverCanvasGroup.alpha < 1)
        {
            gameOverCanvasGroup.alpha += Time.deltaTime;
        }
    }

    private void PlayerHealthComponent_OnHealthReachZero(object sender, System.EventArgs e)
    {
        gameOverCanvasGroup.interactable = true;
        scoreText.text = ScoreManager.instance.GetScore().ToString("0000000000");
    }
}
