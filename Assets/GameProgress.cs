using UnityEngine;
using System.Collections;

public class GameProgress : MonoBehaviour {

    private bool isWinNac;



    // Use this for initialization
    void Start () {

        isWinNac = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Win_NaC()
    {
        isWinNac = true;
    }

}
