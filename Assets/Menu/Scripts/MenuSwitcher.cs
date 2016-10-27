using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class MenuSwitcher : MonoBehaviour {

    private float timer = 1f;

    private List<MItem> list = new List<MItem>();

    private MItem currentSelected;

    public event EventHandler<ButtonEventArgs> SelectedChange = delegate { };

    private void ReleaseMenuItem (object sender, ButtonEventArgs e)
    {
        switch(e.Name)
        {
            case "exit":
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif

                    break;
                }
            case "new":
                {
                    PlayerPrefs.SetString("NextLevel", "newScene");
                    SceneManager.LoadScene("loading", LoadSceneMode.Single);
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

    private void SelectMenuItem(object sender, ButtonEventArgs e)
    {
        foreach (MItem item in list)
            if (item.name == e.Name)
            {
                item.isActive = true;
                currentSelected = item;
            }
    }

    private void DeselectedItem(object sender, ButtonEventArgs e)
    {
        foreach (MItem item in list)
            if (item.name == e.Name) item.isActive = false;
        currentSelected = null;
    }

    private void ChangeSelect(float input)
    {
        if (currentSelected != null)
        {
            list[currentSelected.index - 1].isActive = false;
            currentSelected.isActive = false;
            if (input < 0)
            {
                if (currentSelected.index != list.Count) currentSelected = list[currentSelected.index];
                else return;
            }

            if (input > 0)
            {
                if (currentSelected.index != 1) currentSelected = list[currentSelected.index-2];
                else return;
            }
        }
        else currentSelected = list[0];

        list[currentSelected.index-1].isActive = true;
        SelectedChange(this, new ButtonEventArgs(currentSelected.name));
    }

    void Start()
    {       
        Menu_item_control[] array = GetComponentsInChildren<Menu_item_control>();

        int i = 1;
        foreach (Menu_item_control c in array)
        {
            c.released += ReleaseMenuItem;
            c.selected += SelectMenuItem;
            c.deselected += DeselectedItem;
            SelectedChange += c.UpdateSelect;

            list.Add(new MItem(i, c.name, false));
            i++;
        }
    }

    void Update()
    {
        if (timer<1f)
            timer+=Time.deltaTime;

        if ((Input.GetAxisRaw("Vertical") != 0)&&(timer>=0.25f))
        {
            ChangeSelect(Input.GetAxisRaw("Vertical"));
            timer = 0;
        }
    }
}

public class MItem
{
    public int index { get; set; }
    public string name { get; set; }
    public bool isActive { get; set; }

    private MItem()
    {

    }

    public MItem(int i, string s, bool a)
    {
        index = i;
        name = s;
        isActive = a;
    }
}


