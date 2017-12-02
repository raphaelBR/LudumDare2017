using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Party: Guest {

	private NavMeshAgent agent;

	protected override void Start () {
		agent = GetComponent<NavMeshAgent> ();
		Move ();
		base.Start ();
	}

	protected override void Update () {
		base.Update ();
	}

	protected override void Hungry () {
		base.Hungry ();
	}

	protected override void Thirsty () {
		base.Thirsty ();
	}

	IEnumerator Wait () {
		yield return new WaitForSeconds (Random.Range (waitingDelayMin, waitingDelayMax));
		Move ();
	}

	void Move () {
		agent.SetDestination (manager.GiveDestination(agent.transform.position.y));
		StartCoroutine (Wait ());
	}
}
