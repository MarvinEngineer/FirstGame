using UnityEngine;
using System.Collections;
using System;

public class ScoreView : MonoBehaviour, IScoreView  {

    private int scorepoints;

    public int Scorepoints
    {
        get
        {
            return scorepoints;
        }

        set
        {
            scorepoints = value;
        }
    }

    public event EventHandler<CoinRaisedEventArgs> CoinRaised;

    void Start()
    {
        Scorepoints = 20;
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
        {
            CoinRaised(this, new CoinRaisedEventArgs(Scorepoints));
            Destroy(gameObject);
        }
    }
}
