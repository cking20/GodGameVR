using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMenu : MonoBehaviour {
    Transform left;
    Transform right;
    GameObject canvas;
    bool opened;
    OVRGrabbable grabbable;
    MeshDeformerInput mdi;
    ObjectPlacementInput opi;
    BuildAstroBodies bab;
    PowerInput pi;
    public static BookMenu instance;
    public int bannedEvents;
    //menus
    public GameObject mainM;
    public GameObject buildM;
    public GameObject astroSM;
    public GameObject natureSM;
    public GameObject hallowSM;

    public GameObject powerM;
    public GameObject eSM;
    public GameObject wSM;
    public GameObject aSM;
    public GameObject fSM;

    public GameObject cmdsM;
    public GameObject systemM;
    public GameObject settingsM;
    public GameObject controlM;

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

        opened = false;
        left = transform.GetChild(0).Find("left");
        right = transform.GetChild(0).Find("right");
        canvas = transform.GetChild(0).Find("Canvas").gameObject;
        grabbable = GetComponent<OVRGrabbable>();
        GameObject p = GameManager.instance.player;

        mdi = p.GetComponent<MeshDeformerInput>();
        opi = p.GetComponent<ObjectPlacementInput>();
        bab = p.GetComponent<BuildAstroBodies>();
        pi = p.GetComponent<PowerInput>();
    }
	
	// Update is called once per frame
	void Update () {
        canvas.SetActive(grabbable.isGrabbed);
          
            if (grabbable.isGrabbed)//opened animation
            {
            //print("open");
            if (opened == false) {
                ShowMain();
                opened = true;
            }
                left.localRotation = Quaternion.identity;
                right.localRotation = Quaternion.identity;
                
                
            }
            else
            {//closed animation
                //print("close");
                left.localRotation = Quaternion.Euler(new Vector3(0,0,-90));
                right.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            opened = false;
                
               
            }
        
	}
    public void Genesis() {
        AIManager.instance.testBirth(gameObject.transform);
    }
    public void SetBuildGameObj(GameObject refer) {
        bab.selectedPrefab = refer;
    }
    public void SetPlaceGameObj(GameObject refer)
    {
        opi.activeCreate = refer;
    }
    public void SetPower(GameObject refer) {
        pi.activePower = refer;
    }
    public void EnableSculpt() {
        mdi.enabled = true;
    }
    public void ShowMain() {
        HideAll();
        mainM.SetActive(true);
    }
    public void ShowBuild()
    {
        print("showBuild");
        HideAll();
        buildM.SetActive(true);
        DisablePlayerControls();
        bab.enabled = true;
    }
    public void ShowPowers()
    {
        print("showPowers");
        HideAll();
        powerM.SetActive(true);
        DisablePlayerControls();
        pi.enabled = true;
    }
    public void ShowCmds()
    {
        print("showCmds");
        HideAll();
        cmdsM.SetActive(true);
    }
    public void ShowControls()
    {
        //print("showCmds");
        HideAll();
        controlM.SetActive(true);
    }

    public void ShowSystem()
    {
        HideAll();
        systemM.SetActive(true);
    }
    public void ShowSettings()
    {
        HideAll();
        settingsM.SetActive(true);
    }
    public void ShowAstro()
    {
        HideSubMenus();
        astroSM.SetActive(true);
        DisablePlayerControls();
        bab.enabled = true;
    }
    public void ShowNature()
    {
        HideSubMenus();
        natureSM.SetActive(true);
        DisablePlayerControls();
        opi.enabled = true;
    }
    public void ShowHallow()
    {
        HideSubMenus();
        hallowSM.SetActive(true);
        DisablePlayerControls();
        opi.enabled = true;
    }
    public void ShowEarth()
    {
        HideSubMenus();
        eSM.SetActive(true);
    }
    public void ShowWater()
    {
        HideSubMenus();
        wSM.SetActive(true);
    }
    public void ShowAir()
    {
        HideSubMenus();
        aSM.SetActive(true);
    }
    public void ShowFire()
    {
        HideSubMenus();
        fSM.SetActive(true);
    }

    public void HideAll() {
        mainM.SetActive(false);
        buildM.SetActive(false);
        astroSM.SetActive(false);
        natureSM.SetActive(false);
        hallowSM.SetActive(false);

        powerM.SetActive(false);
        eSM.SetActive(false);
        wSM.SetActive(false);
        aSM.SetActive(false);
        fSM.SetActive(false);

        cmdsM.SetActive(false);

        systemM.SetActive(false);

        settingsM.SetActive(false);
}
    public void HideSubMenus() {
        astroSM.SetActive(false);
        natureSM.SetActive(false);
        hallowSM.SetActive(false);       
        eSM.SetActive(false);
        wSM.SetActive(false);
        aSM.SetActive(false);
        fSM.SetActive(false);
    }
    private void DisablePlayerControls() {
        mdi.enabled = false;
        opi.enabled = false;
        bab.enabled = false;
        pi.enabled = false;
    }

    public bool IsEventBanned(int index)
    {
        return (bannedEvents & (1 << index)) != 0;
    }

    public void SetRule(bool b, int index)
    {
        if (b)
            bannedEvents |= (1 << index);
        else
            bannedEvents &= ~(1 << index);
    }
}
