using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitVelocity : MonoBehaviour {
    public GameObject orbitObj;
    Rigidbody orbit;
    Rigidbody myRb;
    SystemGravity theSystem;
    float G;
    float speed;
    // Use this for initialization
    void Start () {
        orbit = orbitObj.GetComponent<Rigidbody>();
        myRb = GetComponent<Rigidbody>();
        //GetComponent<UseSystemGravity>().enabled = false;
        theSystem = GameObject.FindGameObjectWithTag("System").GetComponent<SystemGravity>();
        //theSystem.Unsubscribe(myRb);
        G = theSystem.G;
        Vector3 diff = (transform.position - orbit.position);
        float R = diff.magnitude;
        print("R " + R);
        speed = Mathf.Sqrt((theSystem.G * orbit.mass) / R);
        print("speed " + speed);
        transform.LookAt(orbit.transform);
        myRb.velocity = (transform.right * speed);
       
    }

    void FixedUpdate()
    {
        if (orbit != null) { 
        G = theSystem.G;
        Vector3 tempDiff = myRb.position - orbit.position;
        //print("radius "+tempDiff.magnitude);
        //print("G " + G);
        float f = ((G * myRb.mass * orbit.mass) / tempDiff.sqrMagnitude);
        //print("Force due to grav " + f);
        myRb.AddForce(-tempDiff.normalized * f);
    }
    }

}
