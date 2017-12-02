using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfScene : MonoBehaviour {

	public string nextScene;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player") == true) {
			SceneManager.LoadScene (nextScene);
		}
	}
}
