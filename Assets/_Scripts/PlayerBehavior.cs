using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerBehavior : MonoBehaviour {
	
	private NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				agent.SetDestination (hit.point);
			}
		}
	}
}
