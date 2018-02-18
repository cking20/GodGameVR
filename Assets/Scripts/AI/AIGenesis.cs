using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenesis : MonoBehaviour
{
    public GameObject NPCPrefab;
    //GameObject curr;
    BaseAI curAI;
    bool birthed;
    void Start()
    {
        
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            print("new NPC");
            curAI = other.GetComponent<BaseAI>();
            birthed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            print("done with NPC");
            curAI = null;
            birthed = true;
        }
    }
    void OnParticleCollision(GameObject other)
    {
        print("sprinkled");
        if (other.CompareTag("AttrParts") && !birthed)
        {
            int c = curAI.GetTotalAttrib();
            print(c);
            if (other.name.CompareTo("Vitality") == 0)
                curAI.maxHealth++;
            if (other.name.CompareTo("Fertility") == 0)
                curAI.fertility++;
            if (other.name.CompareTo("Morality") == 0)
                curAI.morality++;
            




            transform.localScale = new Vector3(0.0125f, ((1f+c) / 3000f), 0.0125f);
            if (c+1 >= 100)
                birthed = true;
        }
    }
}
