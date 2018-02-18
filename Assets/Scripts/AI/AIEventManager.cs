using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEventManager : MonoBehaviour {
    public static AIEventManager instance;
    public enum Event { born, idle, scaredOff, angry, amazed, frozen, moveTo, listen, think, sleep, eat, build, fight, farm,
        houseOnFire, onfire, houseBuilt, reproduce, hurt, dead, devinePickUp, devineAmaze, inAir,
        catastrophy, };
    private Queue<EventItem> eventQueue;
    //private AIManager AIMan;
    private float maxEventTime = 1f / 60f;
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
    void Start() {
        eventQueue = new Queue<EventItem>(1000);
        //AIMan = GetComponent<AIManager>();
        //pa = GameManager.instance.player.GetComponent<PlayerAttributes>();
    }

    // Update is called once per frame
    void Update() {
        float cutOffTime = Time.time + maxEventTime;
        int startingCount = eventQueue.Count;
        int i = 0;
        for (i = 0; i < startingCount && Time.time < cutOffTime; i++)
        {
            ProcessEvent(eventQueue.Dequeue());
        }
        //print(i + "events processed");
    }
    public void EnqueueEvent(EventItem e) {
        eventQueue.Enqueue(e);
    }
    private void ProcessEvent(EventItem e) {
        switch (e.theEvent) {
            case Event.moveTo://keep moving if not at target
                if (!AtPos(e.happenTo.transform.position, e.target))
                {
                    e.happenTo.MoveTo(e.target);
                    //EnqueueEvent(e);
                }
                else e.happenTo.state = Event.idle;
                break;
            case Event.born://keep moving if not at target
                if (!AtPos(e.happenTo.transform.position, e.target))
                {
                    e.happenTo.MoveTo(e.target);
                    //EnqueueEvent(e);
                }
                else e.happenTo.state = Event.idle;
                break;
            case Event.idle:
                e.happenTo.SetText("What is the meaning of life...");
                break;
            case Event.dead:
                print(e.happenTo.name+" died");
                GameManager.instance.currentSystem.GetComponent<SystemAttributes>().systemCurrency += e.happenTo.belief;
                AIManager.instance.NPCList.Remove(e.happenTo);
                Destroy(e.happenTo.gameObject);
                break;
            case Event.build:
                e.happenTo.SetText("time to build");
                //print(GameManager.instance.player.GetComponent<PlayerAttributes>().orbiting.name);
                PlanetObjectsHandler poh = GameManager.instance.player.GetComponent<PlayerAttributes>().orbiting.GetComponent<PlanetObjectsHandler>();
                GameObject t = poh.AddSingle(e.happenTo.transform.position,Vector3.forward,100f,10f, PrefabManager.instance.NPCHouse);
                //e.happenTo.state = Event.houseBuilt;
                if (t != null)
                {
                    e.happenTo.SetText("what a lovely home");
                    e.happenTo.home = t;
                    //go to the house
                    e.happenTo.state = Event.houseBuilt;
                    e.happenTo.targetPos = t.transform.position;
                    e.happenTo.belief += e.happenTo.fickleness * 10;
                }
                else
                {
                    e.happenTo.belief -= e.happenTo.fickleness;
                }
                break;
            case Event.reproduce:
                if (e.happenTo.home == null)// || e.happenTo.partner == null)
                {
                    e.happenTo.SetText("Should build a house before kids");
                    e.happenTo.state = Event.idle;
                    //EnqueueEvent(new EventItem(e.happenTo, Event.idle, e.happenTo.home));
                    break;
                }

                if (!AtPos(e.happenTo.transform.position, e.happenTo.home.transform.position))
                {
                    e.happenTo.SetText("Bout to get me that PUHSAAHHH");
                    e.happenTo.MoveTo(e.happenTo.home.transform.position);
                }
                else {//if (AtPos(e.happenTo.partner.transform.localPosition, e.happenTo.home.transform.position)) {
                    e.happenTo.SetText("I Did a Birth");
                    e.happenTo.state = Event.idle;
                    e.happenTo.belief += e.happenTo.fickleness * 5;
                    AIManager.instance.BirthChild(e.happenTo);
                }
                break;
            case Event.fight:
                if (!AtPos(e.happenTo.transform.localPosition, e.target))
                {
                    e.happenTo.MoveTo(e.target);
                }
                else
                {
                    if ((e.happenTo.attacking.health -= e.happenTo.fightingSkill) <= 0)
                    {
                        e.happenTo.attacking = null;
                        e.happenTo.state = Event.idle;
                    }
                }
                break;
            default:
                break;
        }
        return;
    }
    bool AtPos(Vector3 a, Vector3 b)
    {
        float dist = (a - b).magnitude;
        //print(dist);
        return dist < 1f;
    }



}
public class EventItem{
    public BaseAI happenTo;
    public AIEventManager.Event theEvent;
    public Vector3 target;

    public EventItem(BaseAI g, AIEventManager.Event e, Vector3 t) {
        happenTo = g;
        theEvent = e;
        target = t;

    }
}
