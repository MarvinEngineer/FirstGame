using System;
using UnityEngine;

public class Menu_item_control : MonoBehaviour {

    private GameObject _cube;
    public GameObject cube;

    private bool isSelected;

    public event EventHandler<ButtonEventArgs> selected = delegate { };
    public event EventHandler<ButtonEventArgs> deselected = delegate { };

    public event EventHandler<ButtonEventArgs> released = delegate { };

    // Use this for initialization
    void Start () {
        isSelected = false;
    }

    void Update()
    {
        if (isSelected)
        {
            RotateCube();
            if (Input.GetButton("Submit"))
                released(this, new ButtonEventArgs(name)); 
        }
    }

    void OnMouseEnter()
    {
        Select();
        selected(this, new ButtonEventArgs(name));
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
            released(this, new ButtonEventArgs(name));
    }

    void OnMouseExit()
    {
        Deselect();
        deselected(this, new ButtonEventArgs(name));
    }

    private void RotateCube()
    {
        if (_cube != null)
            _cube.transform.Rotate((new Vector3(1, 1, 0)), 100 * Time.deltaTime);
    }

    private void Select()
    {        
        GetComponent<TextMesh>().color = Color.red;
        _cube = Instantiate(cube, this.transform.position - new Vector3(7, 5, 0), this.transform.rotation) as GameObject;
        isSelected = true;
    }

    public void UpdateSelect(object sender, ButtonEventArgs e)
    {
        if (e.Name == name) Select();
        else if (isSelected) Deselect();
    }

    private void Deselect()
    {
        GetComponent<TextMesh>().color = Color.white;
        if (_cube != null) Destroy(_cube);
        isSelected = false;
    }
}

public class ButtonEventArgs: EventArgs
{
    public string Name;
    public ButtonEventArgs(string name) { Name = name; }
}
