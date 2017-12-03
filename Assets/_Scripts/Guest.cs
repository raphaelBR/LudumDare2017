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
	protected float alcoolismLimit = 100f;
	[SerializeField]
	protected float alcoolismRate = 5f;

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


	void Awake () {
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
			switch (state.Peek())
			{
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
		}
	}

	void DespawnBeer(){
		eventmanager.beerSource.actives[0].GetComponent<OptimisationItem> ().Despawn ();	
	}

	void DespawnJuice(){
		eventmanager.juiceSource.actives[0].GetComponent<OptimisationItem> ().Despawn ();
	}

	void DespawnVodka(){
		eventmanager.vodkaSource.actives[0].GetComponent<OptimisationItem> ().Despawn ();
	}

	protected virtual void DrinkAlcohol ()
	{
		if (eventmanager.beerSource.actives.Count > 0) {
			alcoolismLevel += alcoolismRate;
			//			Debug.Log ("I drink beer / Alcolism level at " + alcoolismLevel);
			Satisfy ();
			Invoke("DespawnBeer", 2);
		}else if(eventmanager.juiceSource.actives.Count > 0){
			//			Debug.Log ("I drink Juice / Alcolism level at " + alcoolismLevel);
			Satisfy ();
			Invoke("DespawnJuicer", 2);
		}else if(eventmanager.vodkaSource.actives.Count > 0){
			alcoolismLevel += alcoolismRate;
			Satisfy ();
			Invoke("DespawnVodka", 2);
//			Debug.Log ("I drink Vodka / Alcolism level at " + alcoolismLevel);
		}
		else {
//			Debug.Log ("There is no Drinks");
			unsatisfied = true;
			StartCoroutine (ToggleBubble (drinkBubble));
		}
	}


	void DespawnPizza(){
		eventmanager.pizzaSource.actives[0].GetComponent<OptimisationItem>().Despawn ();
	}

	void DespawnChips(){
		eventmanager.chipsSource.actives[0].GetComponent<OptimisationItem>().Despawn ();
	}


	protected virtual void EatFood()
	{
		if (eventmanager.pizzaSource.actives.Count > 0) {
			//			Debug.Log ("Eating Pizza");
			Satisfy ();
			Invoke(" DespawnPizza", 2);
		}
		else if(eventmanager.chipsSource.actives.Count > 0)
		{
			//			Debug.Log ("Eating Chips");
			Satisfy ();
			Invoke("DespawnChips", 2);
		}
		else {
			StartCoroutine (ToggleBubble (foodBubble));
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
		agent.SetDestination (manager.GiveDestination(agent.transform.position.y, room));
	}
		
	protected virtual void Thirsty ()
	{
		DrinkAlcohol();
	}

	void Satisfy () {
		
		unsatisfied = false;
		state.Dequeue ();
	}

	IEnumerator ThirstTimer ()
	{
		yield return new WaitForSeconds (thirstDelay);
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
		yield return new WaitForSeconds (hungerDelay);
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

	protected IEnumerator NoDrinks()
	{
		yield return new WaitForSeconds (1);
		eventmanager.IncreaseMayhem (noDrinksMayhemLevel);
		StartCoroutine (NoDrinks());
	}

	IEnumerator ToggleBubble (SpriteRenderer bubble) {
		bubble.enabled = true;
		yield return new WaitForSeconds (1f);
		bubble.enabled = false;
	}

}
