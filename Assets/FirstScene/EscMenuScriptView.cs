using UnityEngine;
using System.Collections;
using System;

public class EscMenuScriptView : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Switcher(string s)
    {
        switch(s)
        {
            case "back":
                {
                    break;
                }
            case "save":
                {
                    break;
                }
            case "load":
                {
                    break;
                }
            case "option":
                {
                    break;
                }
            case "exit":
                {
                    gameObject.AddComponent<LevelLoader>();
                    GetComponent<LevelLoader>().LoadALevel("Menu");
                    break;
                }
        }
        menuButtonSelected(this, new SelectMenuEventArgs(s));
    }

    public event EventHandler<SelectMenuEventArgs> menuButtonSelected = delegate { };

}
