using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {

	private Vector3 mouseMovement;

	private Rigidbody rb;

	private Vector3 direction;

	private float distance;

	private bool closeEnough;

	public int speed;

	private double snapToCursorThreshold = 0.09;

	private bool hitGround = false;

	private bool postDropRelease = true;

	// Key states
	private bool letMove = false;
	private bool wKeyPressed = false;
	private bool sKeyPressed = false;

	// Thrust
	public float thrust;

	// LOG INFO
	public Text groundInfoText;
	public Text cursor3dPosText;

	private SphereCollider cursorCollider;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		closeEnough = false;
		cursorCollider = GetComponent<SphereCollider> ();
		Event.current = new Event ();
	}
	
	// Colider Flys to mouse position and when it gets there it jumps around to the 
	// mouse position when it reaches the cursor
	void Update () {
		mouseMovement = Input.mousePosition;
		mouseMovement.z = -Camera.main.transform.position.z;
		mouseMovement = Camera.main.ScreenToWorldPoint (mouseMovement);

		direction = mouseMovement - transform.position;

		distance = Vector3.Distance (mouseMovement, transform.position);
			
		if (Input.GetMouseButton (0) && !hitGround) {
			// Reset Keys
			sKeyPressed = false;
			wKeyPressed = false;
			if (distance >= snapToCursorThreshold && !closeEnough && postDropRelease) {
				rb.AddForce (direction * (speed));
				rb.drag = 1 / distance;
			} else {
				postDropRelease = false;
				rb.transform.position = mouseMovement;
				Debug.Log ("close enoug");
			}
		} else if ( Input.GetMouseButtonUp(0) ) {
			postDropRelease = true;
			hitGround = false;
			closeEnough = false;
		}

		// TODO: Factor out to helper
		if (wKeyPressed || sKeyPressed) {
			float tmp = rb.transform.position.z;
			if (wKeyPressed && tmp < 5) {
				float zMove = tmp += 0.1f;
				mouseMovement.z = zMove;
				rb.MovePosition (mouseMovement);
				Debug.Log ("w keypressed");
			} else if (sKeyPressed && tmp < 5) {
				float zMove = tmp -= 0.1f;
				mouseMovement.z = zMove;
				rb.MovePosition (mouseMovement);
				Debug.Log ("s keypressed");
			}
		}

		if (Input.GetKeyUp (KeyCode.W)) {
			sKeyPressed = false;
			wKeyPressed = false;
			Debug.Log ("w UP");
		} else if (Input.GetKeyUp (KeyCode.S)) {
			wKeyPressed = false;
			sKeyPressed = false;
			Debug.Log ("s UP");
		}
	}

	void SetKeyState() {
		if (Input.GetKeyDown (KeyCode.W) && !wKeyPressed) {
			sKeyPressed = false;
			wKeyPressed = true;
		} else if (Input.GetKeyDown (KeyCode.S) && !sKeyPressed) {
			wKeyPressed = false;
			sKeyPressed = true;
		}
	}

	void OnGUI() {
		if (Event.current.Equals (Event.KeyboardEvent ("w"))) {
			SetKeyState ();
		} else if (Event.current.Equals (Event.KeyboardEvent ("s") ) ) {
			SetKeyState ();
		}
	}


	void OnTriggerEnter(Collider other) {
		// When the sphere hits the ground 
		// Stop the sphear from following the cursor
		if(other.CompareTag("Ground")) {
			groundInfoText.text = " " + other.transform.position;
			cursor3dPosText.text = " " + transform.position;
			transform.position = new Vector3 (transform.position.x, cursorCollider.radius, transform.position.z);
			hitGround = true;
		}
		// Have the sphere set its x and z relative to its closes position on the ground
	}

	void ClearGates() {
	}

	void OnMouseUp() {
		closeEnough = false;
		Debug.Log ("Mouse UP");
	}

	void OnMouseExit() {
		hitGround = false;
		closeEnough = false;
		Debug.Log ("Mouse Exit");
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
