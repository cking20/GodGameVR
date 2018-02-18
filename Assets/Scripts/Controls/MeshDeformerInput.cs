using UnityEngine;
using System.Collections;

public class MeshDeformerInput : MonoBehaviour {

	public float force = 10f;
    public float radius = 10f;
	public float forceOffset = 0.1f;
	public float heightOffset = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    /*
	void Update () {
		if (Input.GetMouseButton(0)) {
            //HandleSlap();
            HandleInputLower();
		}
		if (Input.GetMouseButton(1)) {
			HandleInputRaise();
		}
	}
    */
	void HandleSlap(){
		Ray inputRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (inputRay, out hit)) {
			MeshDeformation deformer = hit.collider.GetComponent<MeshDeformation> ();
			if (deformer) {
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddDeformingForce(point, force);
			}
		}
	}

    public void HandleInput(Ray inputRay, float mult)
    {
        /*
		Ray inputRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        */
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {
            print(hit.transform.name);
            MeshDeformation deformer = hit.collider.GetComponent<MeshDeformation>();
            if (deformer)
            {
                Vector3 point = hit.point;
                point += hit.normal * forceOffset;
                deformer.AddHeight(point, hit.normal, force * mult, radius);
            }
        }
    }




    void HandleInputRaise(Ray inputRay){
        /*
		Ray inputRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        */
		RaycastHit hit;
        
		if (Physics.Raycast (inputRay, out hit)) {
			MeshDeformation deformer = hit.collider.GetComponent<MeshDeformation> ();
			if (deformer) {
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddHeight(point,hit.normal, force,radius);
			}
		}
	}

    void HandleInputLower(Ray inputRay)
    {
        //Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {
            MeshDeformation deformer = hit.collider.GetComponent<MeshDeformation>();
            if (deformer)
            {
                Vector3 point = hit.point;
                point += hit.normal * forceOffset;
                deformer.AddHeight(point, hit.normal, -force, radius);
            }
        }
    }
}
