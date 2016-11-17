using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour {

	public Renderer rend;
	public Color lerpedColor;

	private int hoverState = 0;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		lerpedColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetColor () {
		lerpedColor = Color.Lerp(Color.blue, Color.white, hoverState );
		rend.material.color = lerpedColor;
	}

	void OnMouseEnter () {
		hoverState = 1;
		//SetColor ();
	}

	void OnMouseExit () {
		hoverState = 0;
		//SetColor ();
	}
}
