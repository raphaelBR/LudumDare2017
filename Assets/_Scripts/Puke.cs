using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puke : MonoBehaviour {

//	public OptimisationPool vodkaSource;
//	public Transform table;
//
//	public void AddVodka () {
//		GameObject vodka = vodkaSource.Spawn ();
//		vodka.transform.position = new Vector3 (Random.Range (table.position.x + table.lossyScale.x / 2, table.position.x - table.lossyScale.x / 2), table.position.y, Random.Range (table.position.z + table.lossyScale.z / 2, table.position.z - table.lossyScale.z / 2));
//	}

	private EventManager eventManagement;

	void Start () {
		eventManagement = FindObjectOfType<EventManager> ().GetComponent<EventManager> ();
	}

	public void Resolve () {
		if (eventManagement.ResolvePuke () == true) {
			GetComponent<AutoDestruction> ().SelfDestruct ();
		}
	}

	void Update () {
		
	}
}
