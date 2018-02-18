using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickButton : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {      
       if(other.CompareTag("Player"))
            gameObject.GetComponent<Button>().onClick.Invoke();
    }
    public void click() {
        print(gameObject.name+" was clicked");
    }
}
