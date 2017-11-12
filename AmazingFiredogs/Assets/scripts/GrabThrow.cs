using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabThrow : MonoBehaviour {
	Rigidbody2D body;
	PlayerMove move;

	public GameObject Grabbed;
	Rigidbody2D grabbedBody;

	float carryOffsetX = 5f;
	float carryOffsetY = 2f;

	float grabRange = 4;

	void Start() {
		body = GetComponent<Rigidbody2D>();
		move = GetComponent<PlayerMove>();
	}
	
	public void CheckGrab() {
		if (Grabbed) {
			Throw();
		} else {
			Grab();
		}
	}

	void Update() {
		if (Grabbed) {
			Grabbed.transform.localPosition = new Vector2(carryOffsetX * move.Facing, carryOffsetY);
			float angle = 90 * (1 - move.Facing);
			Grabbed.transform.rotation = Quaternion.Euler(0, angle, 0);
		}
	}

	void Grab() {
		foreach (Collider2D near in Physics2D.OverlapCircleAll(body.position, grabRange)) {
			if (near.gameObject == gameObject) {
				continue;
			}
			Grabbable grabbable = near.GetComponent<Grabbable>();
			if (grabbable && !grabbable.IsGrabbed) {
				Debug.Log("Grabbing " + grabbable);
				GrabThrow otherGrabThrow = near.GetComponent<GrabThrow>();
				if (!otherGrabThrow || otherGrabThrow.Grabbed != gameObject) {
					Grabbed = near.gameObject;
					Grabbed.transform.parent = transform;
					Grabbed.transform.localPosition = new Vector2(carryOffsetX * move.Facing, carryOffsetY);

					grabbable.IsGrabbed = true;
					grabbedBody = near.GetComponent<Rigidbody2D>();
					grabbedBody.angularVelocity = 0;
					grabbedBody.simulated = false;
					break;
				}
			}
		}
	}

	void Throw() {
		grabbedBody.velocity = body.velocity;

		Grabbed.GetComponent<Grabbable>().IsGrabbed = false;
		grabbedBody.simulated = true;
		grabbedBody = null;
		Grabbed.transform.parent = null;
		Grabbed = null;
	}
}
