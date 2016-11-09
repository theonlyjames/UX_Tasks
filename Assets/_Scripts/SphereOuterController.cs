using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereOuterController : MonoBehaviour {
	private static ILogger logger = Debug.logger;
	
	public GameObject innerSphere;
	public Renderer rend;
	public Color lerpedColor;
	public int threshold;

	public Text cursorPos;
	public Text onTriggerDist;
	public Text innerSpherePos;
	public Text transitionDistText;
	public Text otherTransformPosText;
	public Text outerInnerDeltaText;

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

	// Cursor Size
	private float cursorSize = 0;
	private Vector3 cursor3dPos;

	void Start() {
		rend = innerSphere.GetComponent<Renderer>();
		lerpedColor = Color.white;
		otherLocal = null;

		logger.Log ("outerInnerDelta: ", outerInnerDelta);
	}

	void Update () {
		if (Input.GetMouseButton (0)) {
			logger.Log ("outerInnerDelta: ", outerInnerDelta);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Cursor")) {
			logger.Log ("ontroggerenter");
		}
	}

	void CalcDistances () {
		// cursorDistance = Vector3.Distance (transform.position, otherLocal.transform.position);
		cursor3dPos = new Vector3(otherLocal.transform.position.x, otherLocal.transform.position.y, otherLocal.transform.position.z);
		inner3dPos = new Vector3 (innerSphere.transform.position.x, innerSphere.transform.position.y, innerSphere.transform.position.z);
		transitionDistance = Vector3.Distance (cursor3dPos, inner3dPos);
	}

	void CalcSphereDelta() {
		// Calc size use x, y, or z because it's a sphear.
		outerSize = transform.localScale.x;
		innerSize = innerSphere.transform.localScale.x;
		outerInnerDelta = (outerSize - innerSize) / 2;

		// Cursor Size
		cursorSize = otherLocal.transform.localScale.x;
	}

	void SetColor () {
		lerpedColor = Color.Lerp(Color.white, Color.blue, transitionDistance - outerInnerDelta + cursorSize );
		if (transitionDistance > 0) {
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

	void UILog() {
		otherTransformPosText.text = "other.transform.position: " + otherLocal.transform.position;
		cursorPos.text = "cursor3dpos: " + cursor3dPos;
		onTriggerDist.text = "transform.position: " + transform.position;
		innerSpherePos.text = "inner3dpos: " + inner3dPos;
		transitionDistText.text = "cursor3dPos -> inner3dPos: " + transitionDistance;
		float sizeTransDelta = transitionDistance - outerInnerDelta + cursorSize;
		outerInnerDeltaText.text = "transitionDistance - outerInnerDelta: " + sizeTransDelta;
	}
}
