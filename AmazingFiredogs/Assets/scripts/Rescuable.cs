﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescuable : MonoBehaviour {
	public int BuildingNum;

	void Start() {
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		RescueZone rescue = other.GetComponent<RescueZone>();
		if (rescue) {
			Destroy(gameObject);
			rescue.Rescue(BuildingNum);
		}
	}
}
