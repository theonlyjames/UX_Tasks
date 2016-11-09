using UnityEngine;
using System.Collections;

public class SphereOuterController : MonoBehaviour {
	private static ILogger logger = Debug.logger;
	
	public GameObject innerSphere;

	public Renderer rend;

	public Color lerpedColor = Color.white;

	private float enterPoint;

	private Material innerSphereMaterial;

	private float cursorDistance;

	void Start() {
		SetRenderer();
		innerSphereMaterial = innerSphere.GetComponent<Material> ();
	}

	void SetRenderer () {
		rend = innerSphere.GetComponent<Renderer>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Cursor")) {
			Debug.Log ("ontroggerenter");
			enterPoint = Vector3.Distance (transform.position, other.transform.position);
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag ("Cursor")) {
			Debug.logger.Log ("outer pos", transform.position);
			Debug.logger.Log ("inner pos", innerSphere.transform.position);
			Debug.logger.Log ("cursor dis", cursorDistance);
			cursorDistance = Vector3.Distance (transform.position, other.transform.position);
			lerpedColor = Color.Lerp(Color.green, Color.red, cursorDistance - innerSphere.transform.position.y);
			rend.material.color = lerpedColor;
		}
	}

//	void OnMouseEnter() {
//		rend.material.color = Color.green;
//	}

//	void OnMouseOver() {
//		rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
//	}

//	void OnMouseExit() {
//		rend.material.color = Color.white;
//	}
}
