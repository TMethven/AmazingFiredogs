using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour {
	Grabbable grabbed;
	ParticleSystem particles;

	void Start() {
		grabbed = GetComponent<Grabbable>();
		particles = GetComponentInChildren<ParticleSystem>();
	}
	
	void Update() {
		if (grabbed.IsGrabbed && !particles.isEmitting) {
			particles.Clear();
			particles.Play();
		}
		if (!grabbed.IsGrabbed && particles.isPlaying) {
			particles.Stop();
		}
	}
}
