using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    bool isOpenEscapeMenu;
    bool isOpenInventory;

    [SerializeField] private Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {
            Debug.DrawLine(ray.origin, hit.point);
            if ((Input.GetKeyUp(KeyCode.F)) && (hit.transform.gameObject.GetComponent<MiniGameSphere>()) != null)
                Debug.Log("Work!");
        }

    }

    void FixUpdate()
    {


    }


}
