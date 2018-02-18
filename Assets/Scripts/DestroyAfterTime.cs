using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    public float lifeTime = 10f;
    public float time;
	// Use this for initialization
	void Start () {
        time = Time.time;
        time += lifeTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > time)
            Destroy(gameObject);
	}
}
