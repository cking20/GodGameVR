using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {

    //public static PlayerAttributes instance;
	public Transform orbiting;
	public GameObject selected;
    public float speed = .5f;

    /*void Awake()
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
    }*/
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
