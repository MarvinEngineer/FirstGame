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