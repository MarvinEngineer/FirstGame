using UnityEngine;
using System.Collections;

public class PlayerIndicatorsView : MonoBehaviour, IPlayerIndicatorsView {

    [SerializeField]    private UILabel healthLabel;
    [SerializeField]    private UILabel scoreLabel;

    private int currentScore;
    private float currentHealth;

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }

        set
        {
            currentScore = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }

    public void UpdateIndicators(int score, float health)
    {
        CurrentHealth = health;
        CurrentScore = score;
        healthLabel.text = "Health: " + ((int)health).ToString();
        scoreLabel.text = "Score: " + score.ToString();
    }
}
