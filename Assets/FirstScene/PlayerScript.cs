using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour {

    [SerializeField] private Camera cam;

    public event EventHandler<StartMinigameEventArgs> MinigameStarted = delegate { };

	void Start ()
    {
        cam = Camera.main;	
	}
	
	void Update () {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {
            Debug.DrawLine(ray.origin, hit.point);
            if ((Input.GetKeyUp(KeyCode.F)) && (hit.transform.gameObject.GetComponent<MiniGameSphere>()) != null)
                MinigameStarted(this, new StartMinigameEventArgs(0));
        }
    }
}
