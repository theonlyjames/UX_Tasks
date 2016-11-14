using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChildContainerController : MonoBehaviour {
		public GameObject bulletPrefab;
		public Transform bulletSpawn;
		public Vector3 eulerAngleVelocity;

		private float numberOfObjects = 0;

	public Text rotateText;

	private Rigidbody rb;

		void Update() {
			var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
			var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		    rb = GetComponent<Rigidbody> ();
	
			transform.Rotate(0, x, 0);
			transform.Translate(0, 0, z);

			if (Input.GetKeyDown(KeyCode.Space))
			{
				Fire();
			}
		}


	void Fire() {
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation
		);

		// Set the bullet inside container
		//bullet.transform.SetParent (this);

		bullet.transform.parent = transform;

		var brb = bullet.GetComponent<Rigidbody> ();

		// Add velocity to the bullet
		//brb.velocity = bullet.transform.forward;

		//if (bullet.transform.position.z >= 2) {
			// bullet.GetComponent<Rigidbody> ().velocity = Vector3.zero;
//			var bx = bullet.transform.position.x;
//			var by = bullet.transform.position.y;
		bullet.transform.position = new Vector3 (0.0f, 0.0f, 2);
			Debug.Log ("> 2");
		//}

		// number of objects
		numberOfObjects = numberOfObjects += 1;
		// float rotateAngle = 360 / numberOfObjects;
		//float rotate = (numberOfObjects += brb.GetComponent<SphereCollider>().radius);
		float rotate = brb.GetComponent<SphereCollider>().radius * 10;
		// Rotate Entire Container
		//float tmpR = transform.rotation.y + ;
		//float tmpRUpdate = tmpR += 45;
		// rb.transform.rotation = new Vector3(0.0f, tmpR, 0.0f);
		 eulerAngleVelocity = new Vector3(0.0f, rotate, 0.0f);

		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
		rb.MoveRotation(rb.rotation * deltaRotation);

		//Vector3 toRotation = new Vector3(0.0f, tmpRUpdate, 0.0f);
		//transform.rotation = Quaternion.FromToRotation (Vector3.zero, toRotation);

		rotateText.text = "" + brb.GetComponent<SphereCollider>().radius;

		// Destroy the bullet after 2 seconds
		// Destroy(bullet, 2.0f);        
	}
}
