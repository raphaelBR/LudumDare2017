using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : Guest {

	//After Puke resets alcolism level to this value 
	[SerializeField]
	private float AlcolismResetLevel = 10f;

	//Delay after which Friend pukes
	[SerializeField]
	private float pukeDelay = 10.0f;

	protected override void Start ()
	{
		alcoolismLimit = 50f;	//First alcolism level trigger for student
		alcoolismRate = 5f;		//How much student drinks each time
		thirstDelay = 5f; 		//How long Before Student wants to drink
		hungerDelay = 4f;		//How long Before Student wants to eat
		alcoolismLimit = 30f;
		alcoolismLevel = 0f;
		Move ();
		base.Start ();
	}

	protected override void Update ()
	{
		//Friend's alcolism reashed 30
		if (alcoolismLevel >= alcoolismLimit && !state.Contains (GuestStates.Sick)) {
			state.Clear ();
			state.Enqueue (GuestStates.Sick);
			sickBubble.enabled = true;
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
		

	void Puke()
	{
		state.Clear ();
		ResetAlcolismLevel (AlcolismResetLevel);
		ChangeLayer (0);
		base.Puke ();
	}

}
