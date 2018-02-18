using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyGalaxy : MonoBehaviour {
    //GenGalaxy gg;
    public float deltaTheta = 2;
    Vector3 startingAngle;
	// Use this for initialization
	void Start () {
        //gg = GetComponent<GenGalaxy>();
        //InvokeRepeating("UpdateGalaxy", .1f, .1f);
        startingAngle = transform.eulerAngles;

    }
	
	// Update is called once per frame
	void Update () {
        UpdateGalaxy();
	}
    void UpdateGalaxy() {
        //gg.angleDelta = (gg.angleDelta + .001f) ;
        GameObject g;
        GameObject gal = gameObject;
        for (int j = 0; j < gal.transform.childCount; j++) {
            for(int i = 0; i < gal.transform.GetChild(j).transform.childCount; i++)
            {
                g = gal.transform.GetChild(j).GetChild(i).gameObject;
                //g.transform.GetChild(0).gameObject.transform.localScale += new Vector3(1 + i * gg.scaleDelta, 1 + i * gg.scaleDelta, 1 + i * gg.scaleDelta);
                //g.transform.GetChild(0).gameObject.transform.localPosition -= new Vector3(0, (1 + i * gg.scaleDelta) / 2f, 0);
                Vector3 t =startingAngle + new Vector3(90, 0, (i * deltaTheta));
                g.transform.eulerAngles = Vector3.Lerp(startingAngle, t, 1f);

            }
        }
        
    }
}
