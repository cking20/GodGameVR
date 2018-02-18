using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBounds : MonoBehaviour {

    // Use this for initialization
    
	void Start () {
        
	}

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AstroBody") || other.CompareTag("NPC"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Player"))
        {
            print("player leaving");
            GameManager.instance.SetCurrentSystem(null);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("player coming");
            GameManager.instance.SetCurrentSystem(gameObject);

        }
    }
}
