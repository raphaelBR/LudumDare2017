using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

	[SerializeField]
	private Image MayhemBar;

	[SerializeField]
	private float maxLevelMayhem = 100.0f;

	[SerializeField]
	private float animationSpeed = 0.2f;

	public OptimisationPool pukeSource;
	private PlayerBehavior player;

	private float currentMayhemLevel = 0.0f;

	void Awake()
	{
		player = FindObjectOfType<PlayerBehavior> ().GetComponent<PlayerBehavior> ();
	}

	// Use this for initialization
	void Start ()
	{
		MayhemBar.fillAmount = 0.0f;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (pukeSource.actives.Count);
		if (currentMayhemLevel > 100.0f)
			currentMayhemLevel = 100.0f;
		
		MayhemBar.fillAmount  = Mathf.Lerp(MayhemBar.fillAmount, (currentMayhemLevel / maxLevelMayhem), Time.deltaTime * animationSpeed);
	}

	public void IncreaseMayhem(float value)
	{
		currentMayhemLevel += value;
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
}
