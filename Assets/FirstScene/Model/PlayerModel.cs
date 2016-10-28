using UnityEngine;
using System.Collections;
using System;

public class PlayerModel : IPlayerModel {

    private float health;
    private float maxHealth = 100;
    private int score;
    private bool isDead;

    public PlayerModel()
    {
        Health = 100;
        Score = 0;
        isDead = false;
    }

    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            if (health > maxHealth)
                health = maxHealth;
            else if (health <= 0)
            {
                health = 0;
                IsDead = true;
                PlayerDead(this, new EventArgs());
            }
            playerUpdated(this, new PlayerInformationEventArgs(score, health));
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            playerUpdated(this, new PlayerInformationEventArgs(score, health));
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }

        set
        {
            isDead = value;
        }
    }

    public void Resurrect()
    {
        if (IsDead)
        {
            IsDead = false;
            health = 100;
        }
    }

    public void Resurrect(float hp)
    {
        if (IsDead)
        {
            IsDead = false;
            health = hp;
        }
    }

    public void Kill()
    {
        if(!IsDead)
        {
            health = 0;
        }
    }

    event EventHandler PlayerDead = delegate { };

    public event EventHandler<PlayerInformationEventArgs> playerUpdated = delegate { };
}
