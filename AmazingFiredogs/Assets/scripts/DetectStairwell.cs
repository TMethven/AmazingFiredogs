using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectStairwell : MonoBehaviour {
	PlayerMove move;

	void Start() {
		move = GetComponent<PlayerMove>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		Stairwell stairwell = other.GetComponent<Stairwell>();
		if (stairwell) {
			move.SetStairwell(stairwell);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		Stairwell stairwell = other.GetComponent<Stairwell>();
		if (stairwell) {
			move.ExitStairwell(stairwell);
		}
	}

}
