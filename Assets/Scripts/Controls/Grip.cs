using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour {
    public OVRInput.Controller controller;
    public string buttonName;
    public float throwMult;
    public float grabRadius;
    public LayerMask grabLayer;
    private GameObject grabbed;
    private bool grabbing;
    void GrabObj()
    {
        grabbing = true;
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabLayer);
        if (hits.Length > 0)
        {
            int closest = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                print(hits[i].transform.name);
                if (hits[i].distance < hits[closest].distance && !hits[closest].transform.CompareTag("Player")) closest = i;
            }
            print("Closest: " + hits[closest].transform.name);
            if (!hits[closest].transform.CompareTag("Player"))
            {
                grabbed = hits[closest].transform.gameObject;
                grabbed.GetComponent<Rigidbody>().isKinematic = true;
                grabbed.transform.position = transform.position;
                grabbed.transform.parent = transform;
            }
        }
    }
    void DropObj()
    {
        grabbing = false;
        if (grabbed != null)
        {
            grabbed.transform.parent = null;
            Rigidbody rb = grabbed.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = OVRInput.GetLocalControllerVelocity(controller).magnitude * throwMult * transform.forward;
            rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);

            grabbed = null;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (!grabbing && Input.GetAxis(buttonName) == 1) GrabObj();
        if (grabbing && Input.GetAxis(buttonName) < 1) DropObj();
    }
}