using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public float moveSpeed = 10;
	private Vector3 moveDir;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		moveDir = new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        
        
    }
	void FixedUpdate(){
        rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);

    }







}
