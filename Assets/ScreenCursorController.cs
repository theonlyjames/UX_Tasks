using UnityEngine;
using System.Collections;

public class ScreenCursorController : MonoBehaviour {
	public GameObject screen;
	public GameObject toggleObject;

	private Vector3 mouseMovement;
	private Vector3 screenPosition;
	private bool follow = false;

	// Use this for initialization
	void Start () {
		//screenPosition = screen.transform
	}
	
	// Update is called once per frame
	void Update () {
		mouseMovement = Input.mousePosition;	
		mouseMovement.z = -Camera.main.transform.position.z;
		//mouseMovement.z = transform.position.z;
		mouseMovement = Camera.main.ScreenToWorldPoint (mouseMovement);
	}

	void FixedUpdate() {
		if (follow) {
			FollowMouseToggle ();
		} else {
			toggleObject.SetActive (true);
		}
	}

	void OnGUI() {
		if (Event.current.type.Equals(EventType.KeyUp)) {
			// Nothing for now
		} else if (Event.current.type.Equals(EventType.KeyDown) ) {
			SetKeyStateDown ();
		}
	}

	void FollowMouseToggle() {
		// mouseMovement = Camera.main.ScreenToWorldPoint (mouseMovement);
		transform.position = mouseMovement;
	}

	void SetKeyStateDown() {
		switch (Event.current.keyCode.ToString ()) {
		case "H":
			follow = true;
			break;
		case "J":
			follow = false;
			break;
		default:
			break;
		}
	}
}
