using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFocusLine : MonoBehaviour {
    
    
    private LineRenderer LineDrawer;
    
    // Use this for initialization
    void Start()
    {
        LineDrawer = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.orbiting != null) {
            LineDrawer.SetPosition(0, transform.position);
            LineDrawer.SetPosition(1, GameManager.instance.orbiting.transform.position);
        }
        //LineDrawer.SetPosition(2, new Vector3(GameManager.instance.currentSystem.transform.position.x, floor, GameManager.instance.currentSystem.transform.position.z));
    }
}
