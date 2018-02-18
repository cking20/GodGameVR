using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponent : MonoBehaviour {
    public GameObject nextMenu;
    MeshDeformerInput mdi;
    ObjectPlacementInput opi;
    BuildAstroBodies bab;
    PowerInput pi;
    public int target = 0;
    void Start()
    {
        GameObject p = GameObject.Find("PlayArea");
        
        mdi = p.GetComponent<MeshDeformerInput>();
        opi = p.GetComponent<ObjectPlacementInput>();
        bab = p.GetComponent<BuildAstroBodies>();
        pi = p.GetComponent<PowerInput>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collided");
        if (collision.transform.CompareTag("Player"))
        {
            switch (target)
            {
                case 1:
                    mdi.enabled = true;
                    opi.enabled = false;
                    bab.enabled = false;
                    pi.enabled = false;
                    break;
                case 2:
                    mdi.enabled = false;
                    opi.enabled = true;
                    bab.enabled = false;
                    pi.enabled = false;
                    break;
                case 3:
                    mdi.enabled = false;
                    opi.enabled = false;
                    bab.enabled = true;
                    pi.enabled = false;
                    break;
                case 4:
                    mdi.enabled = false;
                    opi.enabled = false;
                    bab.enabled = false;
                    pi.enabled = true;
                    break;
                default:
                    mdi.enabled = false;
                    opi.enabled = false;
                    bab.enabled = false;
                    pi.enabled = false;
                    break;
            }
            transform.parent.gameObject.SetActive(false);
            if (nextMenu != null) nextMenu.SetActive(true); else transform.parent.parent.gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        print("triggered");
        if (other.CompareTag("Player")) {
            switch (target) {
                case 1:
                    mdi.enabled = true;
                    opi.enabled = false;
                    bab.enabled = false;
                    pi.enabled = false;
                    break;
                case 2:
                    mdi.enabled = false;
                    opi.enabled = true;
                    bab.enabled = false;
                    pi.enabled = false;
                    break;
                case 3:
                    mdi.enabled = false;
                    opi.enabled = false;
                    bab.enabled = true;
                    pi.enabled = false;
                    break;
                case 4:
                    mdi.enabled = false;
                    opi.enabled = false;
                    bab.enabled = false;
                    pi.enabled = true;
                    break;
                default:
                    mdi.enabled = false;
                    opi.enabled = false;
                    bab.enabled = false;
                    pi.enabled = false;
                    break;
            }
            transform.parent.gameObject.SetActive(false);
            if (nextMenu != null) nextMenu.SetActive(true); else transform.parent.parent.gameObject.SetActive( false);
        }
    }


}
