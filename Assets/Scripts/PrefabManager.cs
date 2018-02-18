using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour {
    public static PrefabManager instance;
    public GameObject PlayerPrefab;
    public GameObject SystemPrefab;
    public GameObject NPC;
    public GameObject NPCHouse;
    public GameObject MenuSystem;
    public GameObject Fire;
    public GameObject WholePlanet;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	
}
