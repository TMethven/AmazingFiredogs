using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour {

	void Start() {
		SpriteRenderer personSprite = transform.Find("Human").GetComponent<SpriteRenderer>();
		PlayerMove move = GetComponentInParent<PlayerMove>();
		Color tint;
		if (move.PlayerNum == 1) {
			tint = GlobalInput.DogColours[(int)GlobalInput.Player1DogType];
		} else {
			tint = GlobalInput.DogColours[(int)GlobalInput.Player2DogType];
		}
		personSprite.color = tint;

		Destroy(gameObject, 5);
	}
}
