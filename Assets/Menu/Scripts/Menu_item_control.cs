using UnityEngine;
using System.Collections;

public class Menu_item_control : MonoBehaviour {

    protected TextMesh text;    
    private GameObject _cube;
    public GameObject cube;
    public GameObject switcher;

    // Use this for initialization
    void Start () {
        text = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnMouseEnter()
    {
        text.color = Color.red;
        _cube = Instantiate(cube, this.transform.position - new Vector3(7, 5, 0), this.transform.rotation) as GameObject;
    }

    void OnMouseOver()
    {
        RotateCube();
        if (Input.GetButtonDown("Fire1"))
            switcher.GetComponent<MenuSwitcher>().SelectMainMenu(this.name);
    }

    void OnMouseExit()
    {
        text.color = Color.white;
        Destroy(_cube);
    }

    private void RotateCube()
    {
        if (_cube != null)
            _cube.transform.Rotate((new Vector3(1, 1, 0)), 100 * Time.deltaTime);
    }
}
