using UnityEngine;
using System.Collections;
using System;


public class MainView : MonoBehaviour, IMainView {

    public enum State { Run, EscMenu, InventoryMenu, Pause };

    private bool isGameOver;

    private State currentState
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

#endregion EventArgs


public interface IMainView
{
    event EventHandler<GameStateEventArgs> GameStateUpdated;
}