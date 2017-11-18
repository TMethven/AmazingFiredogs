using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOutFire : MonoBehaviour {
	ParticleSystem system;
	List<ParticleCollisionEvent> collisions;

	private GameObject[] buildings;
	private List<BuildingOcupancyScript> buildingOccupancies;
	private List<FireControllerScript> fireControllers;

	void Start() {
		system = GetComponent<ParticleSystem>();
		collisions = new List<ParticleCollisionEvent>();

		buildings = GameObject.FindGameObjectsWithTag("Building");
		buildingOccupancies = new List<BuildingOcupancyScript>();
		fireControllers = new List<FireControllerScript>();
		foreach (GameObject building in buildings) {
			buildingOccupancies.Add(building.GetComponent<BuildingOcupancyScript>());
			fireControllers.Add(building.GetComponent<FireControllerScript>());
		}
	}

	void FixedUpdate() {
		PutOut(transform.position);
	}

	void OnParticleCollision(GameObject other) {
		ParticlePhysicsExtensions.GetCollisionEvents(system, other, collisions);

		foreach (ParticleCollisionEvent collision in collisions) {
			Vector3 pos;
			if (other.GetComponent<FireSpriteController>()) {
				pos = other.transform.position + Vector3.right + Vector3.up; // Because the fire origin is at the bottom left.
			} else {
				pos = collision.intersection;
			}
			PutOut(pos);
		}
		collisions.Clear();
	}

	void PutOut(Vector3 pos) {
		for(int i = 0; i < buildingOccupancies.Count; i++) {
			BuildingOcupancyScript occupancy = buildingOccupancies[i];
			FireControllerScript fire = fireControllers[i];
			Vector2Int? buildingPos = occupancy.worldToBuildingCoord(pos);
			fire.reduceFire(buildingPos);

		}
	}
}
