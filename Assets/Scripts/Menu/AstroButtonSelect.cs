﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroButtonSelect : MonoBehaviour {
    public GameObject myPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            GameObject.Find("PlayArea").GetComponent<BuildAstroBodies>().selectedPrefab = myPrefab;
            //transform.parent.gameObject.SetActive(false);
            //transform.parent.parent.GetChild(0).gameObject.SetActive(true);
            Destroy(transform.parent.parent.gameObject);//.SetActive(false);
        }
    }
}
