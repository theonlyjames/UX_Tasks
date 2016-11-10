using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Sphere outer controller.
/// 
/// This ended up being far more convoluted than it neeeded to be. 
/// I just wanted to get something quick and dirty to you guys. 
/// 
/// Lets Discuss when you have a chance. 
/// 
/// Instructions: 
/// 
/// Click anywhere on the screen with a mouse and the "Cursor" object will fly to the cursor. 
/// When the Cursor approaches the mouse and increase in drag is added until it stops within a threshold.
/// When it passes the threshold and you keep mousedown it will snap onto the mouse and manipulation of
/// the objects transform will be used instead of adding force to attract it. 
/// 
/// You can then use the "Cursor" object to pass through the translucent outer sphere (I could have just used the 
/// Sphere Collider here) but instead went this route. 
/// 
/// </summary>

public class SphereOuterController : MonoBehaviour {
	private static ILogger logger = Debug.logger;
	
	public GameObject innerSphere;
	public Renderer rend;
	public Color lerpedColor;
	public float threshold; // range [0, 1]

	// TEXT FIELDS
	public Text cursor3DPos;
	public Text innerSpherePos;
	public Text transitionDistText;
	public Text transformPosText;
	public Text sizeTransDeltaText;

	private float transitionDistance;
	private Vector3 cursorDistance;
	private Collider otherLocal;

	// Sphear Sizes to calculate edges
	// Outer
	private float outerSize;
	// Innner
	private float innerSize;
	private Vector3 inner3dPos;
	// Outer + Inner
	private float outerInnerDelta = 0;

	private float sizeTransDelta; // to init the ball white

	// Cursor Size
	private float cursorSize = 0;
	private Vector3 cursor3dPos;

	void Start() {
		rend = innerSphere.GetComponent<Renderer>();
		lerpedColor = Color.white;
		otherLocal = null;
		var col = GetComponent<SphereCollider> ();
		var outerRadius = col.radius;
		Debug.LogFormat ("Outter Rad: {0}", outerRadius);
	}

	void Update () {
		if (Input.GetMouseButton (0)) {
			logger.Log ("outerInnerDelta: ", outerInnerDelta);
		}
	}

	void OnTriggerEnter(Collider other) {
		logger.Log ("ontroggerenter");
		if (other.CompareTag ("Cursor")) {
		}
	}

	void CalcDistances () {
		cursor3dPos = new Vector3(otherLocal.transform.position.x, 
								  otherLocal.transform.position.y, 
								  otherLocal.transform.position.z);
		inner3dPos = new Vector3 (innerSphere.transform.position.x, 
								  innerSphere.transform.position.y, 
								  innerSphere.transform.position.z);
		transitionDistance = Vector3.Distance (cursor3dPos, inner3dPos);
	}

	void CalcSphereDelta() {
		// TODO: Switch calcultions of size
		// to calculating object radius.		

		// Calc size use x, y, or z because it's a sphear.
		outerSize = transform.localScale.x;
		innerSize = innerSphere.transform.localScale.x;
		outerInnerDelta = (outerSize - innerSize) / 2;

		// Cursor Size
		cursorSize = otherLocal.transform.localScale.x;

		sizeTransDelta = transitionDistance - outerInnerDelta + (cursorSize / 2); 
	}

	void SetColor () {
		lerpedColor = Color.Lerp(Color.blue, Color.white, sizeTransDelta );
		if (sizeTransDelta > threshold) {
			rend.material.color = lerpedColor;
		} else {
			rend.material.color = Color.red;
		}
	}

	void OnTriggerStay(Collider other) {
		otherLocal = other;
		if (other.CompareTag ("Cursor")) {
			CalcDistances ();
			SetColor ();
			CalcSphereDelta ();
			UILog ();
		}
	}

	void OnTriggerExit() {
		sizeTransDelta = 1;
		SetColor ();
	}
		
	void UILog() {
		// logger.Log("james was here");
		cursor3DPos.text = "" + cursor3dPos;
		transformPosText.text = "" + transform.position;
		innerSpherePos.text = "" + inner3dPos;
		transitionDistText.text = "" + transitionDistance;
		sizeTransDeltaText.text = "" + sizeTransDelta;
	}
}
