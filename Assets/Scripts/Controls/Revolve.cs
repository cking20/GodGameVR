using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour {
    public Transform around;
    public float speed = 1f;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
        if(around != null)
            transform.RotateAround(around.position,new Vector3(0f, 1f, 0f), speed * Time.deltaTime);
	}
}
