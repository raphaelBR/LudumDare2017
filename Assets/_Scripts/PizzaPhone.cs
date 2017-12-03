using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaPhone : MonoBehaviour {

	private bool available = true;

	public SpriteRenderer phone;

	private PizzaGuy dude;

	public float cooldownDelay = 5f;

	public void Call () {
		if (available == true) {
			dude.Deliver ();
			StartCoroutine (Cooldown ());
		}
	}

	IEnumerator Cooldown () {
		available = false;
		phone.enabled = false;
		yield return new WaitForSeconds (cooldownDelay);
		phone.enabled = true;
		available = true;
	}
}
