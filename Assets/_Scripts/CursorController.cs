using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorController : MonoBehaviour {

	private Vector3 mouseMovement;
	private Rigidbody rb;
	private Vector3 direction;
	private float distance;
	private bool closeEnough;
	private double snapToCursorThreshold = 0.09;
	private bool hitGround = false;
	private Vector3 seatPos;


	// Key states
	private bool wKeyPressed = false;
	private bool sKeyPressed = false;
	private bool keyDown = false;

	// Trigger states
	private bool seatEnter = false;
	private bool stopMotion = false;

	public int speed;

	// Thrust
	public float thrust;

	// LOG INFO
	public Text groundInfoText;
	public Text cursor3dPosText;
	public Text mouseCursorDeltaText;

	private SphereCollider cursorCollider;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		closeEnough = false;
		cursorCollider = GetComponent<SphereCollider> ();
		Event.current = new Event ();

		 mouseMovement.z = -transform.position.z;
	}
	
	// Colider Flys to mouse position and when it gets there it jumps around to the 
	// mouse position when it reaches the cursor
	void Update () {
	}

	void FixedUpdate() {
		mouseMovement = Input.mousePosition;
		if (!keyDown) {
			 // mouseMovement.z = -transform.position.z;
			mouseMovement.z = -Camera.main.transform.position.z - transform.position.z;
			mouseMovement = Camera.main.ScreenToWorldPoint (mouseMovement);
		}

		direction = mouseMovement - transform.position;

		CalcMouseCursorDelta ();
			
		//if (Input.GetMouseButton (0) && !hitGround) {
//			if (distance >= snapToCursorThreshold && !closeEnough && !keyDown) {
//				rb.AddForce (new Vector3 (direction.x * speed, direction.y * speed, direction.z * speed));
//				rb.drag = 1 / distance;
//			} else {
//				closeEnough = true;
//				AdjustCursorPosition (mouseMovement);
//				Debug.Log ("close enoug");
//			}
		//} else if (Input.GetMouseButtonUp (0)) {
			hitGround = false;
			closeEnough = false;
			if (seatEnter && !stopMotion) {
				Vector3 pos = Vector3.zero;
				//AdjustCursorPosition (pos);
				MovePositionWithForce();
			}
		//}


		KeyControl();

		LogData ();
	}

	void MovePositionWithForce () {
		var snapTo = new Vector3 (transform.position.x, 0.5f, 0.0f);
		direction = snapTo - transform.position;
		rb.AddForce (new Vector3 (direction.x * speed, direction.y * speed, direction.z * speed));

		var dist = Vector3.Distance (snapTo, transform.position);

		rb.drag = 1 / dist;
		if (dist <= 0.1) {
			stopMotion = true;
		}
		Debug.Log (dist);
	}

	void CalcMouseCursorDelta() {
		Vector3 tmp = mouseMovement;
		tmp.z = transform.position.z;
		distance = Vector3.Distance (tmp, transform.position);
	}

	void KeyControl() {
		// TODO: Factor out to helper
		Vector3 dir = transform.position;
		if (wKeyPressed || sKeyPressed && keyDown) {
			if (wKeyPressed) {
				dir = transform.forward;
			} else if (sKeyPressed) {
				dir = -transform.forward;
			}
			rb.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
		}
	}

	void AdjustCursorPosition(Vector3 updatePosition) {

		rb.MovePosition (transform.position - updatePosition * Time.deltaTime);
//		if (!hitGround) {
//			rb.MovePosition (mouseMovement);
//		}
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
		case "R":
			transform.position = new Vector3 (3.5f, 1.5f, 0f);
			rb.velocity = Vector3.zero;
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
//			hitGround = true;
//			groundInfoText.text = " " + other.transform.position;
//			cursor3dPosText.text = " " + transform.position;
//			rb.drag = 10;
//			rb.AddForce(new Vector3 (-direction.x * speed, -direction.y * speed, 0.0f));
//			rb.velocity = new Vector3(0, 0, 0);
//			rb.position = (new Vector3 (transform.position.x, other.gameObject.transform.position.y + cursorCollider.radius, transform.position.z));
		}
		SetSeat (other);
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag ("Seat")) {
			seatEnter = true;
			stopMotion = false;
			Debug.Log ("Seat EXIT");
		}
	}

	void SetSeat(Collider other) {
		if (other.gameObject.CompareTag ("Seat")) {
			Debug.Log ("Seat Entered");
			seatPos = new Vector3 (transform.position.x, 0.2f, 0.0f);
			seatEnter = true;
			//rb.MovePosition (seatPos * Time.deltaTime);
		}
	}


	void OnMouseUp() {
		closeEnough = false;
	}

	void OnMouseExit() {
		hitGround = false;
	}

	void LogData() {
		mouseMovement.z = transform.position.z;
		mouseCursorDeltaText.text = " " + distance;
	}
}
