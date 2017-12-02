using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Guest : MonoBehaviour {

	public enum EnemyStates {Hungry, Thirsty}

	public Queue<EnemyStates> state = new Queue<EnemyStates> ();

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
	protected float alcoolismRate = 10f;
	[SerializeField]
	protected float thirstDelay = 2f;
	[SerializeField]
	protected float hungerDelay = 2f;
	[SerializeField]
	protected float musicDelay = 2f;


	void Awake () {
		manager = FindObjectOfType<EnemyManager> ().GetComponent<EnemyManager> ();
	}

	protected virtual void Start () {
		StartCoroutine (ThirstTimer ());
		StartCoroutine (HungerTimer ());
	}

	protected virtual void Update () {
		if (state.Count != 0) {
			switch (state.Peek()) {
			case EnemyStates.Hungry:
				Hungry ();
				break;
			case EnemyStates.Thirsty:
				Thirsty ();
				break;
			default:
				break;
			}
		}
	}

	protected virtual void DrinkAlcohol () {
		alcoolismLevel += alcoolismRate;
	}

	protected virtual void Hungry () {
		Debug.Log ("I eat");
		Satisfy ();
	}

	protected virtual void Thirsty () {
		Debug.Log ("I drink");
		Satisfy ();
	}

	void Satisfy () {
		state.Dequeue ();
	}

	IEnumerator ThirstTimer () {
//		Debug.Log ("Starting Thirsty");
		yield return new WaitForSeconds (thirstDelay);
		if (state.Contains (EnemyStates.Thirsty) == false) {
			state.Enqueue (EnemyStates.Thirsty);
		}
		StartCoroutine (ThirstTimer ());
	}

	IEnumerator HungerTimer () {
//		Debug.Log ("Starting Hungry");
		yield return new WaitForSeconds (hungerDelay);
		if (state.Contains (EnemyStates.Hungry) == false) {
			state.Enqueue (EnemyStates.Hungry);
		}
		StartCoroutine (HungerTimer ());
	}
}
