using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabThrow : MonoBehaviour {
	Rigidbody2D body;
	PlayerMove move;

	public GameObject Grabbed;
	Rigidbody2D grabbedBody;

	float carryOffsetX = 4f;
	float carryOffsetY = 1f;

	float grabRange = 3;

	FixedJoint2D grabJoint;

	void Start() {
		body = GetComponent<Rigidbody2D>();
		move = GetComponent<PlayerMove>();
		grabJoint = GetComponent<FixedJoint2D>();
		grabJoint.enabled = false;
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
			Debug.Log("Facing: " + move.Facing);
			grabJoint.anchor = new Vector2(carryOffsetX * move.Facing, carryOffsetY);
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
					grabbable.IsGrabbed = true;
					grabbedBody = near.GetComponent<Rigidbody2D>();
					grabbedBody.angularVelocity = 0;
					grabJoint.enabled = true;
					grabJoint.connectedBody = grabbedBody;
					grabJoint.autoConfigureConnectedAnchor = false;
					grabJoint.anchor = new Vector2(carryOffsetX, carryOffsetY);
					grabJoint.connectedAnchor = Vector2.zero;
					break;
				}
			}
		}
	}

	void Throw() {
		grabJoint.enabled = false;
		grabbedBody.velocity = body.velocity;

		Grabbed.GetComponent<Grabbable>().IsGrabbed = false;
		grabbedBody = null;
		Grabbed = null;
	}
}
