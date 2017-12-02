using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detection : MonoBehaviour {

	public List<Transform> targets;
	[Header("Field Of View")]
	public bool canSee = true;
	public bool useRaycasts = true;
	[Range(0f, 360f)]
	public float visionAngle;
	[Range(0f, 200f)]
	public float visionRange;
	public UnityEvent onSee;

	[Header("Hearing Zone")]
	public bool canHear = true;
	[Range(0f, 200f)]
	public float hearingRange;
	public UnityEvent onHear;

	void Update () {
		if (canHear == true) {
			Listen ();
		}
		if (canSee == true) {
			Watch ();
		}
	}

	void Listen () {
		foreach (Transform target in targets) {
			float distance = Vector3.Distance (transform.position, target.position);
			if (distance <= hearingRange) {
				onHear.Invoke ();
				break;
			}
		}
	}

	void Watch () {
		foreach (Transform target in targets) {
			Vector3 direction = target.position - transform.position;
			float angle = Vector3.Angle (direction, Vector3.forward);
			float distance = Vector3.Distance (transform.position, target.position);
			if (angle <= (visionAngle / 2) && distance <= visionRange) {
				if (useRaycasts == true) {
					RaycastHit hit;
					Ray ray = new Ray (transform.position, direction);
					if (Physics.Raycast (ray, out hit)) {
						if (hit.collider.gameObject == target) {
							onSee.Invoke ();
							break;
						}
					}
				} else {
					onSee.Invoke ();
					break;
				}
			}
		}
	}
}
