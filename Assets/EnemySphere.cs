using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySphere : MonoBehaviour {

    public float speed;

    public List<Vector3> patrolPoints = new List<Vector3>();
    private int currentDestinationPoint;


	// Use this for initialization
	void Start () {
        currentDestinationPoint = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (patrolPoints.Count == 0) patrolPoints.Add(transform.position);
        Patrol();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player") Debug.Log("Game over");
    }

    void Patrol()
    {        
        if (DestinationReached(patrolPoints[currentDestinationPoint]))
        {
            if (currentDestinationPoint != patrolPoints.Count-1) currentDestinationPoint++;
            else currentDestinationPoint = 0;            
        }
        transform.LookAt(patrolPoints[currentDestinationPoint]);
        if ((patrolPoints[currentDestinationPoint]-transform.position).magnitude < (Vector3.forward * Time.deltaTime * speed).magnitude) transform.position = patrolPoints[currentDestinationPoint];
        else transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private bool DestinationReached(Vector3 v)
    {
        if (transform.position == v) return true;
        else return false;
    }
}
