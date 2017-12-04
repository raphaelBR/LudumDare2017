using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour {

	[SerializeField]
	private Image MayhemBar;

	[SerializeField]
	private float maxLevelMayhem = 100.0f;

	[SerializeField]
	private float animationSpeed = 0.2f;

	[SerializeField]
	private float pukeRatio = 0.05f;

	[SerializeField]
	private Text timeText;

	public OptimisationPool juiceSource;
	public OptimisationPool beerSource;
	public OptimisationPool vodkaSource;
	public OptimisationPool chipsSource;
	public OptimisationPool pizzaSource;
	public Transform table;

	public OptimisationPool pukeSource;
	private PlayerBehavior player;
	private Menu menu;

	private float timer = 0f;

	private float currentMayhemLevel = 0.0f;

	public string overScene;

	private JukeBox juke;

	private int soundLevel = 1;

	private float survivedTime = 0f;

	void Start ()
	{
		survivedTime = 0f;
		MayhemBar.fillAmount = 0.0f;	
		timer = 0f;
		soundLevel = 1;
		juke = FindObjectOfType<JukeBox> ().GetComponent<JukeBox> ();
		player = FindObjectOfType<PlayerBehavior> ().GetComponent<PlayerBehavior> ();
	}

	void Update ()
	{
		timer += Time.deltaTime;
		survivedTime += Time.deltaTime;
		timeText.text = "Score: " + (Mathf.Round(survivedTime*100)/100).ToString ();
		if (pukeSource.actives.Count > 0 && timer >= 1f)
		{
			IncreaseMayhem ((float)pukeSource.actives.Count * pukeRatio);
//			Debug.Log ("Puke party");
			timer = 0f;
		}
		MayhemBar.fillAmount  = currentMayhemLevel / maxLevelMayhem;
	}

	public void IncreaseMayhem (float value)
	{
		currentMayhemLevel += value;
		if (currentMayhemLevel >= maxLevelMayhem) {
			PlayerPrefs.SetFloat ("Score", survivedTime);
			SceneManager.LoadScene (overScene);
		}
		if ((currentMayhemLevel / (maxLevelMayhem/10f)) > (float)soundLevel){
			soundLevel = soundLevel + 1;
			juke.ChangeSong (soundLevel);
		} 

	}

	public void MakePuke (Transform transf)
	{
		GameObject puke = pukeSource.Spawn ();
		puke.transform.position = transf.position;
	}

	public bool ResolvePuke () {
		if (player.item == PlayerBehavior.Item.Mop) {
			return true;
		} else {
			return false;
		}
	}

	public void BringVodka ()
	{
		GameObject vodka = vodkaSource.Spawn ();
		vodka.transform.position = new Vector3 (Random.Range (table.position.x + table.lossyScale.x / 2, table.position.x - table.lossyScale.x / 2), table.position.y, Random.Range (table.position.z + table.lossyScale.z / 2, table.position.z - table.lossyScale.z / 2));
	}
}
