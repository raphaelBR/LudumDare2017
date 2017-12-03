using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Friend : Guest {

	private NavMeshAgent agent;

	//Delay after which Friend pukes
	[SerializeField]
	private float pukeDelay = 10.0f;

	protected override void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		Move ();
		base.Start ();
	}

	protected override void Update ()
	{
		//Friend's alcolism reashed 30
		if (alcoolismLevel >= alcoolismLimit && !state.Contains(GuestStates.Sick))
		{
			state.Clear();
			state.Enqueue (GuestStates.Sick);
			Debug.Log ("I m going to puke in " + pukeDelay);
			Invoke ("Puke", pukeDelay);

		}

		base.Update ();
	}

	protected override void Hungry ()
	{
		base.Hungry ();
	}

	protected override void Thirsty ()
	{
		base.Thirsty ();
	}

	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (Random.Range (waitingDelayMin, waitingDelayMax));
		Move ();
	}

	void Move ()
	{
		agent.SetDestination (manager.GiveDestination(agent.transform.position.y));
		StartCoroutine (Wait ());
	}

	void Puke()
	{
		Debug.Log (" Friend Puking");
		eventmanager.IncreaseMayhem (10);
		state.Clear ();
		ResetAlcolismLevel (10);
	}
}
