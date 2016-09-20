using UnityEngine;
using System.Collections;
using System;

public class NaC_ControlView : MonoBehaviour, IControlView {
    public event EventHandler ResetClicked = delegate { };

    // Use this for initialization
    void Start ()
    {
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reset()
    {
        Component[] components = GetComponentsInChildren(typeof(CubeView));

        if (components != null)
        {
            foreach (CubeView cube in components)
                cube.Reset();
        }
        ResetClicked(this, new EventArgs());
    }

}


