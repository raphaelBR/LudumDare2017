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
	private Menu menu;

	private float currentMayhemLevel = 0.0f;

	public OptimisationPool vodkaSource;
	public Transform table;

	void Awake()
	{
		player = FindObjectOfType<PlayerBehavior> ().GetComponent<PlayerBehavior> ();
		menu = FindObjectOfType<Menu> ().GetComponent<Menu> ();
	}

	void Start ()
	{
		MayhemBar.fillAmount = 0.0f;	
	}

	void Update ()
	{
		if (currentMayhemLevel > 100.0f)
		{
			currentMayhemLevel = 100.0f;
		}
		MayhemBar.fillAmount  = Mathf.Lerp(MayhemBar.fillAmount, (currentMayhemLevel / maxLevelMayhem), Time.deltaTime * animationSpeed);
	}

	public void IncreaseMayhem (float value)
	{
		currentMayhemLevel += value;
		if ((float)MayhemBar.fillAmount >= maxLevelMayhem) {
			menu.GoToDeath ();
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
