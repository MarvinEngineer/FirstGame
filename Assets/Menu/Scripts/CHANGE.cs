using UnityEngine;
using System.Collections;

public class CHANGE : MonoBehaviour {

    public string nextSceneName;
    public int nextSceneNumber;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        nextSceneName = null;
        nextSceneNumber = 0;
    }
}
