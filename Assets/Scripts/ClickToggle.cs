using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToggle : MonoBehaviour {

    public AIEventManager.Event targetEvent;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            gameObject.GetComponent<Toggle>().isOn = !gameObject.GetComponent<Toggle>().isOn;
    }
    public void click()
    {
        print(gameObject.name + " was clicked value = " + gameObject.GetComponent<Toggle>().isOn);
        BookMenu.instance.SetRule(gameObject.GetComponent<Toggle>().isOn, (int)targetEvent);
    }
}
