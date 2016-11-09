using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

	private Vector3 mouseMovement;

	private Rigidbody rb;

	private Vector3 direction;

	private float distance;

	private bool closeEnough;

	public int speed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		closeEnough = false;
	}
	
	// Colider Flys to mouse position and when it gets there it jumps around to the 
	// mouse position when it reaches the cursor
	void Update () {
		mouseMovement = Input.mousePosition;
		mouseMovement.z = -Camera.main.transform.position.z;
		mouseMovement = Camera.main.ScreenToWorldPoint (mouseMovement);

		direction = mouseMovement - transform.position;

		distance = Vector3.Distance (mouseMovement, transform.position);
			
		if ( Input.GetMouseButton (0) ) {
			if ( distance >= 0.05 && !closeEnough ) {
				rb.AddForce (direction * (speed));
				rb.drag = 1 / distance;
			} else {
				closeEnough = true;
				rb.transform.position = mouseMovement;
				Debug.Log ("close enoug");
			}
		}
	}


//	SAVE THIS CODE IT WORKS FOR ELASTISITY
//	void Update () {
//		if (Input.GetMouseButton (0)) {
//			mouseMovement = Input.mousePosition;
//			mouseMovement.z = -Camera.main.transform.position.z;
//			mouseMovement = Camera.main.ScreenToWorldPoint(mouseMovement);
//			direction = mouseMovement - transform.position;
//			// rigidbody.transform.position = direction;
//			// rigidbody.velocity = ( ( transform.right * mouseMovement.x ) + ( transform.forward * mouseMovement.y ) ) / Time.deltaTime;
//			// rigidbody.velocity = new Vector3( ( transform.position.x * mouseMovement.x ), ( transform.position.y * mouseMovement.y ), ( transform.position.z * mouseMovement.z ) );
//			rigidbody.AddForce (direction * (speed / Time.deltaTime));
//			Debug.Log(Input.mousePosition);
//		}
//	}
}
