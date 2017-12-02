using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public float quantityIncrement = 1f;
	public float delayIncrement = 1f;

	private float spawnQuantity = 1;
	private float spawnDelay = 5f;
	private OptimisationPool pool;

	void Start () {
		pool = GetComponent<OptimisationPool> ();
		StartCoroutine (Spawning ());
	}

	void Update () {
		
	}

	IEnumerator Spawning () {
		for (int i = 0; i < Mathf.RoundToInt(spawnQuantity); i++) {
			pool.Spawn ();
		}
		yield return new WaitForSeconds (spawnDelay);
		spawnQuantity += quantityIncrement;
		spawnDelay += delayIncrement;
		StartCoroutine (Spawning ());
	}
}
