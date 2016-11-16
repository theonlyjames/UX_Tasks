using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChildContainerController : MonoBehaviour {
	public GameObject itemPrefab;
	public Transform itemSpawn;
	public Vector3 eulerAngleVelocity;

	public Text positionText;
	public Text angleText;

	private Rigidbody rb;

	public int numObjects = 0;

	void Update() {
//			var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
//			var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
//
//		    rb = GetComponent<Rigidbody> ();
//	
//			transform.Rotate(0, x, 0);
//			transform.Translate(0, 0, z);

		if (Input.GetKeyDown(KeyCode.Space)) {
			// Fire();
			numObjects++;
			MakeNew ();
		}

	}


	void Start() {
	}

	void MakeNew() {
		Vector3 center = transform.position;
		Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center);

		var item = (GameObject)Instantiate(itemPrefab, center, rot);
		item.transform.parent = transform;
		UpdatePosition ();
	}

	void UpdatePosition() {
		Vector3 center = transform.position;
//		for (int i = 0; i < numObjects; i++){
//			Vector3 pos = RandomCircle(center, 5.0f, i);
//			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
//			
//			positionText.text = "" + pos;
//		}
		int i = 0;
		foreach (Transform child in transform) {
			i++;
			Vector3 pos = RandomCircle(center, 5.0f, i);
			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
			child.rotation = rot;
			child.position = pos;
			Debug.Log(child.position);
		}
	}

	Vector3 RandomCircle ( Vector3 center ,   float radius, int i  ){
		float ang = 360 / numObjects * Mathf.PI / 180f;
		angleText.text = "" + ang;
		Vector3 pos;
		//pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.x = center.x + Mathf.Sin(ang * i) * radius;
		pos.z = center.z + Mathf.Cos(ang * i) * radius;
		pos.y = center.y;
		//pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		//pos.z = center.z + radius * Mathf.Cos(ang * numObjects);
		return pos;
	}

//	void Fire() {
//		// Create the Bullet from the Bullet Prefab
//		var bullet = (GameObject)Instantiate(
//			bulletPrefab,
//			bulletSpawn.position,
//			bulletSpawn.rotation
//		);
//
//		bullet.transform.parent = transform;
//
//		var brb = bullet.GetComponent<Rigidbody> ();
//
//		bullet.transform.position = new Vector3 (0.0f, 0.0f, 2);
//
//		// number of objects
//		numberOfObjects = numberOfObjects += 1;
//		// float rotateAngle = 360 / numberOfObjects;
//		//float rotate = (numberOfObjects += brb.GetComponent<SphereCollider>().radius);
//		float rotate = brb.GetComponent<SphereCollider>().radius * 10;
//		// Rotate Entire Container
//		//float tmpR = transform.rotation.y + ;
//		//float tmpRUpdate = tmpR += 45;
//		// rb.transform.rotation = new Vector3(0.0f, tmpR, 0.0f); //		eulerAngleVelocity = new Vector3(0.0f, rotate, 0.0f);
//
//		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
//		rb.MoveRotation(rb.rotation * deltaRotation);
//
//		//Vector3 toRotation = new Vector3(0.0f, tmpRUpdate, 0.0f);
//		//transform.rotation = Quaternion.FromToRotation (Vector3.zero, toRotation);
//
//		rotateText.text = "" + brb.GetComponent<SphereCollider>().radius;
//
//		// Destroy the bullet after 2 seconds
//		// Destroy(bullet, 2.0f);        
//	}

}
