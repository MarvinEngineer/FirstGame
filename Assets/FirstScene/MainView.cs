using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class MainView : MonoBehaviour, IMainView {

    [SerializeField]    private GameObject Player;
    [SerializeField]    private GameObject PlayerController;
    [SerializeField]    private GameObject EscMenu;
    [SerializeField]    private List<GameObject> MiniGames;

    public enum State { Run, EscMenu, InventoryMenu, Pause, Minigame };

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
        CurrentState = State.Run;
        isGameOver = false;
        Player.GetComponentInChildren<PlayerScript>().MinigameStarted += StartMinigame;
        GameStateUpdated += SwitchInterface;
    }

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (CurrentState == State.Run)
            {
                CurrentState = State.EscMenu;
                return;
            }
            if ((CurrentState == State.EscMenu) || (CurrentState == State.InventoryMenu))
            {
                CurrentState = State.Run;
                return;
            }
            Debug.Log("Esc");
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            if (CurrentState == State.Run)
            {
                CurrentState = State.InventoryMenu;
                return;
            }
            if (CurrentState == State.InventoryMenu)
            {
                CurrentState = State.Run;
                return;
            }
        }
    }

    private void PlayerDead()
    {
        isGameOver = true;
    }

    public event EventHandler<GameStateEventArgs> GameStateUpdated = delegate { };

    private void SwitchInterface (object sender, GameStateEventArgs e)
    {
        switch(e.GameState)
        {
            case State.Run:
                {
                    Player.SetActive(true);
                    PlayerController.SetActive(true);
                    PlayerController.GetComponent<FirstPersonController>().enabled = true;

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1f;

                    EscMenu.SetActive(false);
                    foreach (GameObject g in MiniGames)
                        g.SetActive(false);
                    break;
                }
            case State.EscMenu:
                {
                    Player.SetActive(true);
                    PlayerController.SetActive(true);
                    PlayerController.GetComponent<FirstPersonController>().enabled = false;

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0f;

                    EscMenu.SetActive(true);
                    foreach (GameObject g in MiniGames)
                        g.SetActive(false);
                    break;
                }
            case State.InventoryMenu:
                {
                    Player.SetActive(true);
                    PlayerController.SetActive(true);
                    PlayerController.GetComponent<FirstPersonController>().enabled = false;

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0f;

                    EscMenu.SetActive(false);
                    foreach (GameObject g in MiniGames)
                        g.SetActive(false);
                    break;
                }
        }
    }

    private void StartMinigame (object sender, StartMinigameEventArgs e)
    {
        PlayerController.SetActive(false);
        MiniGames[e.MinigameIndex].SetActive(true);
        CurrentState = State.Minigame;
    }
}

public interface IMainView
{
    event EventHandler<GameStateEventArgs> GameStateUpdated;
}