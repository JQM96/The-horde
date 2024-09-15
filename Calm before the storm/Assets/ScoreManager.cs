using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private int decayTime;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;

    private int score;
    private int scoreMultiplier = 1;

    private float multiplierDecay;
    private float decayTimer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        multiplierText.text = "x" + scoreMultiplier.ToString("00");
    }

    private void Update()
    {
        if (scoreMultiplier > 1)
        {
            decayTimer += Time.deltaTime;
            multiplierDecay = decayTime / scoreMultiplier;

            if (decayTimer >= multiplierDecay)
            {
                scoreMultiplier -= 1;
                decayTimer = 0;

                multiplierText.text = "x" + scoreMultiplier.ToString("00");
            }
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd * scoreMultiplier;

        scoreText.text = "Score: " + score.ToString("0000000000");
    }

    public void AddMultiplier(int multiplierToAdd)
    {
        scoreMultiplier += multiplierToAdd;

        multiplierText.text = "x" + scoreMultiplier.ToString("00");
    }

    public int GetScore()
    {
        return score;
    }
}
