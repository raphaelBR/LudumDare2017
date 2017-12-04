using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party: Guest {

	//After Smoking weed resets alcolism level to this value 
	[SerializeField]
	private float AlcolismResetLevel = 10f;

	[SerializeField]
	private float partyGuyHighLevel = 100;

	//How long Party Guy smokes
	[SerializeField]
	private float smokingTime = 4.0f;

	//Time it takes to roll
	[SerializeField]
	private float rollingTime = 2.0f;


	private bool turboMode = false;

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		if (alcoolismLevel > 110)
			alcoolismLevel = 100;

		if (alcoolismLevel >= alcoolismLimit && !turboMode)
		{
			vodkaAlcoolismRate *= 2;
			beerAlcoolismRate *= 2;
			turboMode = true;
//			Debug.Log ("Party Guy is drinking turbo mode");
			ChangeLayer (1);
		}

		if (alcoolismLevel >= partyGuyHighLevel && !state.Contains(GuestStates.High))
		{
			state.Clear ();
			state.Enqueue(GuestStates.High);
			weedBubble.enabled = true;
//			Debug.Log ("Party Guy is Rolling");
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
	void GetHigh()
	{
		//Make Smoke
		weedBubble.enabled = false;
		weedParticles.gameObject.SetActive (true);
//		Debug.Log("Party Guy is getting High");
		Invoke("FinishSmoking", smokingTime);
		sound.clip = ganjaSound;
		sound.Play ();
	}

	void FinishSmoking()
	{
//		Debug.Log ("Party Guy finished the joint");
		weedParticles.gameObject.SetActive (false);
		turboMode = false;
		beerAlcoolismRate /= 2;
		vodkaAlcoolismRate /= 2;
		state.Clear();
		ResetAlcolismLevel(AlcolismResetLevel);
		ChangeLayer (0);
	}
}
