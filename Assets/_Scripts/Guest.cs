using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Guest : MonoBehaviour {

	public enum GuestStates {Hungry, Thirsty, Sick, Fighting, High}

	public Queue<GuestStates> state = new Queue<GuestStates> ();

	protected EventManager eventmanager;

	protected Animator anim;

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

	protected NavMeshAgent agent;


	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponentInChildren<Animator> ();
		anim.SetBool ("Moving", false);
		ChangeLayer (0);
		manager = FindObjectOfType<EnemyManager> ().GetComponent<EnemyManager> ();
		eventmanager = FindObjectOfType<EventManager> ().GetComponent<EventManager>();

		state.Enqueue (GuestStates.Thirsty);
	}

	protected virtual void Start () {
		StartCoroutine (ThirstTimer ());
		StartCoroutine (HungerTimer ());
	}

	protected virtual void Update () {
		if (agent.velocity == Vector3.zero) {
			anim.SetBool ("Moving", false);
		} else {
			anim.SetBool ("Moving", true);
		}

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
			Debug.Log ("Adding Thirsty State");
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
			Debug.Log ("Adding Hungry State");
			state.Enqueue (GuestStates.Hungry);
		}
		StartCoroutine (HungerTimer ());
	}

	protected void ResetAlcolismLevel(float newLevel)
	{
		Debug.Log ("Alcolism Level Reset to" + newLevel);
		alcoolismLevel = newLevel;
	}

	protected void Puke ()
	{
		eventmanager.MakePuke (transform);
	}

	protected void ChangeLayer (int id) {
		for (int i = 0; i < anim.layerCount; i++) {
			anim.SetLayerWeight (i, 0);
		}
		anim.SetLayerWeight (id, 1);
	}

}
