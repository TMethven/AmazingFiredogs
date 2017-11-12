using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescuable : MonoBehaviour {
	public int BuildingNum;
	FireControllerScript building1;
	FireControllerScript building2;
	public GameObject cookedPrefab;

	int burned = 0;

	void Start() {
		if (transform.position.x > 0) {
			BuildingNum = 2;
		} else {
			BuildingNum = 1;
		}

		building1 = GameObject.Find("P1Building").GetComponent<FireControllerScript>();
		building2 = GameObject.Find("P2Building").GetComponent<FireControllerScript>();
	}

	void Update() {
		int fireLevel = building1.checkFireLevel(transform.position) + building2.checkFireLevel(transform.position);
		burned += fireLevel;
		if (burned > 1000) {
			GameObject.Instantiate(cookedPrefab, transform.position, transform.rotation);
			GameObject.Destroy(gameObject);
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
