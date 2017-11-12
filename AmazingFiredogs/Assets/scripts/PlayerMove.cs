using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
	public int PlayerNum;
	int controllerNum = 0;

	public int Facing = 1; // 1 for right, -1 for left.
	public float JumpForce;
	public float AirControl = 0.2f;
	public float Speed = 3;

	float moveForce = 100;
	Rigidbody2D body;

	float noGroundTime = 0.1f; // Allow some leeway on player being grounded.
	float jumpTimer = 0; // Used to make sure the player can't jump then jump again immediately.

	public bool Grounded = false;

	SpriteRenderer sprite;

	public Transform[] RaycastPoints;

	public Sprite corgiSprite;
	public Sprite huskySprite;

	Stairwell stairwell = null;
	bool movedStairwell = false;

	GrabThrow grab;

	LayerMask groundedMask;

	void Start() {
		body = GetComponent<Rigidbody2D>();
		sprite = GetComponent<SpriteRenderer>();
		grab = GetComponent<GrabThrow>();
		groundedMask = LayerMask.GetMask(new string[] {"Default"});

		if (PlayerNum == 1) {
			controllerNum = GlobalInput.Player1Controller;
			if (controllerNum == -1) {
				controllerNum = 1; // Default controller number for debugging.
			}
		} else {
			controllerNum = GlobalInput.Player2Controller;
			if (controllerNum == -1) {
				controllerNum = 2; // Default controller number for debugging.
			}
		}

		Sprite selectedSprite = sprite.sprite;

		if (PlayerNum == 1) {
			if (GlobalInput.Player1DogType == GlobalInput.DogType.Corgi) {
				selectedSprite = corgiSprite;
			} else if (GlobalInput.Player1DogType == GlobalInput.DogType.Husky) {
				selectedSprite = huskySprite;
			}
		} else {
			if (GlobalInput.Player2DogType == GlobalInput.DogType.Corgi) {
				selectedSprite = corgiSprite;
			} else if (GlobalInput.Player2DogType == GlobalInput.DogType.Husky) {
				selectedSprite = huskySprite;
			}
		}

		this.sprite.sprite = selectedSprite;
	}

	void FixedUpdate() {
		float horiz = Input.GetAxis(GlobalInput.Horizontal[controllerNum]);
		float vert = Input.GetAxis(GlobalInput.Vertical[controllerNum]);

		CheckGrounded(horiz);
		CheckMovement(horiz);
		CheckJump();
		CheckVertical(vert);
		CheckGrab();

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
				if (Physics2D.Raycast(raycastPoint.position, Vector2.down, 0.02f, groundedMask)) {
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
			Facing = 1;
			sprite.flipX = true;
		}
		if (horiz < 0) {
			Facing = -1;
			sprite.flipX = false;
		}

		if (Grounded) {
			body.AddForce(new Vector2(horiz * moveForce, 0));
		} else {
			body.AddForce(new Vector2(horiz * moveForce * AirControl, 0));
		}
	}

	void CheckJump() {
		if (jumpTimer > 0) {
			jumpTimer -= Time.fixedDeltaTime;
		}
		if (Input.GetButton(GlobalInput.Fire[controllerNum])) {
			if (Grounded && jumpTimer <= 0) {
				Vector2 vel = body.velocity;
				vel.y = JumpForce;
				body.velocity = vel;
				jumpTimer = 0.2f;
			}		
		}
	}

	void CheckGrab() {
		if (Input.GetButtonDown(GlobalInput.Shield[controllerNum])) {
			grab.CheckGrab();
		}
	}

	void CheckVertical(float vert) {
		if (Mathf.Abs(vert) < 0.01) {
			movedStairwell = false;
		}
		
		if (stairwell && !movedStairwell) {
			if (vert > 0.1 && stairwell.stairwellAbove) {
				body.position = stairwell.stairwellAbove.position;
				movedStairwell = true;
			}
			if (vert < -0.1 && stairwell.stairwellBelow && !movedStairwell) {
				body.position = stairwell.stairwellBelow.position;
				movedStairwell = true;
			}
		}
	}


	// Stairwell stuff.
	public void SetStairwell(Stairwell stairwell) {
		this.stairwell = stairwell;
	}

	public void ExitStairwell(Stairwell stairwell) {
		if (stairwell == this.stairwell) {
			this.stairwell = null;
		}
	}
}
