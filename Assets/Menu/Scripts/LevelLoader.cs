using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    GameObject SceneInfo;

    public void LoadALevel(string _name)
    {
        SceneInfo = new GameObject("ChangeObject");
        SceneInfo.AddComponent<CHANGE>();
        if (GameObject.Find("ChangeObject") != null)        
            GameObject.Find("ChangeObject").GetComponent<CHANGE>().nextSceneName = _name;
        
        SceneManager.LoadScene("loading", LoadSceneMode.Single);
    }
}

