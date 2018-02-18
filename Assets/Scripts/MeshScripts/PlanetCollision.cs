using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollision : MonoBehaviour {
    public float destructabiltily = 5;
    public float radius = 40;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.transform.CompareTag("AstroBody")) {
                GetComponent<MeshDeformation>().AddHeight(contact.point, contact.normal, 
                    -collision.relativeVelocity.sqrMagnitude*destructabiltily, 
                    contact.otherCollider.transform.localScale.magnitude* 2 * radius);

                //blow up objects in raduis

                Destroy(contact.otherCollider.gameObject);
            }
            if (contact.otherCollider.transform.CompareTag("Player"))
            {
                GetComponent<MeshDeformation>().AddDeformingForce(contact.point,
                    -collision.relativeVelocity.sqrMagnitude * destructabiltily);

                
            }
        }
        
    }
}
