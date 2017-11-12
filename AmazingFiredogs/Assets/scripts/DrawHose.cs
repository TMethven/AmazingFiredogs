using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHose : MonoBehaviour {
	Vector3 connectedTo;
	LineRenderer line;

	void Start() {
		connectedTo = transform.Find("ConnectedTo").transform.position;
		line = GetComponent<LineRenderer>();
		line.SetPosition(0, connectedTo);
	}
	
	void Update() {
		line.SetPosition(1, transform.position);
	}
}
