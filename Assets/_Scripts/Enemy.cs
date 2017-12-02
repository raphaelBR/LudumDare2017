using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

	public enum EnemyStates {Waiting, Moving}

	public float destinationMinX = -5f;
	public float destinationMaxX = 5f;
	public float destinationMinZ = -5f;
	public float destinationMaxZ = 5f;

	public List<Transform> targets;

	public float waitingDelayMin = 3f;
	public float waitingDelayMax = 3f;

	public EnemyStates state = EnemyStates.Moving;

	private NavMeshAgent agent;
	private EnemyManager manager;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		Move ();
	}

	void Update () {
		switch (state) {
		case EnemyStates.Moving:
			if (agent.remainingDistance <= agent.stoppingDistance) {
				StartCoroutine (Wait ());
				state = EnemyStates.Waiting;
			}
			break;
		case EnemyStates.Waiting:
			break;
		default:
			break;
		}
	}

	IEnumerator Wait () {
		yield return new WaitForSeconds (Random.Range (waitingDelayMin, waitingDelayMax));
		Move ();
	}

	void Move () {
		agent.SetDestination (new Vector3 (Random.Range (destinationMinX, destinationMaxX), transform.position.y, Random.Range (destinationMinZ, destinationMaxZ)));
		state = EnemyStates.Moving;
	}
}
