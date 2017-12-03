using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Student : Guest {

	//Second Alcolism limit of student
	[SerializeField]
	private float studentAlcolismLevel2 = 100;

	//Delay Before student starts puking
	[SerializeField]
	private float pukeDelay = 3.0f;

	//Delay before students Starts fighting
	[SerializeField]
	private float fightDelay = 3.0f;

	//Fight Duration
	[SerializeField]
	private float fightDuration = 5.0f;

	private bool fought = false;
	private NavMeshAgent agent;

	protected override void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		Move ();
		base.Start ();
	}

	protected override void Update ()
	{
		if(alcoolismLevel >= alcoolismLimit && !state.Contains(GuestStates.Fighting) && !fought)
		{
			state.Clear();
			state.Enqueue (GuestStates.Fighting);
			Debug.Log ("Student Will start Fighting in " + fightDelay);
			Invoke ("Fight", fightDelay);
		}

		if (alcoolismLevel >= studentAlcolismLevel2 && !state.Contains (GuestStates.Sick))
		{
			state.Clear ();
			state.Enqueue (GuestStates.Sick);
			Debug.Log ("Student is going to puke in " + pukeDelay);
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

	void Fight()
	{
		//Increase level of mayhem
		Debug.Log ("Student Is Fighting for " + fightDuration + " Seconds");
		Invoke ("FinishFight", fightDuration);
	}

	void Puke()
	{
		Debug.Log ("Student Puking");
		state.Clear ();
		ResetAlcolismLevel (20);
		fought = false;
	}

	void FinishFight()
	{
		fought = true;
		state.Clear ();
	}
}
