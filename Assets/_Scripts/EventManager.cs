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

	private float currentMayhemLevel = 0.0f;

	void Awake()
	{

	}

	// Use this for initialization
	void Start ()
	{
		MayhemBar.fillAmount = 0.0f;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentMayhemLevel > 100.0f)
			currentMayhemLevel = 100.0f;
		
		MayhemBar.fillAmount  = Mathf.Lerp(MayhemBar.fillAmount, (currentMayhemLevel / maxLevelMayhem), Time.deltaTime * animationSpeed);
	}

	public void IncreaseMayhem(float value)
	{
		currentMayhemLevel += value;
	}

	public void RepairPuke ()
	{
		Debug.Log ("OK");
	}
}
