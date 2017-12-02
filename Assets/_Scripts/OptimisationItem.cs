using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimisationItem : MonoBehaviour {

	[HideInInspector]
	public OptimisationPool pool;

	public void Despawn () {
		if (pool != null) {
			pool.reserve.Add (gameObject);
			pool.actives.Remove (gameObject);
			gameObject.transform.position = pool.transform.position;
		}
	}
}
