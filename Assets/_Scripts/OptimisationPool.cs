using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptimisationPool : MonoBehaviour {

	public OptimisationItem prefab;

	public List<GameObject> reserve;

	public List<GameObject> actives;

	GameObject Create () {
		GameObject o = Instantiate (prefab.gameObject, transform.position, transform.rotation);
		reserve.Add (o);
		o.transform.parent = null;
		o.GetComponent<OptimisationItem> ().pool = this.gameObject.GetComponent<OptimisationPool> ();
		o.SetActive (false);
		return o;
	}

	public GameObject Spawn () {
		GameObject item;
		if (reserve.Count == 0) {
			item = Create ();
		}
		item = reserve [0];
		item.transform.position = transform.position;
		item.transform.rotation = transform.rotation;
		item.SetActive (true);
		reserve.Remove (item);
		actives.Add (item);
		return item;
	}
}
