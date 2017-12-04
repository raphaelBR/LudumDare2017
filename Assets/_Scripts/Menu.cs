using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public string gameScene;
	public string mainScene;

	public Text score;

	void Start () {
		if (score != null) {
			score.text = "Score: " + PlayerPrefs.GetFloat ("Score").ToString ();;
		}
	}

	public void GoToGame () {
		SceneManager.LoadScene (gameScene);
	}

	public void GoToMain () {
		SceneManager.LoadScene (mainScene);
	}
}
