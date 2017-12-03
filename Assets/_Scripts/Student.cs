using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : Guest {

	//Second Alcolism limit of student
	[SerializeField]
	private float studentAlcolismLevel2 = 50;

	//After Puke resets alcolism level to this value 
	[SerializeField]
	private float AlcolismResetLevel = 20f;

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

	

	protected override void Start ()
	{

		Move ();
		base.Start ();
		//eventmanager.beerSource.actives.Count;
		//eventmanager.beerSource.actives [0].GetComponent<OptimisationItem> ().Despawn ();
	}

	protected override void Update ()
	{
		if(alcoolismLevel >= alcoolismLimit && !state.Contains(GuestStates.Fighting) && !fought)
		{
			state.Clear();
			state.Enqueue (GuestStates.Fighting);
//			Debug.Log ("Student Will start Fighting in " + fightDelay);
			madBubble.enabled = true;
			ChangeLayer (2);
			Invoke ("Fight", fightDelay);
		}

		if (alcoolismLevel >= studentAlcolismLevel2 && !state.Contains (GuestStates.Sick))
		{
			state.Clear ();
			state.Enqueue (GuestStates.Sick);
			sickBubble.enabled = true;
//			Debug.Log ("Student is going to puke in " + pukeDelay);
			ChangeLayer (1);
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

	void Fight()
	{
		//Increase level of mayhem
		madBubble.enabled = false;
		fightParticles.gameObject.SetActive (true);
//		Debug.Log ("Student Is Fighting for " + fightDuration + " Seconds");
		Invoke ("FinishFight", fightDuration);
	}

	void Puke()
	{
//		Debug.Log ("Student Puking");
		state.Clear ();
		ResetAlcolismLevel (AlcolismResetLevel);
		fought = false;
		ChangeLayer (0);
		base.Puke ();
	}

	void FinishFight()
	{
		fightParticles.gameObject.SetActive (false);
		fought = true;
		state.Clear ();
		ChangeLayer (0);
	}
}
