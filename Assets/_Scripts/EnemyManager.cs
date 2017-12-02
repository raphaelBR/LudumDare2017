using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public enum Rooms {Garden, Kitchen, Dining, Bedroom, Toilets}

	public OptimisationPool friendPool;
	public OptimisationPool studentPool;
	public OptimisationPool partyPool;
	[Space(10f)]
	public float quantityIncrement = 1f;
	public float delayIncrement = 1f;
	[Space(10f)]
	public Transform gardenLimits;
	public Transform kitchenLimits;
	public Transform diningLimits;
	public Transform bedroomLimits;
	public Transform toiletsLimits;

	private float spawnQuantity = 1;
	private float spawnDelay = 5f;

	void Start () {
		StartCoroutine (Spawning ());
	}

	void Update () {
//		foreach (GameObject enemy in friendPool.actives) {
//			Debug.Log ("Friends " + enemy.name);
//		}
//		foreach (GameObject enemy in studentPool.actives) {
//			Debug.Log ("Student " + enemy.name);
//		}
//		foreach (GameObject enemy in partyPool.actives) {
//			Debug.Log ("Party " + enemy.name);
//		}
	}

	IEnumerator Spawning () {
		for (int i = 0; i < Mathf.RoundToInt(spawnQuantity); i++) {
			switch (Random.Range(0, 3)) {
			case 0:
				friendPool.Spawn ();
				break;
			case 1:
				studentPool.Spawn ();
				break;
			case 2:
				partyPool.Spawn ();
				break;
			default:
				break;
			}
		}
		yield return new WaitForSeconds (spawnDelay);
		spawnQuantity += quantityIncrement;
		spawnDelay += delayIncrement;
		StartCoroutine (Spawning ());
	}

	public Vector3 GiveDestination (float constantY) {
		Transform roomTransform;
		switch ((Rooms)Random.Range (0, System.Enum.GetValues (typeof(Rooms)).Length)) {
		case Rooms.Garden:
			roomTransform = gardenLimits;
			break;
		case Rooms.Kitchen:
			roomTransform = kitchenLimits;
			break;
		case Rooms.Dining:
			roomTransform = diningLimits;
			break;
		case Rooms.Bedroom:
			roomTransform = bedroomLimits;
			break;
		case Rooms.Toilets:
			roomTransform = toiletsLimits;
			break;
		default:
			roomTransform = gardenLimits;
			break;
		}
		return new Vector3 (Random.Range (roomTransform.position.x + roomTransform.lossyScale.x/2, roomTransform.position.x - roomTransform.lossyScale.x/2), constantY, Random.Range (roomTransform.position.z + roomTransform.lossyScale.z/2, roomTransform.position.z - roomTransform.lossyScale.z/2));
	}
}
