using UnityEngine;
using System.Collections;

public class SphereOuterController : MonoBehaviour {
	
	public GameObject innerSphere;

	public Renderer rend;

	void SetRenderer () {
		rend = innerSphere.GetComponent<Renderer>();
	}

	void Start() {
		//rend = GetComponent<Renderer>();
		SetRenderer();
	}

	void OnMouseEnter() {
		rend.material.color = Color.green;
	}

	void OnMouseOver() {
		rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime;
	}

	void OnMouseExit() {
		rend.material.color = Color.white;
	}
}
