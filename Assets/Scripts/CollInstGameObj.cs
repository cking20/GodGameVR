using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollInstGameObj : MonoBehaviour {
    public GameObject toInst;
    // Use this for initialization
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(toInst, transform.position, Quaternion.identity, collision.gameObject.transform);
    }
}
