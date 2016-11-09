using UnityEngine;
using System.Collections;

public class TestSphereController : MonoBehaviour {

	private Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
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
