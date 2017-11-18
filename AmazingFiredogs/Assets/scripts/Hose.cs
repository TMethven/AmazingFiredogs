using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour {
	Grabbable grabbed;
	ParticleSystem particles;

	AudioSource waterSound;

    void Start() {
		grabbed = GetComponent<Grabbable>();
		particles = GetComponentInChildren<ParticleSystem>();
		waterSound = GetComponent<AudioSource>();
    }
	
	void FixedUpdate() {
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
