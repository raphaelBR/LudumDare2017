using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerBehavior : MonoBehaviour {

	public enum Item {None, Mop, Pizza, Chips, Juice, Beer, Vodka}
	public Item item = Item.None;

	public SpriteRenderer itemImage;
	public Sprite mopSprite;
	public Sprite pizzaSprite;
	public Sprite chipsSprite;
	public Sprite juiceSprite;
	public Sprite beerSprite;
	public Sprite vodkaSprite;

	public float pauseDuration = 0.1f;

	private NavMeshAgent agent;
	private TransformExtension transf;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		transf = GetComponent<TransformExtension> ();
	}

	void Update () {
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.GetComponent<Bubble> () != null) {
					agent.SetDestination (hit.collider.gameObject.GetComponent<Bubble> ().destination.position);
				} else {
					agent.SetDestination (hit.point);
				}
			}
		}
	}

	public bool TakeItem (Item takenItem) {
		if (item == Item.None) {
			switch (takenItem) {
			case Item.Mop:
				itemImage.sprite = mopSprite;
				break;
			case Item.Pizza:
				itemImage.sprite = pizzaSprite;
				break;
			case Item.Chips:
				itemImage.sprite = chipsSprite;
				break;
			case Item.Juice:
				itemImage.sprite = juiceSprite;
				break;
			case Item.Beer:
				itemImage.sprite = beerSprite;
				break;
			case Item.Vodka:
				itemImage.sprite = vodkaSprite;
				break;
			default:
				break;
			}
			StartCoroutine (Pause ());
			item = takenItem;
			return true;
		}
		return false;
	}

	public bool DropItem (Item droppedItem) {
		if (item == droppedItem) {
			StartCoroutine (Pause ());
			item = Item.None;
			itemImage.sprite = null;
			return true;
		}
		return false;
	}

	IEnumerator Pause () {
		transf.enabled = true;
		transf.InitializeFreeze ();
		yield return new WaitForSeconds (pauseDuration);
		transf.enabled = false;
	}
}
