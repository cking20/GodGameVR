using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsFollow : MonoBehaviour {
    public Transform toFollow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (toFollow == null)
            Destroy(gameObject);
        transform.position = toFollow.position;
        transform.rotation = toFollow.rotation;
        
    }
}
