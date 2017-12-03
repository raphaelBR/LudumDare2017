using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flipper : MonoBehaviour {

	private SpriteRenderer spr;
	private NavMeshAgent agent;

	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		agent = transform.parent.gameObject.GetComponent<NavMeshAgent> ();
	}

	void Update () {
		if (agent.desiredVelocity != Vector3.zero) {
			if (agent.desiredVelocity.x > 0f) {
				spr.flipX = true;
			} else {
				spr.flipX = false;
			}
		}
	}
}
