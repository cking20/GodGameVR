using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAstroBodies : MonoBehaviour {

    // Use this for initialization
    
    public GameObject selectedPrefab;
    public  GameObject current;
    private float massToScaleRatio = 1f;
    public  bool building = false;
    //GameManager gm;
    void Start () {
        //gm = GameObject.Find("Manager").GetComponent<GameManager>();
	}
	
	
    public void StartBuilding(Transform t) {
        if (GameManager.instance.currentSystem == null)
            GameManager.instance.currentSystem = Instantiate(PrefabManager.instance.SystemPrefab, GameManager.instance.player.transform.position, Quaternion.identity, GameManager.instance.currentSystem.transform);
        if (!building && current == null)
        {
            current = instGO(selectedPrefab,t);
            if (current != null)
            {
                current.SetActive(true);
                building = true;
            }
        }
    }
    public void KeepBuilding(Transform t) {
        if (building && current != null)
        {

            current.transform.position = t.position;
            current.transform.localScale += new Vector3(.01f, .01f, .01f);
        }
        else
            StartBuilding(t);
    }
    public void FinishBuilding() {
        if (building)
        {
            //gather.gameObject.SetActive(false);
            //asteroid.GetComponent<UsePlanetGravity>().pg = transform.parent.GetComponent<PlayerAttributes>().orbiting.GetComponent<PlanetGravity>();
            if (current != null) { 
                current.transform.SetParent(null);
                //current.GetComponent<Rigidbody>().mass = massToScaleRatio * current.transform.localScale.x;
                current = null;
            }
            building = false;
        }
    }


    
    private GameObject instGO(GameObject pre, Transform t) {
        //gather.gameObject.SetActive(true);
        if (GameManager.instance.currentSystem != null)
        {
            current = Instantiate(pre, t.position, Quaternion.identity, GameManager.instance.currentSystem.transform);
            current.transform.localScale = new Vector3(.001f, .001f, .001f);
            return current;
        }
        return null;
    }


}
