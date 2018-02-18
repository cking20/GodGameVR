using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlanet : MonoBehaviour {

    // Use this for initialization
    //public GameObject wholePlanet;
    public bool autoBuild = false;
    bool built;

    OVRGrabbable grabbable;
    void Start()
    {
        built = false;
        grabbable = GetComponent<OVRGrabbable>();
        if (autoBuild)
            Build();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbable.isGrabbed)//opened animation
        {
            //print("open");
            if (built == false)
            {
                Build();
            }
        }
    }
    void Build() {
        GameObject p = Instantiate(PrefabManager.instance.WholePlanet, transform.position, Quaternion.identity, transform.parent);
        p.GetComponentInChildren<Planet>().terrainRadius = transform.localScale.x / .175f;
        p.GetComponentInChildren<Planet>().GeneratePlanet();
        Destroy(gameObject);
    }
}
