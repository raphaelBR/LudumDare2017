using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Guest : MonoBehaviour {

	public enum GuestStates {Hungry, Thirsty, Sick, Fighting, High}

	public Queue<GuestStates> state = new Queue<GuestStates> ();

	protected EventManager eventmanager;

	[SerializeField]
	protected float waitingDelayMin = 10f;
	[SerializeField]
	protected float waitingDelayMax = 15f;

	protected EnemyManager manager;

	[SerializeField]
	protected float alcoolismLevel = 0f;
	[SerializeField]
	protected float alcoolismLimit = 0f;
	[SerializeField]
	protected float alcoolismRate = 5f;
	[SerializeField]
	protected float thirstDelay = 2f;
	[SerializeField]
	protected float hungerDelay = 2f;
	[SerializeField]
	protected float musicDelay = 2f;


	void Awake () {
		manager = FindObjectOfType<EnemyManager> ().GetComponent<EnemyManager> ();
		eventmanager = FindObjectOfType<EventManager> ().GetComponent<EventManager>();

		state.Enqueue (GuestStates.Thirsty);
	}

	protected virtual void Start () {
		StartCoroutine (ThirstTimer ());
		StartCoroutine (HungerTimer ());
	}

	protected virtual void Update () {
		if (state.Count != 0) {
			switch (state.Peek())
			{
			case GuestStates.Hungry:
				Hungry ();
				break;
			case GuestStates.Thirsty:
				Thirsty ();
				break;
			default:
				break;
			}
		}
	}

	protected virtual void DrinkAlcohol ()
	{
		alcoolismLevel += alcoolismRate;
		Debug.Log ("I drink / Alcolism level at " + alcoolismLevel);
	}

	protected virtual void Hungry ()
	{
		Debug.Log ("I eat");
		Satisfy ();
	}

	protected virtual void Thirsty ()
	{
		DrinkAlcohol();
		Satisfy ();
	}

	void Satisfy () {
		state.Dequeue ();
	}

	IEnumerator ThirstTimer ()
	{
		yield return new WaitForSeconds (thirstDelay);
		if (!state.Contains (GuestStates.Thirsty) && !state.Contains(GuestStates.Sick)
			&& !state.Contains(GuestStates.Fighting))
		{
			//Debug.Log ("Adding Thirsty State");
			state.Enqueue (GuestStates.Thirsty);
		}
		StartCoroutine (ThirstTimer ());
	}

	IEnumerator HungerTimer ()
	{
		yield return new WaitForSeconds (hungerDelay);
		if (!state.Contains (GuestStates.Hungry) && !state.Contains(GuestStates.Sick)
			&& !state.Contains(GuestStates.Fighting))
		{
			//Debug.Log ("Adding Hungry State");
			state.Enqueue (GuestStates.Hungry);
		}
		StartCoroutine (HungerTimer ());
	}

	protected void ResetAlcolismLevel(float newLevel)
	{
		Debug.Log ("Alcolism Level Reset to" + newLevel);
		alcoolismLevel = newLevel;
	}
}
