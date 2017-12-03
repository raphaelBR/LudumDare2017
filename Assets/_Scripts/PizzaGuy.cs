using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PizzaGuy : MonoBehaviour {

	public Transform door;
	public Transform home;

	private NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}

	public void GoHome () {
		agent.SetDestination (new Vector3 (home.position.x, transform.position.y, home.position.z));
	}

	public void Deliver () {
		agent.SetDestination (new Vector3 (door.position.x, transform.position.y, door.position.z));
	}
}
