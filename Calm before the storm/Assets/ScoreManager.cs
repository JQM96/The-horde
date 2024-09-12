using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private int decayTime;

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
    }

    private void Update()
    {
        Debug.Log(score + " / " + scoreMultiplier);

        if (scoreMultiplier > 1)
        {
            decayTimer += Time.deltaTime;
            multiplierDecay = decayTime / scoreMultiplier;

            if (decayTimer >= multiplierDecay)
            {
                scoreMultiplier -= 1;
                decayTimer = 0;
            }
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd * scoreMultiplier;
    }

    public void AddMultiplier(int multiplierToAdd)
    {
        scoreMultiplier += multiplierToAdd;
    }
}
