using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class MenuSwitcher : MonoBehaviour {

    private Loader loader = new Loader();

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

public class Loader : MonoBehaviour
{

    public void LoadALevel(string _name)
    {
        if (GameObject.Find("ChangeObject") != null)
        {
            GameObject.Find("ChangeObject").GetComponent<CHANGE>().nextSceneName = _name;
            GameObject.Find("ChangeObject").GetComponent<CHANGE>().nextSceneNumber = SceneManager.GetSceneByName(_name).buildIndex;
        }        
        SceneManager.LoadScene("loading", LoadSceneMode.Single);
    }

    public void LoadALevel(int _number)
    {
        if (GameObject.Find("ChangeObject") != null)
        {
            GameObject.Find("ChangeObject").GetComponent<CHANGE>().nextSceneNumber = _number;
            GameObject.Find("ChangeObject").GetComponent<CHANGE>().nextSceneName = SceneManager.GetSceneAt(_number).name;
        }
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
