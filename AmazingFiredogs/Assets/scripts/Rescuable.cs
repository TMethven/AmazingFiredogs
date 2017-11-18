using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescuable : MonoBehaviour {
	public int BuildingNum;
	GameObject[] buildings;
	List<FireControllerScript> fires;
	FireControllerScript building2;
	public GameObject cookedPrefab;

	int burned = 0;

	void Start() {
		if (transform.position.x > 0) {
			BuildingNum = 2;
		} else {
			BuildingNum = 1;
		}

		buildings = GameObject.FindGameObjectsWithTag("Building");
		fires = new List<FireControllerScript>();
		foreach (GameObject building in buildings) {
			fires.Add(building.GetComponent<FireControllerScript>());
		}

	}

	void Update() {
		int fireLevel = 0;
		foreach (FireControllerScript fire in fires) {
			fireLevel += fire.checkFireLevel(transform.position);
		}

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
