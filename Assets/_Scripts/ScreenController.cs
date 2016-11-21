using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour {

	//
	// Borrow Start
	//
	public int gunDamage = 1;                                           // Set the number of hitpoints that this gun will take away from shot objects with a health script
	public float fireRate = 0.25f;                                      // Number in seconds which controls how often the player can fire
	public float weaponRange = 50f;                                     // Distance in Unity units over which the player can fire
	public float hitForce = 100f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player
	public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun

	public Camera mainCam;                                              // Holds a reference to the first person camera
	private Camera fpsCam;                                              // Holds a reference to the first person camera

	private WaitForSeconds shotDuration = new WaitForSeconds(0.5f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
	private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
	private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing

	//
	// Borrow END
	//

	private bool stopShoot = true;

	public GameObject hyperlink;
	private Renderer hyperlinkRend;

	// public Renderer rend;
	public GameObject cursor;
	public Color lerpedColor;

	private Vector3 mouseMovement;
	private Vector3 rayOrigin;

	// Use this for initialization
	void Start () {
		// Get and store a reference to our LineRenderer component
		laserLine = GetComponent<LineRenderer>();

		// Get and store a reference to our Camera by searching this GameObject and its parents
		fpsCam = mainCam.GetComponent<Camera>();


		//rend = GetComponent<Renderer>();
		lerpedColor = Color.white;

		hyperlinkRend = hyperlink.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Check if the player has pressed the fire button and if enough time has elapsed since they last fired
		if (Input.GetButtonDown ("Fire1") && Time.time > nextFire && !stopShoot) {
			// Update the time when our player can fire next
			nextFire = Time.time + fireRate;

			// Start our ShotEffect coroutine to turn our laser line on and off
			StartCoroutine (ShotEffect ());

			// Create a vector at the center of our camera's viewport
			rayOrigin = fpsCam.ViewportToWorldPoint (cursor.transform.position + (cursor.transform.forward * weaponRange));

			// Declare a raycast hit to store information about what our raycast has hit
			RaycastHit hit;

			// Set the start position for our visual effect for our laser to the position of gunEnd
			laserLine.SetPosition (0, gunEnd.position);

			// Check if our raycast has hit anything
			if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) {
				// Set the end position for our laser line 
				//laserLine.SetPosition (1, hit.point);

				// Get a reference to a health script attached to the collider we hit
				//ShootableBox health = hit.collider.GetComponent<ShootableBox>();

				// If there was a health script attached
				//if (health != null)
				//{
				// Call the damage function of that script, passing in our gunDamage variable
				//health.Damage (gunDamage);
				//}

				// Check if the object we hit has a rigidbody attached
				if (hit.rigidbody != null) {
					// Add force to the rigidbody we hit, in the direction from which it was hit
					//hit.rigidbody.AddForce (-hit.normal * hitForce);
					//Debug.Log ("HIT");
				}
			} else {
				// If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
				laserLine.SetPosition (1, cursor.transform.position + (cursor.transform.forward * weaponRange));
			}
		} else {
			// Declare a raycast hit to store information about what our raycast has hit
			RaycastHit hit;

			if (Physics.Raycast (gunEnd.position, (cursor.transform.forward * weaponRange), out hit, weaponRange)) {
				if (hit.collider.gameObject.CompareTag ("Hyperlink")) {
					Debug.Log ("HIT");
					hyperlinkRend.material.color = Color.red;
				} else {
					hyperlinkRend.material.color = Color.white;
				}
			} 
		}

		Debug.DrawRay(gunEnd.position, (cursor.transform.forward * weaponRange), Color.green);
	}

	private IEnumerator ShotEffect()
	{
		// Turn on our line renderer
		laserLine.enabled = true;

		//Wait for .07 seconds
		yield return shotDuration;

		// Deactivate our line renderer after waiting
		 laserLine.enabled = false;
	}

	void SetKeyStateDown() {
		switch (Event.current.keyCode.ToString ()) {
		case "H":
			stopShoot = false;
			break;
		case "J":
			stopShoot = true;
			break;
		default:
			break;
		}
	}
}
