using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveLoadScript : MonoBehaviour {

    [SerializeField]    private Camera camera;
    [SerializeField]    private GameObject saveList;
    [SerializeField]    private GameObject savePrefab;

    [SerializeField]
    private UITexture screentex;
    [SerializeField]
    private UILabel savedate;



    private int lastindex;
    private byte[] scr;
    private string dirSave;

    private Transform t_saveList;

    public List<string> list;

    void Awake()
    {
        dirSave = Application.dataPath + "\\Save";
        t_saveList = saveList.transform;
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Save()
    {
        StartCoroutine(CaptureScreen());

        string _n = "save" + (lastindex + 1).ToString("D4");
        Save save = new Save(scr,DateTime.Now,_n);
        lastindex++;


        string pathSave = dirSave + "\\" + save.name + ".dat";
        if (!Directory.Exists(dirSave))
        {
            Directory.CreateDirectory(dirSave);
        }
        if (!File.Exists(pathSave))
        {
            FileStream fs = new FileStream(pathSave, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, save);
            }
            catch (SerializationException e)
            {
                Debug.Log("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }

    private void ReadDirectory()
    {
        list = new List<string>();
        DirectoryInfo savedir = new DirectoryInfo(dirSave);
        foreach (var savefile in savedir.GetFiles())
        {
            if (savefile.Extension==".dat")
            {
                list.Add(savefile.Name);
            }
        }
    }

    public void ReadSave(string name)
    {

    }

    public void UpdateList()
    {
        GameObject tmp;
        ReadDirectory();

        foreach (Transform child in t_saveList)
        {
            Destroy(child.gameObject);
        }

        foreach (string s in list)
        {
            tmp = Instantiate(savePrefab,new Vector3(0,0,0), Quaternion.identity, t_saveList) as GameObject;
            tmp.GetComponent<ChangeColor>().ChangeName(s);
        }
        NGUITools.ImmediatelyCreateDrawCalls(saveList);
        saveList.GetComponent<UITable>().Reposition();
        NGUITools.ImmediatelyCreateDrawCalls(saveList);

        Component[] components = saveList.GetComponentsInChildren<ChangeColor>();
        foreach (ChangeColor ch in components)
            ch.Selected += saveMenuItemSelected;
    }

    private void saveMenuItemSelected(object sender, SelectSaveEventArgs e)
    {
        if (File.Exists(dirSave+"\\"+ e.saveName))
        {
            FileStream fs = new FileStream(dirSave + "\\" + e.saveName, FileMode.Open);

            Save tmp;
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                tmp = (Save)formatter.Deserialize(fs);
            }
            catch (SerializationException ex)
            {
                Debug.Log("Failed to deserialize. Reason: " + ex.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            Texture2D tmptex = new Texture2D(320,180);
            tmptex.LoadImage(tmp.screenshot);
            screentex.mainTexture = tmptex;
            savedate.text = tmp.name + "\n" + tmp.Time;
        }
    }

    public void Load()
    {

    }

    public IEnumerator CaptureScreen()
    {

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();
        Texture2D screenshot;
        screenshot = new Texture2D(1000, 700, TextureFormat.RGB24, false);
        
        RenderTexture rt = new RenderTexture(1000, 700, 24);
        camera.targetTexture = rt;
        camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, 1000, 700), 0, 0);
        screenshot.Apply();

        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        scr = screenshot.EncodeToPNG();
    }


    #region RightPanel

    // Заргузка данных сейва в правую панель
    void ReloadRightPannel()
    {
        // Обновление скриншота
        // Обновление даты, времени, названия
    }


    #endregion RightPanel

}



public struct HeaderFile
{
    public string name { get; set; }
    public List<HeaderFileItem> list { get; set; }

    public HeaderFile(string s, List<HeaderFileItem> l)
    {
        name = s;
        list = l;
    }

    public HeaderFile(string s)
    {
        name = s;
        list = new List<HeaderFileItem>();
    }
}

public struct HeaderFileItem
{
    public int index { get; set; }
    public string name { get; set; }
    public Texture2D screen { get; set; }
    public DateTime date { get; set; }
    public string saveFileName { get; set; }

    public HeaderFileItem(int i, string n, Texture2D t, DateTime d, string sn)
    {
        index = i;
        name = n;
        screen = t;
        date = d;
        saveFileName = sn;
    }
}
[Serializable]
public struct Save
{
    public byte[] screenshot;
    public DateTime Time;
    public string name;

    public Save(byte[] s, DateTime t, string n)
    {
        Time = t;
        screenshot = s;
        name = n;
    }
}

