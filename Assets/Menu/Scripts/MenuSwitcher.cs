using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class MenuSwitcher : MonoBehaviour {

    private LevelLoader loader = new LevelLoader();

    public void SelectMainMenu (string _c)
    {
        switch(_c)
        {
            case "exit":
                {
                    Application.Quit();
                    break;
                }
            case "new":
                {
                    loader.LoadALevel("newScene");
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
        }       
    }    
}

