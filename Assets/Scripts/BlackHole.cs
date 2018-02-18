using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {


    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            if (other.gameObject.transform.localScale.x > 0)
                other.gameObject.transform.localScale -= new Vector3(.1f, .1f, .1f);
            else
                Destroy(other.gameObject);
    }
}
