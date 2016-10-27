using UnityEngine;
using System.Collections;
using System;

public class ChangeColor : MonoBehaviour {

    [SerializeField] private UILabel label;
    [SerializeField]    private SaveLoadScript sl_script;
    [SerializeField]    private UIToggle toggel;
    private new string name;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    void Start()
    {
        label = GetComponentInChildren<UILabel>();
        toggel = GetComponent<UIToggle>();
    }

    public void Change()
    {
        if(toggel!=null)
            label.color =  toggel.isChecked ?  Color.white : Color.black;
        if (toggel.isChecked)
        {
            Selected(this,new SelectSaveEventArgs(label.text));
        }
    }

    public void ChangeName(string s)
    {
        label.text = s;
        Name = s;
    }

    public event EventHandler<SelectSaveEventArgs> Selected = delegate { };
}


