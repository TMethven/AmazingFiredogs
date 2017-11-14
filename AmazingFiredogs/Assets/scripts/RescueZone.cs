using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RescueZone : MonoBehaviour {

	int p1Score = 0;
	int p2Score = 0;

	TextMeshPro p1ScoreText;
	TextMeshPro p2ScoreText;

	private int winScore = 1;    
    private GameObject winText;
    private GameObject P1GO, P2GO;
	float timeToRestart = 5;
	GameObject winner = null;

	void Start() {
		p1ScoreText = transform.Find("P1Score").GetComponent<TextMeshPro>();
		p2ScoreText = transform.Find("P2Score").GetComponent<TextMeshPro>();
        winText = GameObject.Find("Wintext");
        winText.SetActive(false);

        P1GO = GameObject.Find("Player1");
        P2GO = GameObject.Find("Player2");
	}
	
	void Update() {
		if (winner) {
			winText.transform.position = new Vector3(winner.transform.position.x, winner.transform.position.y + 5f, -2f);
			winText.SetActive(true);

			timeToRestart -= Time.deltaTime;
			if (timeToRestart < 0) {
				SceneManager.LoadScene(0);
			}
		}
	}

	public void Rescue(int buildingNum) {
		if (winner) {
			return;
		}


		if (buildingNum == 1)
        {
			p1Score += 1;
			p1ScoreText.text = "" + p1Score;
            if(p1Score >= winScore)
            {
				this.winner = P1GO;
            }
		}
        else
        {
			p2Score += 1;
			p2ScoreText.text = "" + p2Score;

            if (p2Score >= winScore)
            {
				this.winner = P2GO;
            }
        }
	}

}
