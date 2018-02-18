using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseAI : MonoBehaviour {

    //the sum of all stats must == 100
    public int maxAttributeSum = 1000;
    public int maxAge = 1000000;
    public int childhoodTime = 100;
    public int maxHealth = 100;
    public int health = 100;
    public int fertility = 100;//time between kids
    public int fertilityTimer = 0;
    public int adventerous;
    public int speed;
    public int buildingSkill;
    public int fightingSkill;
    public int farmingSkill;
    public int morality;
    public int fickleness = 1;//how quickly they lose belief
    public int awareness = 1;
    public bool adult = false;
    public bool fertile = false;
    public bool hungery = false;
    public bool fighting = false;
    //end stats
    public int belief = 10;
    public int age = 0;
    Rigidbody rb;
    
    public GameObject home;
    public BaseAI attacking;
    public BaseAI parent;
    public AIEventManager.Event state;// just think of the event but with an ing at the end
    public Vector3 targetPos;
    public bool believes;
    private TextMesh text;
    public bool canGenesis;


    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        text = GetComponentInChildren<TextMesh>();
        canGenesis = true;
        //GenesisRandom();
    }
    
   
    private void GenesisRandom()
    {
        int c = maxAttributeSum;
        maxHealth = Random.Range(0, c);
        c -= maxHealth;
        fertility = Random.Range(0, c);
        c -= fertility;
        buildingSkill = Random.Range(0, c);
        c -= buildingSkill;
        fightingSkill = Random.Range(0, c);
        c -= fightingSkill;
        farmingSkill = Random.Range(0, c);
        c -= farmingSkill;
        adventerous = Random.Range(0, c);
        c -= adventerous;
        speed = 1;//(short)Random.Range(0, c);
        //c -= speed;
        morality = c;

        name = "Rando";
        belief = 10;
        
    }
	// Update is called once per frame
	
    public int GetTotalAttrib() {
        return (maxHealth + fertility + adventerous + speed + buildingSkill + fightingSkill + farmingSkill + morality);
    }
    public void MoveTo(Vector3 t) {
        Vector3 diff = t - transform.position;
        Transform centOfPlanet = gameObject.GetComponent<UsePlanetGravity>().pg.transform;
        //diff = centOfPlanet.rotation * diff;
        //Debug.DrawRay(transform.position, diff, Color.red);        
        float theta = Vector3.Angle(transform.forward, diff) * Mathf.Deg2Rad;
        float o = Mathf.Sin(theta) * diff.magnitude;

        //Vector3 localUpPoint = transform.up * o;
        Vector3 localUpPoint = transform.up * o;
        localUpPoint = centOfPlanet.rotation * localUpPoint;
        //Debug.DrawRay(t+transform.position, localUpPoint+transform.position, Color.yellow);
        Vector3 toForeward = diff + localUpPoint;
        toForeward = centOfPlanet.rotation * toForeward;
        //Debug.DrawRay(transform.position, toForeward, Color.blue);        
        rb.AddForce(toForeward.normalized * speed * .25f);
        toForeward.y = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(toForeward), Time.deltaTime * speed);
        
    }
    public void SetText(string t) {
        text.text = t;
    }
    public void OnParticleCollision(GameObject other)
    {
        
        if (other.CompareTag("AttrParts") && canGenesis) {
            int c = GetTotalAttrib();
            if (c >= maxAttributeSum)
                canGenesis = false;
            print(other.name);
            if (other.name.CompareTo("Vitality") == 0)
                maxHealth++;
            if (other.name.CompareTo("Fertility") == 0)
                fertility++;
            if (other.name.CompareTo("Morality") == 0)
                morality++;
            if (other.name.CompareTo("Lifetime") == 0)
                maxAge++;
            if (other.name.CompareTo("Curiosity") == 0)
                adventerous++;
            if (other.name.CompareTo("Build") == 0)
                buildingSkill++;
            if (other.name.CompareTo("Fight") == 0)
                fightingSkill++;
            if (other.name.CompareTo("Farm") == 0)
                farmingSkill++;
        }
    }
}
