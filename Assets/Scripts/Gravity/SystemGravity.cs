using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemGravity : MonoBehaviour {
    public float G = 100f;
    public int maxSize = 50;
    private List<Rigidbody> bodies;
    
	// Use this for initialization
	void Awake () {
        bodies = new List<Rigidbody>(maxSize);
        
	}

    public void Subscribe(Rigidbody rb) {
        bodies.Add(rb);
    }
    public void Unsubscribe(Rigidbody rb)
    {
        bodies.Remove(rb);
    }
    
    public void Attract(Rigidbody rb) {
        
        Vector3 tempDiff = new Vector3();
        for (int j = 0; j < bodies.Count; j++)
        {
            if (bodies[j] == null)
            {
                bodies.RemoveAt(j);
            }
            else if(bodies[j] != rb)
            {
                tempDiff = rb.transform.position - bodies[j].transform.position;
                rb.AddForce(-tempDiff.normalized * ((G * rb.mass * bodies[j].mass) / tempDiff.sqrMagnitude));
            }
        }
            
        
    }
}
