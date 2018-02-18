using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerAttributes))]
public class MovePlayArea : MonoBehaviour {
	Vector3 moveDir;
	PlayerAttributes pa;
	// Use this for initialization
	void Start () {
		pa = GetComponent<PlayerAttributes> ();
	}
	
	// Update is called once per frame
	void Update () {
        //moveDir = new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        if (pa.orbiting != null)
        {
            transform.RotateAround(pa.orbiting.transform.position, transform.TransformDirection(new Vector3(Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal"), 0)), 20 * Time.deltaTime);
            transform.LookAt(pa.orbiting.position);
            if (Input.GetKey(KeyCode.PageUp))
            {

                transform.position = Vector3.MoveTowards(transform.position, pa.orbiting.position, 10f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.PageDown))
            {

                transform.position = Vector3.MoveTowards(transform.position, pa.orbiting.position, -10f * Time.deltaTime);
            }
        }
        else
        {
            transform.position += new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        }
	}
	void FixedUpdate(){
		


	}
}
