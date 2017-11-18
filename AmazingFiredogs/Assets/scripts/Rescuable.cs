using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescuable : MonoBehaviour {
	public int BuildingNum;
	GameObject[] buildings;
	List<FireControllerScript> fires;
	public GameObject cookedPrefab;

	int burned = 0;

	void Start() {
		buildings = GameObject.FindGameObjectsWithTag("Building");

		if (buildings.Length == 2) {
			if (transform.position.x > 0) {
				BuildingNum = 2;
			} else {
				BuildingNum = 1;
			}
		} else {
			Color tint;
			if (Random.value < 0.5) {
				BuildingNum = 1;
				tint = GlobalInput.DogColours[(int)GlobalInput.Player1DogType];
			} else {
				BuildingNum = 2;
				tint = GlobalInput.DogColours[(int)GlobalInput.Player2DogType];
			}
			Debug.Log(tint);
			GetComponent<SpriteRenderer>().color = tint;
		}

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
			if (rescue.Rescue(BuildingNum)) {
				Destroy(gameObject);
			}
		}
	}
}
