using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RescueZone : MonoBehaviour {

	int p1Score = 0;
	int p2Score = 0;

	TextMeshPro p1ScoreText;
	TextMeshPro p2ScoreText;

	void Start() {
		p1ScoreText = transform.Find("P1Score").GetComponent<TextMeshPro>();
		p2ScoreText = transform.Find("P2Score").GetComponent<TextMeshPro>();
	}
	
	void Update() {
		
	}

	public void Rescue(int buildingNum) {
		if (buildingNum == 1) {
			p1Score += 1;
			p1ScoreText.text = "" + p1Score;
		} else {
			p2Score += 1;
			p2ScoreText.text = "" + p2Score;
		}
	}
}
