using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public enum Rooms {Kitchen, Dining, Bedroom, Table, Null}

	public OptimisationPool friendPool;
	public OptimisationPool studentPool;
	public OptimisationPool partyPool;
	[Space(10f)]
	public float spawnQuantity = 1f;
	public float spawnDelay = 3f;
	[Space(5f)]
	public float quantityIncrement = 1f;
	public float delayIncrement = 1f;
	public int spawnMaximum = 50;
	[Space(10f)]
	public Transform kitchenLimits;
	public Transform diningLimits;
	public Transform bedroomLimits;
	public Transform tableLimits;

	public AudioSource background;

	void Awake () {
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
//		Debug.Log(friendPool.actives.Count + partyPool.actives.Count + studentPool.actives.Count);
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
		if (friendPool.actives.Count + partyPool.actives.Count + studentPool.actives.Count >= spawnMaximum) {
			spawnQuantity = 1;
			spawnDelay += delayIncrement * 2;
		}
		background.volume = Mathf.Clamp((friendPool.actives.Count + partyPool.actives.Count + studentPool.actives.Count) / (float)spawnMaximum, 0f, 1f);
		StartCoroutine (Spawning ());
	}

	public Vector3 GiveDestination (float constantY, Rooms room = Rooms.Null) {
		if (room == Rooms.Null) {
			room = (Rooms)Random.Range (0, System.Enum.GetValues (typeof(Rooms)).Length - 2);
		}
		Transform roomTransform;
		switch (room) {
		case Rooms.Kitchen:
			roomTransform = kitchenLimits;
			break;
		case Rooms.Dining:
			roomTransform = diningLimits;
			break;
		case Rooms.Bedroom:
			roomTransform = bedroomLimits;
			break;
		case Rooms.Table:
			roomTransform = tableLimits;
			break;
		default:
			roomTransform = tableLimits;
			break;
		}
		return new Vector3 (Random.Range (roomTransform.position.x + roomTransform.lossyScale.x / 2, roomTransform.position.x - roomTransform.lossyScale.x / 2), 	constantY, Random.Range (roomTransform.position.z + roomTransform.lossyScale.z / 2, roomTransform.position.z - roomTransform.lossyScale.z / 2));
	}
}
