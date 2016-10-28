using UnityEngine;
using System.Collections;
using System;

public interface IScoreView
{
    event EventHandler<CoinRaisedEventArgs> CoinRaised;
    int Scorepoints { get; set; }
    void DestroyCoin();
}
