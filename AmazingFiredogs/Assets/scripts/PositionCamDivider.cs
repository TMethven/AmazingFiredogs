using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCamDivider : MonoBehaviour {
	Camera cam;

	void Start() {
		cam = transform.parent.GetComponent<Camera>();
	}
	
	void Update() {
		float rightPos = cam.ScreenToWorldPoint(new Vector3(cam.pixelRect.xMax, 0, 0)).x;
		Vector3 oldPos = transform.position;
		transform.position = new Vector3(rightPos, oldPos.y, oldPos.z);
	}
}
