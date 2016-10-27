using UnityEngine;
using System.Collections;
using System;

public class MainModel : MonoBehaviour, IMainModel
{
    private enum GameProgress { New, _1stMinigameWin, _2ndMinigameWin, GameOver };

    private Player player;

    public event EventHandler PlayerDead;

    public MainModel()
    {
        player = new Player();
    }

    public void UpdateModel()
    {
        throw new NotImplementedException();
    }
}


    
public interface IMainModel
{
    event EventHandler PlayerDead;

    void UpdateModel();
}

public class Player
{
    private int score;

    private float health;
    private bool isDead;
    public float Health
    {
        get { return health; }
        set
        {
            if ((health - value) <= 0)
            {
                health = 0;
                isDead = true;
            }
            else health -= value;
        } 
    }

    public Player()
    {
        score = 0;
        health = 100;
        isDead = false;
    }

    public void Resurrect(float h)
    {
        isDead = false;
        health = h;
    }
    
}


