using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPlacement : MonoBehaviour {

	public PlayerBehavior.Item item;
	public bool taking = true;

	public SpriteRenderer mopDump;

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
				if (player.DropItem (item) == true) {
					// Place on table
				}
			}
		} else {
			if (available == true && player.TakeItem (item) == true) {
				mopDump.enabled = false;
				available = false;
			} else if (available == false && player.DropItem (item) == true) {
				mopDump.enabled = true;
				available = true;
			}
		}
	}
}
