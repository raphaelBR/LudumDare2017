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
					int k = 0;
					switch (item) {
					case PlayerBehavior.Item.Beer:
						k = 6;
						break;
					case PlayerBehavior.Item.Chips:
						k = 3;
						break;
					case PlayerBehavior.Item.Juice:
						k = 6;
						break;
					case PlayerBehavior.Item.Pizza:
						k = 2;
						break;
					}

					for (int i = 0; i < k; i++) {
						GameObject food = GetComponent<OptimisationPool> ().Spawn ();
						food.transform.position = new Vector3 (Random.Range (transform.position.x + transform.lossyScale.x / 2, transform.position.x - transform.lossyScale.x / 2), transform.position.y, Random.Range (transform.position.z + transform.lossyScale.z / 2, transform.position.z - transform.lossyScale.z / 2));	
					}
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
