using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Party: Guest {

	[SerializeField]
	private float partyGuyHighLevel = 100;

	//How long Party Guy smokes
	[SerializeField]
	private float smokingTime = 4.0f;

	//Time it takes to roll
	[SerializeField]
	private float rollingTime = 2.0f;

	private NavMeshAgent agent;

	private bool turboMode = false;

	protected override void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		Move ();
		base.Start ();
	}

	protected override void Update ()
	{
		if (alcoolismLevel >= alcoolismLimit && !turboMode)
		{
			alcoolismRate *= 2;
			turboMode = true;
			Debug.Log ("Party Guy is drinking turbo mode");
		}

		if (alcoolismLevel >= partyGuyHighLevel && !state.Contains(GuestStates.High))
		{
			state.Clear ();
			state.Enqueue(GuestStates.High);
			Debug.Log ("Party Guy is Rolling");
			Invoke("GetHigh", rollingTime);
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

	void GetHigh()
	{
		//Make Smoke
		Debug.Log("Party Guy is getting High");
		Invoke("FinishSmoking", smokingTime);
	}

	void FinishSmoking()
	{
		Debug.Log ("Party Guy finished the joint");
		turboMode = false;
		state.Clear();
		ResetAlcolismLevel(40);
	}
}
