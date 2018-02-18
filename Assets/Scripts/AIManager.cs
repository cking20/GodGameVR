using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {
    /*
     * keeps track of all the AI, updates their states,and queues events based on their states
     */
    public static AIManager instance;
    public List<BaseAI> NPCList;
    
    //AIEventManager man;
    private float cutOffTime = 1f/60f;
    private int progress = 0;
    //int totalBelief = 0;
    //private PlayerAttributes pa;

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
        //AIPrefab = PrefabManager.instance.NPC;
        //HousePrefab = PrefabManager.instance.NPCHouse;
        //pa = GameManager.instance.player.GetComponent<PlayerAttributes>();
        NPCList = new List<BaseAI>();
        //man = AIEventManager.instance;//GetComponent<AIEventManager>();
        //GameObject adam = Instantiate(AIPrefab, new Vector3(transform.position.x, 5f, transform.position.z), Quaternion.identity, transform);
        //GameObject eve = Instantiate(AIPrefab, new Vector3(transform.position.x, 5f, transform.position.z), Quaternion.identity, transform);
        //adam.GetComponent<BaseAI>().partner = eve.GetComponent<BaseAI>();
        //eve.GetComponent<BaseAI>().partner = adam.GetComponent<BaseAI>();
        //AIs.Add(adam.GetComponent<BaseAI>());
        //AIs.Add(eve.GetComponent<BaseAI>());
        //InvokeRepeating("testBirth", 1f, 2f);
        //InvokeRepeating("UpdateAI", 1f, .1f);
    }

    // Update is called once per frame
    void Update () {
        UpdateAI();
	}
    /* 
     * Queue events based on state and attributes
     */
    void UpdateAI() {
        float stop = Time.time + cutOffTime;
        BaseAI current;
        if (NPCList == null)
            return;
        for(int i = progress; i < NPCList.Count && Time.time < stop; i++)
        {
            current = NPCList[i];
            //totalBelief += current.belief;

            current.fertilityTimer++;
            current.age++;

            //priorities 
            if (current.hungery)
            {
                //current.state = AIEventManager.Event.eat;
                
            }
            if (current.age > current.maxAge || current.health <= 0)
            {
                current.state = AIEventManager.Event.dead;
            }
            switch (current.state) {
                case AIEventManager.Event.idle:
                    if (!current.adult)//child
                    {
                        if (current.age >= current.childhoodTime)
                            current.adult = true;
                        //not a child -> chill
                    }
                    else//adult 
                    {
                        if (current.home == null)
                        {
                            current.state = AIEventManager.Event.build;
                            
                        } else
                        {// adult with a house(do stuffs)
                            
                            if (current.fertilityTimer > current.fertility) {//adult with house having a kid
                                current.fertilityTimer = 0;
                                current.state = AIEventManager.Event.reproduce;
                                
                            }
                            //observe

                            //fight if has a target
                            if (current.attacking != null)
                                current.state = AIEventManager.Event.fight;
                        }

                    }
                    //man.EnqueueEvent(new EventItem(AIs[i], AIEventManager.Event.idle, AIs[i].targetPos));
                    break;
                case AIEventManager.Event.born:
                    AIEventManager.instance.EnqueueEvent(new EventItem(current, AIEventManager.Event.born, current.parent.home.transform.position));
                    break;
                case AIEventManager.Event.moveTo:
                    AIEventManager.instance.EnqueueEvent(new EventItem(current, AIEventManager.Event.moveTo, current.targetPos));
                    break;
                case AIEventManager.Event.build:
                    AIEventManager.instance.EnqueueEvent(new EventItem(current, AIEventManager.Event.build, current.transform.position));
                    break;
                case AIEventManager.Event.houseBuilt:
                    current.state = AIEventManager.Event.moveTo;
                    AIEventManager.instance.EnqueueEvent(new EventItem(current, AIEventManager.Event.moveTo, current.home.transform.position));
                    break;
                case AIEventManager.Event.reproduce:
                    AIEventManager.instance.EnqueueEvent(new EventItem(current, AIEventManager.Event.reproduce, current.home.transform.position));
                    break;
                case AIEventManager.Event.fight:
                    //if(current.health < (current.maxHealth >> 4))//low health, bail out


                    AIEventManager.instance.EnqueueEvent(new EventItem(current, AIEventManager.Event.fight, current.attacking.transform.position));
                    break;
                case AIEventManager.Event.dead:
                    AIEventManager.instance.EnqueueEvent(new EventItem(current, AIEventManager.Event.dead, current.transform.position));
                    break;
                default:// state not implemented
                    current.state = AIEventManager.Event.idle;
                    break;
            }
            progress++;        
        }
        if (progress >= NPCList.Count) {//done going through the list
            progress = 0;
            //GameManager.instance.currentSystem.GetComponent<SystemAttributes>().currencyPerframe = totalBelief;
            //totalBelief = 0;
        }
    }
    public void testBirth(Transform t)
    {
        if (GameManager.instance.player.GetComponent<PlayerAttributes>().orbiting != null)
        {
            GameObject adam = Instantiate(PrefabManager.instance.NPC, t.position, Quaternion.identity, GameManager.instance.currentSystem.transform);
            print(adam.name);
            NPCList.Add(adam.GetComponent<BaseAI>());
            print(NPCList.Count);
        }
    }

    public void BirthChild(BaseAI parent) {
        
        BaseAI a = Instantiate(PrefabManager.instance.NPC, parent.transform.position, Quaternion.identity, GameManager.instance.currentSystem.transform).GetComponent<BaseAI>();
        a.transform.position = parent.transform.position;
        a.parent = parent;
        a.state = AIEventManager.Event.born;
        NPCList.Add(a);

    }
    
}
