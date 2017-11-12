using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

	public Transform target;

	void Start() {
		
	}
	
	void FixedUpdate() {
		transform.position = Vector3.Lerp(transform.position, target.position + (Vector3.up * 4) - Vector3.forward * 10, 0.1f);
	}
}
