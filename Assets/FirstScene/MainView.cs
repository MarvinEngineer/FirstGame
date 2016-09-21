using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class MainView : MonoBehaviour, IMainView {

    [SerializeField]    private GameObject Player;
    [SerializeField]    private GameObject PlayerController;
    [SerializeField]    private GameObject EscMenu;
    [SerializeField]    private List<GameObject> MiniGames;

    public enum State { Run, EscMenu, InventoryMenu, Pause };

    private bool isGameOver;

    private State currentState;
    public State CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = value;
            GameStateUpdated(this, new GameStateEventArgs(currentState));
        }
    }

    void Start()
    {
        currentState = State.Run;
        isGameOver = false;
        Player.GetComponentInChildren<PlayerScript>().MinigameStarted += StartMinigame;
    }

    void Update()
    {

    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (currentState == State.Run)
                currentState = State.EscMenu;
            if ((currentState == State.EscMenu) || (currentState == State.InventoryMenu))
                currentState = State.Run;
        }

        if (Input.GetKey(KeyCode.I))
        {
            if (currentState == State.Run)
                currentState = State.InventoryMenu;
            if (currentState == State.InventoryMenu)
                currentState = State.Run;
        }
    }

    private void PlayerDead()
    {
        isGameOver = true;
    }

    public event EventHandler<GameStateEventArgs> GameStateUpdated = delegate { };

    private void StartMinigame (object sender, StartMinigameEventArgs e)
    {
        PlayerController.SetActive(false);
        MiniGames[e.MinigameIndex].SetActive(true);
    }
}


#region EventArgs

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


#endregion EventArgs


public interface IMainView
{
    event EventHandler<GameStateEventArgs> GameStateUpdated;
}