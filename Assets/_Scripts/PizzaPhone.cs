using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaPhone : MonoBehaviour {

	private bool available = true;

	public SpriteRenderer phone;

	private PizzaGuy dude;

	public float cooldownDelay = 5f;
	private AudioSource sound;

	void Start () {
		dude = FindObjectOfType<PizzaGuy> ().GetComponent<PizzaGuy> ();
		sound = GetComponent<AudioSource> ();
	}

	public void Call () {
		if (available == true) {
			sound.Play ();
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
