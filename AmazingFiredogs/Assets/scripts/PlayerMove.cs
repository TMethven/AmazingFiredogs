using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public int PlayerNum;
	public int Facing = 1; // 1 for right, -1 for left.
	public float JumpForce;

	public float Speed = 3;
	float moveForce = 100;
	float airControl = 0.05f;
	Rigidbody2D body;

	float noGroundTime = 0.1f; // Allow some leeway on player being grounded.
	float jumpTimer = 0; // Used to make sure the player can't jump then jump again immediately.

	public bool Grounded = false;

	SpriteRenderer sprite;

	public Transform[] RaycastPoints;

	void Start() {
		body = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate() {
		float horiz = Input.GetAxis(GlobalInput.Horizontal[PlayerNum]);

		CheckGrounded(horiz);
		CheckMovement(horiz);
		CheckJump();

		if (Mathf.Abs(body.velocity.x) > Speed) {
			body.velocity = new Vector2(Speed * Mathf.Sign(body.velocity.x), body.velocity.y);
		}
	}

	void CheckGrounded(float horiz) {
		if (noGroundTime > 0) {
			Grounded = false;
			noGroundTime -= Time.fixedDeltaTime;
		} else {
			Grounded = false;
			foreach(Transform raycastPoint in RaycastPoints) {
				if (Physics2D.Raycast(raycastPoint.position, Vector2.down, 0.02f)) {
					Grounded = true;
				}
			}
		}

		// Stop the player quickly if they're not trying to move and are grounded.
		if (Grounded && Mathf.Abs(horiz) < 0.1f) {
			body.velocity = new Vector2(0, body.velocity.y);
		}

	}

	void CheckMovement(float horiz) {
		if (horiz > 0) {
			sprite.flipX = true;
		}
		if (horiz < 0) {
			sprite.flipX = false;
		}

		if (Grounded) {
			body.AddForce(new Vector2(horiz * moveForce, 0));
		} else {
			body.AddForce(new Vector2(horiz * moveForce * airControl, 0));
		}
	}

	void CheckJump() {
		if (jumpTimer > 0) {
			jumpTimer -= Time.fixedDeltaTime;
		}
		if (Input.GetButton(GlobalInput.Fire[PlayerNum])) {
			if (Grounded && jumpTimer <= 0) {
				Vector2 vel = body.velocity;
				vel.y = JumpForce;
				body.velocity = vel;
				jumpTimer = 0.2f;
			}		
		}
	}
}
