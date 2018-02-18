using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float lg = (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger));
        float rg = (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger));
        float lt = (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger));
        float rt = (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));

        print(lg+rg+lt+rt);
    }
}
