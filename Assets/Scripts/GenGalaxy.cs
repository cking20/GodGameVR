using UnityEngine;
using System.Collections;

public class GenGalaxy : MonoBehaviour {
	public GameObject ellipse;
    public GameObject highlights;
	public GameObject clouds;
	public GameObject nebulaCenter;
    public GameObject nebulaRim;
    public GameObject h11Regions;
	GameObject center;
	public int numEllipse = 120;
	public int numClouds = 120;
	public int numH11 = 120;
	
	public int numNebula = 1;
	public float scaleDelta = .1f;
	public float angleDelta = 3f;
	// Use this for initialization
	void Awake () {
		
		GameObject g;
        center = gameObject;



        //dhould be evenly distributed in a circle
        GameObject holder = new GameObject("nebcent");
        holder.transform.parent = center.transform;
		for(int i =(1); i<= numNebula/2; i++){
            g = Instantiate(nebulaCenter, center.transform.position, Quaternion.identity) as GameObject;
            g.transform.parent = holder.transform;
            g.transform.GetChild(0).gameObject.transform.localScale += new Vector3(1 + i * scaleDelta, 1 + i * scaleDelta, 1 + i * scaleDelta);
            g.transform.GetChild(0).gameObject.transform.localPosition -= new Vector3(0, (1 + i * scaleDelta) / 2.55f, 0);

            g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize = (g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize / ((i * (scaleDelta))));// + ((-.05f / numEllipse) * i + .2f);
            ParticleSystem.MainModule m = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            //m.simulationSpeed = m.simulationSpeed / (i * (scaleDelta));
            ParticleSystem.ShapeModule e = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().shape;
            e.radius = e.radius / (i * scaleDelta);
            Vector3 t = g.transform.eulerAngles + new Vector3(90, 0, (i * angleDelta));
            g.transform.eulerAngles = Vector3.Lerp(g.transform.eulerAngles, t, 1f);
        }
        holder = new GameObject("rim");
        holder.transform.parent = center.transform;
        for (int i = 1; i <= numNebula; i++)
        {
            g = Instantiate(nebulaRim, center.transform.position, Quaternion.identity) as GameObject;
            g.transform.parent = holder.transform;
            g.transform.GetChild(0).gameObject.transform.localScale += new Vector3(1 + i * scaleDelta, 1 + i * scaleDelta, 1 + i * scaleDelta);
            g.transform.GetChild(0).gameObject.transform.localPosition -= new Vector3(0, (1 + i * scaleDelta) / 2.55f, 0);

            g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize = (g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize / ((1+i * (scaleDelta)))) + ((-.05f / numEllipse) * i + .2f);
            ParticleSystem.MainModule m = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            //m.simulationSpeed = m.simulationSpeed / (i * (scaleDelta));
            ParticleSystem.ShapeModule e = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().shape;
            e.radius = e.radius / (i * scaleDelta);
            Vector3 t = g.transform.eulerAngles + new Vector3(90, 0, (i * angleDelta));
            g.transform.eulerAngles = Vector3.Lerp(g.transform.eulerAngles, t, 1f);
            if (i < numNebula - 10)
                g.SetActive(false);
        }
        holder = new GameObject("clouds");
        holder.transform.parent = center.transform;
        for (int i = 1; i <= numClouds; i++) {

			g = Instantiate(clouds,center.transform.position,Quaternion.identity) as GameObject;
            g.transform.parent = holder.transform;
            g.transform.GetChild (0).gameObject.transform.localScale += new Vector3 (1+i*scaleDelta,1+i*scaleDelta,1+i*scaleDelta);
			g.transform.GetChild (0).gameObject.transform.localPosition -= new Vector3 (0,(1+i*scaleDelta)/2f,0);
            g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize = (g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize / ((i * (scaleDelta))));
            ParticleSystem.MainModule m = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            //m.simulationSpeed = m.simulationSpeed / (i * (scaleDelta));
            ParticleSystem.ShapeModule e = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().shape;
            e.radius = e.radius / (i * scaleDelta);
            Vector3 t = g.transform.eulerAngles + new Vector3 (90, 0, (i * angleDelta));
			g.transform.eulerAngles = Vector3.Lerp (g.transform.eulerAngles, t, 1f);
		}
        holder = new GameObject("stars");
        holder.transform.parent = center.transform;
        for (int i = 1; i <= numEllipse; i++) {

			g = Instantiate(ellipse,center.transform.position,Quaternion.identity) as GameObject;
            g.transform.parent = holder.transform;
            g.transform.GetChild (0).gameObject.transform.localScale += new Vector3 (1+i*scaleDelta,1+i*scaleDelta,1+i*scaleDelta);
            g.transform.GetChild (0).gameObject.transform.localPosition -= new Vector3 (0,(1+i*scaleDelta)/2.55f,0);

            g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize = (g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize / ((1+i * (scaleDelta))));// + ((-.05f / numEllipse) * i + .2f);
            ParticleSystem.MainModule m= g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            //m.simulationSpeed = m.simulationSpeed/ (i * (scaleDelta));
            ParticleSystem.ShapeModule e = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().shape;
            e.radius = e.radius / (i * scaleDelta);
            Vector3 t = g.transform.eulerAngles + new Vector3(90, 0, (i * angleDelta));
            g.transform.eulerAngles = Vector3.Lerp (g.transform.eulerAngles, t, 1f);
		}
        holder = new GameObject("highlights");
        holder.transform.parent = center.transform;
        //highlights
        if (highlights != null)
        for (int i = 1; i <= numEllipse/2; i++)
        {

            g = Instantiate(highlights, center.transform.position, Quaternion.identity) as GameObject;
                g.transform.parent = holder.transform;
                g.transform.GetChild(0).gameObject.transform.localScale += new Vector3(1 + i * scaleDelta*2, 1 + i * scaleDelta * 2, 1 + i * scaleDelta * 2);
            g.transform.GetChild(0).gameObject.transform.localPosition -= new Vector3(0, (1 + i * scaleDelta * 2) / 2f, 0);

                g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize = (g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().startSize / ((i * (scaleDelta))));// + ((-.05f / numEllipse) * i + .2f);
            ParticleSystem.MainModule m = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            //m.simulationSpeed = m.simulationSpeed / (i * (scaleDelta / 4));
            Vector3 t = g.transform.eulerAngles + new Vector3(90, 0, (i * angleDelta));
                g.transform.eulerAngles = Vector3.Lerp(g.transform.eulerAngles, t, 1f);
        }
        holder = new GameObject("holder");
        holder.transform.parent = center.transform;
        for (int i = numEllipse/4; i <= numH11; i++) {

			g = Instantiate(h11Regions,center.transform.position,Quaternion.identity) as GameObject;
            g.transform.parent = holder.transform;
            g.transform.GetChild (0).gameObject.transform.localScale += new Vector3 (1+i*scaleDelta,1+i*scaleDelta,1+i*scaleDelta);
			g.transform.GetChild (0).gameObject.transform.localPosition -= new Vector3 (0,(1+i*scaleDelta)/2f,0);
			g.transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ().startSize = ((-.1f / numH11) * i + .1f);
			g.transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ().startDelay = Random.Range (1, 10) / 10f;
			Vector3 t = g.transform.eulerAngles + new Vector3(90, 0, (i * angleDelta));
            ParticleSystem.MainModule m = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
            //m.simulationSpeed = m.simulationSpeed / (i * (scaleDelta));
            ParticleSystem.ShapeModule e = g.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().shape;
            e.radius = e.radius / (i * scaleDelta);
            g.transform.eulerAngles = Vector3.Lerp (g.transform.eulerAngles, t, 1f);
			i+=5;
		}

	}
	

}
