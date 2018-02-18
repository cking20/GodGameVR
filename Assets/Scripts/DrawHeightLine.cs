using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHeightLine : MonoBehaviour {
    public float floor = -10f;
    private LineRenderer LineDrawer;
    GameObject systemIn;
    // Use this for initialization
    void Start () {
        LineDrawer = GetComponent<LineRenderer>();
        systemIn = GameManager.instance.currentSystem;
    }
	
	// Update is called once per frame
	void Update () {
        LineDrawer.SetPosition(0,transform.position);
        LineDrawer.SetPosition(1, new Vector3(transform.position.x, floor, transform.position.z));
        LineDrawer.SetPosition(2, new Vector3(transform.parent.parent.position.x, floor, transform.parent.parent.position.z));
    }
}
