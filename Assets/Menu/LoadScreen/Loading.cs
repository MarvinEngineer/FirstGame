using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour {
    
    public Slider loadingBar;

    private GameObject g;

    private string status = "Loading...";
    private string lastStatus = null;
    private AsyncOperation async = null; // When assigned, load is in progress.

    void Start()
    {
        if (PlayerPrefs.GetString("NextLevel") != "")
            LoadNextLevel();
        else throw new PlayerPrefsException("NextLevel is absent");
    }

    public void LoadNextLevel()
    {
        async = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("NextLevel"), LoadSceneMode.Single);
        PlayerPrefs.SetString("NextLevel", "");
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
                async.allowSceneActivation = true;
                Time.timeScale = 1f;
            }
            if (lastStatus != status) { lastStatus = status; Debug.Log(status); }
            loadingBar.value = async.progress*100;
            yield return null;
        }
    }
}
