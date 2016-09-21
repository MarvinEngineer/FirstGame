using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    public string scene_name;
    public Slider loadingBar;

    private GameObject g;

    private string status = "Loading...";
    private string lastStatus = null;
    private AsyncOperation async = null; // When assigned, load is in progress.

    void Start()
    {
        if (GameObject.Find("ChangeObject") != null) LoadALevel(GameObject.Find("ChangeObject").GetComponent<CHANGE>().nextSceneName);
        else Debug.Log("fail load");
    }

    public void LoadALevel(string levelName)
    {
        async = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        Destroy(GameObject.Find("ChangeObject"));
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
            { status = "Loading:" + (async.progress * 100f).ToString("F0") + "%"; }
            else
            {
                Time.timeScale = 0f;
                if (Input.anyKeyDown)
                {
                    async.allowSceneActivation = true;
                    Time.timeScale = 1f;
                }
                status = "Press any key";
            }
            if (lastStatus != status) { lastStatus = status; Debug.Log(status); }
            loadingBar.value = async.progress*100;
            yield return null;
        }
        status = "Load complete";
        Debug.Log(status);
    }
}
