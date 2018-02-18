using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemAttributes : MonoBehaviour {
    public int systemProgressLevel = 0;
    public int lifeProgressLevel = 0;
    public int maxUpgradeSlots = 0;
    public int currencyPerframe = 0;
    public int systemCurrency = 0;
    public int systemCurrencyCap = 10000;
    public List<BaseAI> NPCList;
    // Use this for initialization
    void Awake () {
		if(NPCList == null) {
            NPCList = new List<BaseAI>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        systemCurrency += currencyPerframe;
        if (systemCurrency > systemCurrencyCap)
            systemCurrency = systemCurrencyCap;
	}
}
