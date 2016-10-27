using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class EscMenuView : MonoBehaviour {

    [SerializeField]    private GameObject main;
    [SerializeField]    private GameObject save_load;
    [SerializeField]    private GameObject help;
    [SerializeField]    private GameObject option;

    public event EventHandler<GameStateEventArgs> BackButtonClicked = delegate { };


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Main
    public void Main()
    {
        main.SetActive(true);
        save_load.SetActive(false);
        help.SetActive(false);
        option.SetActive(false);
    }

    #region Main
    //Back
    public void BackToGame()
    {
        BackButtonClicked(this, new GameStateEventArgs(MainView.State.Run));
    }
    //Сохранение/загрузка
    public void Save_load()
    {
        main.SetActive(false);
        save_load.SetActive(true);
        save_load.GetComponent<SaveLoadScript>().UpdateList();
    }
    //Help
    public void Help()
    {
        main.SetActive(false);
        help.SetActive(true);
    }
    //Option
    public void Option()
    {
        main.SetActive(false);
        option.SetActive(true);
    }
    //Exit
    public void Exit()
    {
        PlayerPrefs.SetString("NextLevel", "Menu");
        SceneManager.LoadScene("loading", LoadSceneMode.Single);
    }
    #endregion Main

    #region Save/Load menu
    //Save
    public void Save()
    {

    }
    //Load
    public void Load()
    {

    }
    #endregion Save/Load menu
}
