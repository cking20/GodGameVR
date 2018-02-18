using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour {
    
   
    private GameObject instanceOfFire;
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire") && instanceOfFire == null) {
            instanceOfFire = Instantiate(PrefabManager.instance.Fire, transform.position, Quaternion.identity, gameObject.transform);
        }
        if (other.CompareTag("Water") && instanceOfFire != null)
        {
            Destroy(instanceOfFire);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire") && instanceOfFire == null)
        {
            instanceOfFire = Instantiate(PrefabManager.instance.Fire, transform.position, Quaternion.identity, gameObject.transform);
        }
        if (collision.gameObject.CompareTag("Water") && instanceOfFire != null)
        {
            Destroy(instanceOfFire);
        }
    }
}
