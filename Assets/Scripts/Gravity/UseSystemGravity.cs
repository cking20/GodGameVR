using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class UseSystemGravity : MonoBehaviour {
    public SystemGravity subcribedTo;
    private Rigidbody myRB;
	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();
        myRB.useGravity = false;
        if (GameManager.instance.currentSystem != null)
        {
            subcribedTo = GameManager.instance.currentSystem.GetComponent<SystemGravity>();
            subcribedTo.Subscribe(myRB);
        }
        InvokeRepeating("Grav", 0f, 0.5f);
    }
	
	// Update is called once per frame
	private void  Grav() {
        if(subcribedTo == null)
        {
            if (GameManager.instance.currentSystem != null)
            {
                subcribedTo = GameManager.instance.currentSystem.GetComponent<SystemGravity>();
                subcribedTo.Subscribe(myRB);
            }
            return;
        }
        subcribedTo.Attract(myRB);
	}

    private void OnDestroy()
    {
        if(subcribedTo != null)
            subcribedTo.Unsubscribe(myRB);
    }
}
