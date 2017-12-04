using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoDestruction : MonoBehaviour {

	public bool beginOnStart = false;
	public LayerMask destructionOnContact;
	[Range(0f, 60f)]
	public float destructionDelay = 1f;
	public bool vanish = true;
	[Range(0f, 60f)]
	public float vanishingDelay = 1f;
	public bool disable = true;
	public UnityEvent lastWords;

	private bool isVanishing = false;

	void Start () {
		if (beginOnStart == true) {
			SelfDestruct ();
		}
	}

	void OnEnable () {
		isVanishing = false;
		if (GetComponent<Renderer> () == true) {
			GetComponent<Renderer> ().material.color = new Color (GetComponent<Renderer> ().material.color.r, GetComponent<Renderer> ().material.color.g, GetComponent<Renderer> ().material.color.b, 1f);
	
		}
		if (GetComponent<Rigidbody> () == true) {
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
		if (beginOnStart == true) {
			SelfDestruct ();
		}
	}

	void Update () {
		if (isVanishing == true) {
			if (GetComponent<Renderer> () == true) {
				if (GetComponent<Renderer> ().material.color.a > 0f) {
					GetComponent<Renderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
					GetComponent<Renderer> ().material.color = new Color (GetComponent<Renderer> ().material.color.r, GetComponent<Renderer> ().material.color.g, GetComponent<Renderer> ().material.color.b, GetComponent<Renderer> ().material.color.a - (1f / vanishingDelay * Time.deltaTime));
				} else {
					TheEnd ();
				}
			} else {
				TheEnd ();
			}
		} else {
			GetComponent<Renderer> ().material.color = new Color (GetComponent<Renderer> ().material.color.r, GetComponent<Renderer> ().material.color.g, GetComponent<Renderer> ().material.color.b,1f);
		}
	}

	public void SelfDestruct () {
		if (gameObject.activeSelf == true) {
			StartCoroutine (Agony ());
		}
	}

	void OnCollisionEnter (Collision other) {
		if (destructionOnContact == (destructionOnContact | 1<<other.gameObject.layer)) {
			SelfDestruct();
		}
	}

	void OnTriggerEnter (Collider other) {
		if (destructionOnContact == (destructionOnContact | 1<<other.gameObject.layer)) {
			SelfDestruct();
		}
	}

	IEnumerator Agony () {
		yield return new WaitForSeconds (destructionDelay);
		if (vanish == true) {
			isVanishing = true;
		} else {
			TheEnd ();
		}
	}

	void TheEnd () {
		lastWords.Invoke ();
		if (disable == true) {
			this.gameObject.SetActive (false);
		} else {
			GameObject.Destroy (this.gameObject);
		}
	}
}
