using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RescueZone : MonoBehaviour {

	int p1Score = 0;
	int p2Score = 0;

	TextMeshPro p1ScoreText;
	TextMeshPro p2ScoreText;

    private int winScore = 5    
    private GameObject winText;
    private GameObject P1GO, P2GO;

	void Start() {
		p1ScoreText = transform.Find("P1Score").GetComponent<TextMeshPro>();
		p2ScoreText = transform.Find("P2Score").GetComponent<TextMeshPro>();
        winText = GameObject.Find("Wintext");
        winText.SetActive(false);

        P1GO = GameObject.Find("Player1");
        P2GO = GameObject.Find("Player2");
	}
	
	void Update() {
		
	}

	public void Rescue(int buildingNum) {
		if (buildingNum == 1)
        {
			p1Score += 1;
			p1ScoreText.text = "" + p1Score;
            if(p1Score >= winScore)
            {
                winText.transform.position = new Vector3(P1GO.transform.position.x, P1GO.transform.position.y + 5f, -2f);
                winText.SetActive(true);
                Time.timeScale = 0f;
            }
		}
        else
        {
			p2Score += 1;
			p2ScoreText.text = "" + p2Score;

            if (p2Score >= winScore)
            {
                winText.transform.position = new Vector3(P2GO.transform.position.x, P2GO.transform.position.y + 5f, -2f);
                winText.SetActive(true);
                Time.timeScale = 0f;
            }
        }
	}
}
