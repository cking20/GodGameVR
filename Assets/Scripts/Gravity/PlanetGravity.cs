using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {

	public float gravity = -100;
    public float myMass = 200;

    //should run less than at update rate
    public void Attract(Transform body, float mass){

		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(-gravityUp * gravity); //(-gravityUp * ((gravity * mass * myMass)/(body.position-transform.position).sqrMagnitude));
		Quaternion targetRot = Quaternion.FromToRotation (bodyUp, gravityUp) * body.rotation;
		body.rotation = Quaternion.Slerp (body.rotation, targetRot, 100 * Time.deltaTime);
	}
    
    void OnTriggerEnter(Collider other)
    {
        UsePlanetGravity upg = other.GetComponent<UsePlanetGravity>();
        if (upg != null)
        {
            upg.pg = this;
        }
    }
    void OnTriggerExit(Collider other)
    {
        UsePlanetGravity upg = other.GetComponent<UsePlanetGravity>();
        if (upg != null)
        {
            upg.pg = null;
        }
    }
    
}
