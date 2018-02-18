using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakerEmit : MonoBehaviour {

    ParticleSystem myPS;

	// Use this for initialization
	void Start () {
       
        myPS = GetComponent<ParticleSystem>();
       
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Dot(transform.parent.up, Vector3.down) > 0)
        {
            myPS.Emit(1);
        }
	}
}
