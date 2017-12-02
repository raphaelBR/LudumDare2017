using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public float destinationMinX = -5f;
	public float destinationMaxX = 5f;
	public float destinationMinZ = -5f;
	public float destinationMaxZ = 5f;

	public float waitingDelayMin = 3f;
	public float waitingDelayMax = 3f;



	private NavMeshAgent agent;

	void Start () {
		
	}

	void Update () {
		
	}

	IEnumerator Wait () {
		yield return new WaitForSeconds (Random.Range (waitingDelayMin, waitingDelayMax));
		Move ();
	}

	void Move () {
		agent.SetDestination (new Vector3 (Random.Range (destinationMinX, destinationMaxX), transform.position.y, Random.Range (destinationMinZ, destinationMaxZ)));
	}
}
