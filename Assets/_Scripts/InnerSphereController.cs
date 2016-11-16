using UnityEngine;
using System.Collections;

public class InnerSphereController : MonoBehaviour {

	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Cursor")) {
			rend.material.color = Color.red; 
		}
	}
}
