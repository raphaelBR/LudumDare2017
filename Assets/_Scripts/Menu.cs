using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public Image mainMenu;
	public Image gameOver;

	public string gameScene;
	public string menuScene;

	void Awake () {
		DontDestroyOnLoad (gameObject);
		GoToMain ();
	}

	public void GoToGame () {
		mainMenu.gameObject.SetActive (false);
		gameOver.gameObject.SetActive (false);
		SceneManager.LoadScene (gameScene);
	}

	public void GoToMain () {
		mainMenu.gameObject.SetActive (true);
		gameOver.gameObject.SetActive (false);
		SceneManager.LoadScene (menuScene);
	}

	public void GoToDeath () {
		mainMenu.gameObject.SetActive (false);
		gameOver.gameObject.SetActive (true);
		SceneManager.LoadScene (menuScene);
	}
}
