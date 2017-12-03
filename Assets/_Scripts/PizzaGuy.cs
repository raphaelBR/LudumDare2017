using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PizzaGuy : MonoBehaviour {

	public Transform door;
	public Transform home;

	private NavMeshAgent agent;

	private Animator anim;

	public float waitDelay = 15f;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponentInChildren<Animator> ();
		GoHome ();
	}

	void Update() {
		if (agent.velocity == Vector3.zero) {
			anim.SetBool ("Moving", false);
		} else {
			anim.SetBool ("Moving", true);
		}
	}

	public void GoHome () {
		agent.SetDestination (new Vector3 (home.position.x, transform.position.y, home.position.z));
	}

	public void Deliver () {
		agent.SetDestination (new Vector3 (door.position.x, transform.position.y, door.position.z));
		Invoke ("GoHome", waitDelay);
	}
}
