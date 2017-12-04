using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

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
	protected AudioClip pukeSound;
	[SerializeField]
	protected AudioClip ganjaSound;
	[SerializeField]
	protected AudioClip fightSound;
	[SerializeField]
	protected AudioClip madSound;

	[SerializeField]
	protected float alcoolismLevel = 0f;
	[SerializeField]
	protected float alcoolismLimit = 100f;
	[SerializeField]
	protected float beerAlcoolismRate = 5f;
	[SerializeField]
	protected float vodkaAlcoolismRate = 8f;

	[SerializeField]
	protected float noDrinksMayhemLevel = 0.1f;

	[SerializeField]
	protected float thirstDelay = 2f;

	[SerializeField]
	protected float hungerDelay = 2f;

	[SerializeField]
	protected float musicDelay = 2f;

	protected NavMeshAgent agent;

	[SerializeField]
	protected ParticleSystem fightParticles;
	[SerializeField]
	protected ParticleSystem weedParticles;

	[SerializeField]
	protected SpriteRenderer sickBubble;
	[SerializeField]
	protected SpriteRenderer cleanBubble;
	[SerializeField]
	protected SpriteRenderer drinkBubble;
	[SerializeField]
	protected SpriteRenderer foodBubble;
	[SerializeField]
	protected SpriteRenderer weedBubble;
	[SerializeField]
	protected SpriteRenderer madBubble;

	[SerializeField]
	protected bool unsatisfied;

	[SerializeField]
	protected float mayhemIncreaseValue = 0.2f;
	private float timer;
	protected AudioSource sound;


	void Awake () {
		sound = GetComponent<AudioSource> ();
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponentInChildren<Animator> ();
		anim.SetBool ("Moving", false);
		ChangeLayer (0);
		manager = FindObjectOfType<EnemyManager> ().GetComponent<EnemyManager> ();
		eventmanager = FindObjectOfType<EventManager> ().GetComponent<EventManager>();

		state.Enqueue (GuestStates.Thirsty);

		unsatisfied = true;
	}

	protected virtual void Start () {
		StartCoroutine (ThirstTimer ());
		StartCoroutine (HungerTimer ());
		agent.SetDestination (manager.GiveDestination (agent.transform.position.y));
	}

	protected virtual void Update () {
		
		timer += Time.deltaTime;

		if ( unsatisfied && timer >= 1) {
//			Debug.Log ("Increased mayhem level");
			eventmanager.IncreaseMayhem (mayhemIncreaseValue);
			timer = 0;
		}

		if (agent.velocity == Vector3.zero) {
			anim.SetBool ("Moving", false);
		} else {
			anim.SetBool ("Moving", true);
		}

		if (state.Count != 0) {
			switch (state.Peek ()) {
			case GuestStates.Hungry:
				Hungry ();
				Move (EnemyManager.Rooms.Kitchen);
				break;
			case GuestStates.Thirsty:
				Move (EnemyManager.Rooms.Dining);
				Thirsty ();
				break;
			case GuestStates.Sick:
				Move (EnemyManager.Rooms.Bedroom);
				break;
			default:
				Move ();
				break;
			}
		} else {
			Move ();
		}
	}

	protected virtual void DrinkAlcohol ()
	{
		if (eventmanager.beerSource.actives.Count > 0) {
			alcoolismLevel += beerAlcoolismRate;
			//Debug.Log ("I drink beer / Alcolism level at " + alcoolismLevel);
			Satisfy ();
			eventmanager.beerSource.actives[0].GetComponent<AutoDestruction> ().SelfDestruct ();
			eventmanager.beerSource.actives[0].GetComponent<OptimisationItem> ().Despawn ();		
		}else if(eventmanager.juiceSource.actives.Count > 0){
			//Debug.Log ("I drink Juice / Alcolism level at " + alcoolismLevel);
			eventmanager.IncreaseMayhem (mayhemIncreaseValue / 2);
			Satisfy ();
			eventmanager.juiceSource.actives[0].GetComponent<AutoDestruction> ().SelfDestruct ();	
			eventmanager.juiceSource.actives[0].GetComponent<OptimisationItem> ().Despawn ();	
		}else if(eventmanager.vodkaSource.actives.Count > 0){
			alcoolismLevel += vodkaAlcoolismRate;
			Satisfy ();
			eventmanager.vodkaSource.actives[0].GetComponent<AutoDestruction> ().SelfDestruct ();	
			eventmanager.vodkaSource.actives[0].GetComponent<OptimisationItem> ().Despawn ();	
//			Debug.Log ("I drink Vodka / Alcolism level at " + alcoolismLevel);
		}
		else {
//			Debug.Log ("There is no Drinks");
			unsatisfied = true;
			drinkBubble.enabled = true;
		}
	}

	protected virtual void EatFood()
	{
		if (eventmanager.pizzaSource.actives.Count > 0) {
			//			Debug.Log ("Eating Pizza");
			Satisfy ();
			eventmanager.pizzaSource.actives[0].GetComponent<AutoDestruction> ().SelfDestruct ();	
			eventmanager.pizzaSource.actives[0].GetComponent<OptimisationItem> ().Despawn ();
		}
		else if(eventmanager.chipsSource.actives.Count > 0)
		{
			//			Debug.Log ("Eating Chips");
			Satisfy ();
			eventmanager.chipsSource.actives[0].GetComponent<AutoDestruction> ().SelfDestruct ();
			eventmanager.chipsSource.actives[0].GetComponent<OptimisationItem> ().Despawn ();		
		}
		else {
			foodBubble.enabled = true;
			unsatisfied = true;
//			Debug.Log ("There is no food");
		}
	}

	protected virtual void Hungry ()
	{
		EatFood ();
	}
	protected virtual void Move (EnemyManager.Rooms room = EnemyManager.Rooms.Null)
	{
		if (Vector3.Distance(agent.destination, transform.position) <= 3f) {
			agent.SetDestination (manager.GiveDestination (agent.transform.position.y, room));
		}
	}
		
	protected virtual void Thirsty ()
	{
		DrinkAlcohol();
	}

	void Satisfy () {
		foodBubble.enabled = false;
		drinkBubble.enabled = false;
		
		unsatisfied = false;
		state.Dequeue ();
	}

	IEnumerator ThirstTimer ()
	{
		yield return new WaitForSeconds (Random.Range(thirstDelay * 0.25f, thirstDelay * 2f));
		if (!state.Contains (GuestStates.Thirsty) && !state.Contains(GuestStates.Sick)
			&& !state.Contains(GuestStates.Fighting))
		{
//			Debug.Log ("Adding Thirsty State");
			state.Enqueue (GuestStates.Thirsty);
		}
		StartCoroutine (ThirstTimer ());
	}

	IEnumerator HungerTimer ()
	{
		yield return new WaitForSeconds (Random.Range(hungerDelay * 0.25f, hungerDelay * 2f));
		if (!state.Contains (GuestStates.Hungry) && !state.Contains(GuestStates.Sick)
			&& !state.Contains(GuestStates.Fighting))
		{
//			Debug.Log ("Adding Hungry State");
			state.Enqueue (GuestStates.Hungry);
		}
		StartCoroutine (HungerTimer ());
	}

	protected void ResetAlcolismLevel(float newLevel)
	{
//		Debug.Log ("Alcolism Level Reset to" + newLevel);
		alcoolismLevel = newLevel;
	}

	protected void Puke ()
	{
		sound.clip = pukeSound;
		sound.Play ();
		eventmanager.MakePuke (transform);
		sickBubble.enabled = false;
		StartCoroutine (ToggleBubble (cleanBubble));
	}

	protected void ChangeLayer (int id) {
		for (int i = 0; i < anim.layerCount; i++) {
			anim.SetLayerWeight (i, 0);
		}
		anim.SetLayerWeight (id, 1);
	}

	IEnumerator ToggleBubble (SpriteRenderer bubble) {
		bubble.enabled = true;
		yield return new WaitForSeconds (1f);
		bubble.enabled = false;
	}

}
