using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NaC : MonoBehaviour {

    private INaC_Model model;
    private NaC_Presenter presenter;
    private IControlView main;

    void Start () {
        model = new NaC_Model();
        List<ICubeView> list = new List<ICubeView>();
        GetComponentsInChildren(false, list);
        main = GetComponentInChildren<IControlView>();
        presenter = new NaC_Presenter(list, model, main);


        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}