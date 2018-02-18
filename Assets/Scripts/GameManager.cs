using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public int galacticBelief;
    public int beliefCap = 10000;
    public int currentLevel;
    public GameObject currentSystem;
    public GameObject orbiting;
    public GameObject player;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }
    void Start () {
        if(player == null)
            player = Instantiate(PrefabManager.instance.PlayerPrefab,Vector3.zero, Quaternion.identity, null);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetCurrentSystem(GameObject s) {
        // this is where it would save old system
        if (s == null)
            print("system leaving should be null");
        currentSystem = s;
        if (s != null)
            AIManager.instance.NPCList = currentSystem.GetComponent<SystemAttributes>().NPCList;
        else
            AIManager.instance.NPCList = null;
    }
    public void SetOrbiting(GameObject g) {
        orbiting = g;
        player.GetComponent<PlayerAttributes>().orbiting = g.transform;
    }
    
}
