using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleUI : MonoBehaviour {

	

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb;
        if (other.CompareTag("UI")) {
            rb = other.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;
        }
    }
}
