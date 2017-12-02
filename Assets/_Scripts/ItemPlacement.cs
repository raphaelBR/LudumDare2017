using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacement : MonoBehaviour {

	public PlayerBehavior.Item item;
	public bool taking = true;

	private bool available = true;
	private PlayerBehavior player;

	void Awake () {
		player = FindObjectOfType<PlayerBehavior> ().GetComponent<PlayerBehavior> ();
	}

	public void Interact () {
		if (item != PlayerBehavior.Item.Mop) {
			if (taking == true) {
				player.TakeItem (item);
			} else {
				player.DropItem (item);
			}
		} else {
			if (available == true && player.TakeItem (item) == true) {
				available = false;
			} else if (available == false && player.DropItem (item) == true) {
				available = true;
			}
		}
	}
}
