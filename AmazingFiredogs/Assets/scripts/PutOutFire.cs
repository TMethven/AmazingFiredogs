using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOutFire : MonoBehaviour {
	ParticleSystem system;
	List<ParticleCollisionEvent> collisions;

	public GameObject building1;
	public GameObject building2;
	private BuildingOcupancyScript buildingOcupancyScript1;
	private BuildingOcupancyScript buildingOcupancyScript2;
	private FireControllerScript  fireControllerScript1;
	private FireControllerScript fireControllerScript2;

	void Start() {
		system = GetComponent<ParticleSystem>();
		collisions = new List<ParticleCollisionEvent>();

		buildingOcupancyScript1 = building1.GetComponent<BuildingOcupancyScript>();
		buildingOcupancyScript2 = building2.GetComponent<BuildingOcupancyScript>();
		fireControllerScript1 = building1.GetComponent<FireControllerScript>();
		fireControllerScript2 = building2.GetComponent<FireControllerScript>();
	}

	void FixedUpdate() {
		PutOut(transform.position);
	}

	void OnParticleCollision(GameObject other) {
		ParticlePhysicsExtensions.GetCollisionEvents(system, other, collisions);

		foreach (ParticleCollisionEvent collision in collisions) {
			Vector3 pos;
			if (other.GetComponent<FireSpriteController>()) {
				Debug.Log("Hit fire?");
				pos = other.transform.position + Vector3.right + Vector3.up; // Because the fire origin is at the bottom left.
			} else {
				pos = collision.intersection;
			}
			PutOut(pos);
		}
		collisions.Clear();
	}

	void PutOut(Vector3 pos) {
		Vector2Int? buildingGridPos = buildingOcupancyScript1.worldToBuildingCoord(pos);
		fireControllerScript1.reduceFire(buildingGridPos);
		buildingGridPos = buildingOcupancyScript2.worldToBuildingCoord(pos);
		fireControllerScript2.reduceFire(buildingGridPos);
	}
}
