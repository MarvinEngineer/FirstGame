using UnityEngine;
using System.Collections;
using System;

public class GameState {

    public enum State { Run, EscMenu, GameOver, InventoryMenu, Pause };

    public State state
    {
        get;
        set;
    }

    public GameState()
    {
        state = State.Run;
    }

    public void ChangeState(State s)
    {
        state = s;
    }

}
