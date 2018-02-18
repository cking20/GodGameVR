using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerAttributes))]
[RequireComponent(typeof(MeshDeformerInput))]
[RequireComponent(typeof(ObjectPlacementInput))]
[RequireComponent(typeof(BuildAstroBodies))]
[RequireComponent(typeof(PowerInput))]
public class Controls : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject headset;
    //public float throwMult;
    //public float grabRadius;
    //public LayerMask grabLayer;
    private GameObject grabbed;
    private bool grabbing;
    PlayerAttributes pa;
    MeshDeformerInput mdi;
    ObjectPlacementInput opi;
    BuildAstroBodies bab;
    PowerInput pi;
    //public GameObject menu;
    public Transform focus;
    GameObject activeMenu;

    // Use this for initialization
    void Start()
    {
        pa = GetComponent<PlayerAttributes>();
        mdi = GetComponent<MeshDeformerInput>();
        opi = GetComponent<ObjectPlacementInput>();
        bab = GetComponent<BuildAstroBodies>();
        pi = GetComponent<PowerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT STUFF
        if (pa.orbiting != null)//orbit movement
        {
            
            pa.orbiting.transform.RotateAround(pa.orbiting.transform.position, transform.TransformDirection(new Vector3(Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"), 0)), 20 * Time.deltaTime);
            //transform.LookAt(pa.orbiting.position);
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
            {//zoom in
                transform.position = Vector3.MoveTowards(transform.position, pa.orbiting.position, -pa.speed * GameManager.instance.player.transform.localScale.x * Time.deltaTime);
            }
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstick))
            {//zoom out
                transform.position = Vector3.MoveTowards(transform.position, pa.orbiting.position, pa.speed * GameManager.instance.player.transform.localScale.x * Time.deltaTime);
            }
            

        }
        else //regular movement
        {
            transform.position += headset.transform.forward * Input.GetAxis("Vertical"); //new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
            {//scale down
                if (transform.localScale.x < 2000f)
                {
                    transform.localScale *= 1.1f;
                    GameManager.instance.player.GetComponent<PlayerAttributes>().speed *= 1.1f;
                }
            }
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstick))
            {//scale up
                if (transform.localScale.x > 1f)
                {
                    transform.localScale *= .9f;
                    GameManager.instance.player.GetComponent<PlayerAttributes>().speed *= .9f;
                }
            }
        }

        //BUILD/POWER STUFF
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > .9f)
        {// left trigger
            LeftHandActivePower();
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > .9f)
        {//right trigger
            RightHandActivePower();
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == 0f && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) == 0f)
        {//trigger up
            if (bab.enabled)
                bab.FinishBuilding();
            rightHand.GetComponent<LineRenderer>().enabled = false;
            leftHand.GetComponent<LineRenderer>().enabled = false;
        }
        
        //MENU STUFF
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {//left hand menu
            if (activeMenu == null)
            {
                activeMenu = Instantiate(PrefabManager.instance.MenuSystem, leftHand.transform.position, leftHand.transform.rotation, gameObject.transform);
                //rightHand.GetComponent<BoxCollider>().enabled = true;
            }
            else if (activeMenu.activeSelf)
            {
                activeMenu.SetActive(false);
            }
            else {
                activeMenu.SetActive(true);
                activeMenu.transform.position = leftHand.transform.position;
                activeMenu.GetComponent<Rigidbody>().velocity = Vector3.zero;
                activeMenu.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.One))
        {//right hand menu
            if (activeMenu == null)
            {
                activeMenu = Instantiate(PrefabManager.instance.MenuSystem, rightHand.transform.position, rightHand.transform.rotation, gameObject.transform);
               // leftHand.GetComponent<BoxCollider>().enabled = true;
            }
            else if (activeMenu.activeSelf)
            {
                activeMenu.SetActive(false);
            }
            else
            {
                activeMenu.SetActive(true);
                activeMenu.transform.position = rightHand.transform.position;
                activeMenu.GetComponent<Rigidbody>().velocity = Vector3.zero;
                activeMenu.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
        


        //ORBIT STuFF
        if (OVRInput.Get(OVRInput.Button.Two))
        {//orbit
            RaycastRight();

        }
        if (OVRInput.GetUp(OVRInput.Button.Two))
        {//orbit
            rightHand.GetComponent<LineRenderer>().enabled = false;
            pa.orbiting = null;
            Ray r = new Ray(rightHand.transform.position, rightHand.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit))
            {
                print(hit.transform.name);
                GameManager.instance.SetOrbiting(hit.transform.gameObject);
                //pa.orbiting = hit.transform;
                Vector3 diff = focus.position - hit.transform.position;
                transform.position -= diff;
                Scale(hit.transform.localScale.x * 10f);
                //transform.LookAt(transform.position + transform.position);
            }

        }
        if (OVRInput.Get(OVRInput.Button.Four))
        {//orbit
            //rightHand.GetComponent<LineRenderer>().enabled = true;
            //RaycastLeft();

        }
        if (OVRInput.GetUp(OVRInput.Button.Four))
        {//orbit
            //ConfirmOrbit();

        }
        /*
        //Hand Colision boxes
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbRest))
        {
            leftHand.GetComponent<BoxCollider>().enabled = true;
        } else
        {
            leftHand.GetComponent<BoxCollider>().enabled = false;
        }
        if (OVRInput.Get(OVRInput.Touch.SecondaryThumbRest))
        {
            rightHand.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            rightHand.GetComponent<BoxCollider>().enabled = false;
        }
        */
    }
    void RaycastLeft() {
        leftHand.GetComponent<LineRenderer>().enabled = true;
    }
    void RaycastRight() {
        rightHand.GetComponent<LineRenderer>().enabled = true;
        rightHand.GetComponent<LineRenderer>().widthMultiplier = transform.localScale.x / 10f;
    }
    void ConfirmOrbit() {
        rightHand.GetComponent<LineRenderer>().enabled = false;
        leftHand.GetComponent<LineRenderer>().enabled = false;
        pa.orbiting = null;
        Debug.DrawRay(rightHand.transform.position, rightHand.transform.forward);
        Ray r = new Ray(rightHand.transform.position, rightHand.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        {
            //print(hit.transform.name);
            pa.orbiting = hit.transform;
            Vector3 diff = focus.position - hit.transform.position;
            transform.position += diff;
            Scale(hit.transform.localScale.x);

        }
    }
    
    void LeftHandActivePower() {
        Ray r = new Ray(leftHand.transform.position, leftHand.transform.forward);
        if (mdi.enabled)
        {//raise/lower terrain
            leftHand.GetComponent<LineRenderer>().enabled = true;
            mdi.HandleInput(r, -OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y);
        }
        if (opi.enabled)
        {
            leftHand.GetComponent<LineRenderer>().enabled = true;
            opi.HandleInput(r, -OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y);
        }
        if (bab.enabled)
            bab.KeepBuilding(leftHand.transform);
        if (pi.enabled)
        {
            leftHand.GetComponent<LineRenderer>().enabled = true;
            pi.UseActivePower();
        }
    }

    void RightHandActivePower() {
        Ray r = new Ray(rightHand.transform.position, rightHand.transform.forward);
        if (mdi.enabled)
        {//raise/lower terrain
            rightHand.GetComponent<LineRenderer>().enabled = true;
            mdi.HandleInput(r, -OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y);
        }
        if (opi.enabled)
        {
            rightHand.GetComponent<LineRenderer>().enabled = true;
            opi.HandleInput(r, -OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y);
        }
        if (bab.enabled)
            bab.KeepBuilding(rightHand.transform);
        if (pi.enabled)
        {
            rightHand.GetComponent<LineRenderer>().enabled = true;
            pi.UseActivePower();
        }
    }

    void Scale(float newScale) {
        if(newScale <= 2000f && newScale > 1f)
           transform.localScale = new Vector3(newScale,newScale,newScale);
        
    }
    /*
    void GrabObj(GameObject hand)
    {
        grabbing = true;
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(hand.transform.position, grabRadius, hand.transform.forward, 0f, grabLayer);
        if (hits.Length > 0)
        {
            int closest = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                print(hits[i].transform.name);
                if (hits[i].distance < hits[closest].distance && !hits[closest].transform.CompareTag("Player")) closest = i;
            }
            print("Closest: " + hits[closest].transform.name);
            if (!hits[closest].transform.CompareTag("Player"))
            {
                grabbed = hits[closest].transform.gameObject;
                grabbed.GetComponent<Rigidbody>().isKinematic = true;
                grabbed.transform.position = hand.transform.position;
                grabbed.transform.parent = hand.transform;
                grabbed.SendMessage("OnGrabbed", hand, SendMessageOptions.RequireReceiver);
            }
        }
    }
    void DropObj(GameObject hand)
    {
        grabbing = false;
        if (grabbed != null)
        {
            grabbed.transform.parent = null;
            Rigidbody rb = grabbed.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = OVRInput.GetLocalControllerVelocity(hand.GetComponent<TouchController>().controller).sqrMagnitude * throwMult * rb.mass * hand.transform.forward;
            rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(hand.GetComponent<TouchController>().controller);
            grabbed.SendMessage("OnDropped", hand, SendMessageOptions.RequireReceiver);
            grabbed = null;
        }

    }
    */
}
