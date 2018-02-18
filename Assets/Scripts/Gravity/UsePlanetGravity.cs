using UnityEngine;
using System.Collections;

public class UsePlanetGravity : MonoBehaviour {
	public PlanetGravity pg; 
	private Transform myTransform;
    private float myMass;
	// Use this for initialization
	void Start () {
		//gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
        myMass = gameObject.GetComponent<Rigidbody>().mass;
        myTransform = transform;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(pg != null)
		    pg.Attract (myTransform,myMass);
	}
}
