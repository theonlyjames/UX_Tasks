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
		if (Input.GetKeyDown(KeyCode.Space)) {
			numObjects++;
			MakeNew ();
		}

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
		int i = 0;
		foreach (Transform child in transform) {
			i++;
			Vector3 pos = RandomCircle(center, 5.0f, i);
			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
			child.rotation = rot;
			child.position = pos;
		}
	}

	Vector3 RandomCircle ( Vector3 center ,   float radius, int i  ){
		float ang = 360 / numObjects * Mathf.PI / 180f;
		angleText.text = "" + ang;
		Vector3 pos;
		pos.x = center.x + Mathf.Sin(ang * i) * radius;
		pos.z = center.z + Mathf.Cos(ang * i) * radius;
		pos.y = center.y;
		return pos;
	}

}
