using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementInput : MonoBehaviour {
    public float size = 10f;
    public float radius = 10f;
    public GameObject activeCreate;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
    /*
	void Update () {
        if (Input.GetMouseButton(0))
        {
            HandleInputCreate();
        }
        if (Input.GetMouseButton(1))
        {
            HandleInputDestory();
        }
    }

    */

    public void HandleInput(Ray inputRay, float createOrDestoy)
    {
        //Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //print("cr/de" + createOrDestoy);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {
            print(hit.transform.name);
            PlanetObjectsHandler objects = hit.collider.GetComponent<PlanetObjectsHandler>();

            if (objects)
            {
                //print("has handler");
                Vector3 point = hit.point;
                point += hit.normal;
                if(createOrDestoy > 0)
                    objects.AddObject(point, hit.normal, size, radius * createOrDestoy, activeCreate);
                else
                    objects.RemoveObject(point, hit.normal, size, radius * -createOrDestoy, activeCreate);
            }
        }
    }




    void HandleInputCreate()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {
            PlanetObjectsHandler objects = hit.collider.GetComponent<PlanetObjectsHandler>();
            if (objects)
            {
                Vector3 point = hit.point;
                point += hit.normal;
                objects.AddObject(point, hit.normal, size, radius,activeCreate);
            }
        }
    }

    void HandleInputDestory()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {
            PlanetObjectsHandler objects = hit.collider.GetComponent<PlanetObjectsHandler>();
            if (objects)
            {
                Vector3 point = hit.point;
                point += hit.normal;
                objects.RemoveObject(point, hit.normal, size, radius, activeCreate);
            }
        }
    }
}
