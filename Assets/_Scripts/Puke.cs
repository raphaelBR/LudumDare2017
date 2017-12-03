using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puke : MonoBehaviour {


	private EventManager eventManagement;

	void OnEnable () {
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
