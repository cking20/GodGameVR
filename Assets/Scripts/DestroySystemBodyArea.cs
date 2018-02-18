using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySystemBodyArea : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AstroBody")) {
            Destroy(other.gameObject);
        }
    }
}
