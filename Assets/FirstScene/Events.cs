using UnityEngine;
using System.Collections;
using System;


public class GameStateEventArgs : EventArgs
{
    public MainView.State GameState { get; set; }
    public GameStateEventArgs(MainView.State gameState)
    {
        GameState = gameState;
    }
}

public class StartMinigameEventArgs : EventArgs
{
    public int MinigameIndex { get; set; }
    public StartMinigameEventArgs(int index)
    {
        MinigameIndex = index;
    }
}

public class SelectMenuEventArgs : EventArgs
{
    public string buttonName { get; set; }
    public SelectMenuEventArgs(string s)
    {
        buttonName = s;
    }
}


public class SelectSaveEventArgs : EventArgs
{
    public string saveName { get; set; }
    public SelectSaveEventArgs(string s)
    {
        saveName = s;
    }
}

public class HealthChangedEventArgs : EventArgs
{
    public int difference { get; set; }
    public HealthChangedEventArgs(int dif)
    {
        difference = dif;
    }
}

public class CoinRaisedEventArgs : EventArgs
{
    public int points;
    public CoinRaisedEventArgs(int p)
    {
        points = p;
    }
}

public class PlayerInformationEventArgs : EventArgs
{
    public int scores;
    public float hp;
    public PlayerInformationEventArgs(int p, float h)
    {
        scores = p;
        hp = h;
    }
}