using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescuable : MonoBehaviour {
	public int BuildingNum;

	void Start() {
		if (transform.position.x > 0) {
			BuildingNum = 2;
		} else {
			BuildingNum = 1;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		RescueZone rescue = other.GetComponent<RescueZone>();
		if (rescue) {
			Destroy(gameObject);
			rescue.Rescue(BuildingNum);
		}
	}
}
