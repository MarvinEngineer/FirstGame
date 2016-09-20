using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    enum State { Run, EscMenu, GameOver, InventoryMenu };

    private State currentState;

    [SerializeField]    private GameObject control1;
    [SerializeField]    private GameObject control2;
    [SerializeField]    private GameObject control3;


    


    // Use this for initialization
    void Start () {

        currentState = State.Run;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void ChangeState(string _newState)
    {
        switch(_newState)
        {
            case "run":
                {
                    currentState = State.Run;
                    break;
                }
            case "gameover":
                {
                    currentState = State.GameOver;
                    break;
                }
            case "esc":
                {
                    currentState = State.EscMenu;
                    break;
                }
            case "inventory":
                {
                    currentState = State.InventoryMenu;
                    break;
                }
        }
    }


}
