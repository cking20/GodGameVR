using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    public GameObject prefab;
    public bool grow = false;
    public int maxCount = 20;
    private List<GameObject> list;
	// Use this for initialization
	void Start () {
        list = new List<GameObject>();
        for (int i = 0; i < maxCount; i++) {
            list.Add((GameObject)Instantiate(prefab));
        }
	}

    public GameObject GetPooledObject() {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeInHierarchy)
                return list[i];      
        }
        if (grow)
        {
            GameObject g = (GameObject)Instantiate(prefab);
            list.Add(g);
            return g;
        }
        else return null;
    }
}
