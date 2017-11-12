using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour {
	Grabbable grabbed;
	ParticleSystem particles;
    public GameObject building1;
    public GameObject building2;
    private BuildingOcupancyScript buildingOcupancyScript1;
    private BuildingOcupancyScript buildingOcupancyScript2;
    private FireControllerScript  fireControllerScript1;
    private FireControllerScript fireControllerScript2;

	AudioSource waterSound;

    void Start() {
		grabbed = GetComponent<Grabbable>();
		particles = GetComponentInChildren<ParticleSystem>();
        buildingOcupancyScript1 = building1.GetComponent<BuildingOcupancyScript>();
        buildingOcupancyScript2 = building2.GetComponent<BuildingOcupancyScript>();
        fireControllerScript1 = building1.GetComponent<FireControllerScript>();
        fireControllerScript2 = building2.GetComponent<FireControllerScript>();
		waterSound = GetComponent<AudioSource>();
    }
	
	void Update() {
        if(grabbed.IsGrabbed)
        {
            Vector2Int? buildingGridPos = buildingOcupancyScript1.worldToBuildingCoord(transform.position);
            fireControllerScript1.reduceFire(buildingGridPos);
            buildingGridPos = buildingOcupancyScript2.worldToBuildingCoord(transform.position);
            fireControllerScript2.reduceFire(buildingGridPos);
        }

		if (grabbed.IsGrabbed && !particles.isEmitting) {
			particles.Clear();
			particles.Play();
			waterSound.Play();
		}
		if (!grabbed.IsGrabbed && particles.isPlaying) {
			particles.Stop();
			waterSound.Stop();
		}
	}
}
