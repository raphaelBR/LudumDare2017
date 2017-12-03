using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour {

	public Menu prefab;

	void Start () {
		if (FindObjectOfType<Menu> () == null) {
			Instantiate (prefab.gameObject);
		}
	}
}
