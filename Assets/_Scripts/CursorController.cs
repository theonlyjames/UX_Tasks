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


	private float zMove;

	// Key states
	private bool wKeyPressed = false;
	private bool sKeyPressed = false;
	private bool keyDown = false;

	// Mouse state
	private bool mouseUp = true;

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
		zMove = transform.position.z;
		mouseMovement.z = -transform.position.z;
	}
	
	// Colider Flys to mouse position and when it gets there it jumps around to the 
	// mouse position when it reaches the cursor
	void Update () {
		mouseMovement = Input.mousePosition;
		if (!keyDown) {
			 // mouseMovement.z = -transform.position.z;
			mouseMovement.z = -Camera.main.transform.position.z - transform.position.z;
			mouseMovement = Camera.main.ScreenToWorldPoint (mouseMovement);
		}

		direction = mouseMovement - transform.position;
		distance = Vector3.Distance (mouseMovement, transform.position);
			
		if (Input.GetMouseButton (0) && !hitGround) {
			mouseUp = false;
			if (distance >= snapToCursorThreshold && !closeEnough && postDropRelease && !keyDown) {
				rb.AddForce (direction * (speed));
				rb.drag = 1 / distance;
			} else {
				postDropRelease = false;
				// rb.MovePosition (mouseMovement);
				// rb.transform.position = mouseMovement;
				AdjustCursorPosition ();
				Debug.Log ("close enoug");
			}
		} else if (Input.GetMouseButtonUp (0)) {
			mouseUp = false;
			postDropRelease = true;
			hitGround = false;
			closeEnough = false;
		}

		KeyControl();

	}

	void KeyControl() {
		// TODO: Factor out to helper
		if (wKeyPressed || sKeyPressed) {
			Debug.Log ("A key was pressed");
			float tmp = transform.position.z;
			rb.drag = 10;
			if (wKeyPressed && tmp < 5) {
				zMove = tmp += 0.1f;
				mouseMovement.z = zMove;
				//transform.position.z = zMove;
				Debug.Log ("w keypressed");
			} else if (sKeyPressed && tmp < 5) {
				zMove = tmp -= 0.1f;
				mouseMovement.z = zMove;
				//transform.position.z = zMove;
				Debug.Log ("s keypressed");
			}
			//Vector3 adjustZ = new Vector3(transform.position.x, transform.position.y, zMove);
			// rb.MovePosition (adjustZ);
			// rb.transform.position = mouseMovement;
			mouseMovement.x = transform.position.x;
			mouseMovement.y = transform.position.y;
			AdjustCursorPosition ();
		}
	}

	void AdjustCursorPosition() {
		transform.position = mouseMovement;
	}

	void ResetKeysState() {
		sKeyPressed = false;
		wKeyPressed = false;
		keyDown = false;
	}

	void SetKeyStateDown() {
		switch (Event.current.keyCode.ToString ()) {
		case "W":
			sKeyPressed = false;
			wKeyPressed = true;
			break;
		case "S":
			wKeyPressed = false;
			sKeyPressed = true;
			break;
		default:
			break;
		}
		keyDown = true;
	}

	void OnGUI() {
		if (Event.current.type.Equals(EventType.KeyUp)) {
			ResetKeysState ();
		} else if (Event.current.type.Equals(EventType.KeyDown) ) {
			SetKeyStateDown ();
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
