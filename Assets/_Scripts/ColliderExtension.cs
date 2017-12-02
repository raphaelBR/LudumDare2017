using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ColliderExtension : MonoBehaviour {

	public enum SelectionTypes {Any, Not, Only}

	public SelectionTypes types = SelectionTypes.Any;

	public SelectionTypes rigidbodiesCondition = SelectionTypes.Any;

	public SelectionTypes collidersCondition = SelectionTypes.Any;
	public List<Collider> collidersList;

	public SelectionTypes layersCondition = SelectionTypes.Any;
	public LayerMask layersList;

	public SelectionTypes tagsCondition = SelectionTypes.Any;
	public List<string> tagsList;

	public SelectionTypes pMaterialsCondition = SelectionTypes.Any;
	public List<PhysicMaterial> pMaterialsList;

	public UnityEvent onEnter;
	public UnityEvent onStay;
	public UnityEvent onExit;

	void OnTriggerEnter (Collider other) {
		if (Test (other) == true) {
			onEnter.Invoke ();
		}
	}

	void OnCollisionEnter (Collision other) {
		if (Test (other.collider) == true) {
			onEnter.Invoke ();
		}
	}

	void OnTriggerStay (Collider other) {
		if (Test (other) == true) {
			onStay.Invoke ();
		}
	}

	void OnCollisionStay (Collision other) {
		if (Test (other.collider) == true) {
			onStay.Invoke ();
		}
	}

	void OnTriggerExit (Collider other) {
		if (Test (other) == true) {
			onExit.Invoke ();
		}
	}

	void OnCollisionExit (Collision other) {
		if (Test (other.collider) == true) {
			onExit.Invoke ();
		}
	}

	bool Test (Collider other) {
		if ((other.isTrigger == false && types == SelectionTypes.Only) || (other.isTrigger == true && types == SelectionTypes.Not)) {
			return false;
		}
		if ((collidersList.Contains (other) == false && collidersCondition == SelectionTypes.Only) || (collidersList.Contains (other) == true && collidersCondition == SelectionTypes.Not)) {
			return false;
		}
		if ((other.attachedRigidbody == null && rigidbodiesCondition == SelectionTypes.Only) || (other.attachedRigidbody != null && rigidbodiesCondition == SelectionTypes.Not)) {
			return false;
		}
		if ((layersList == (layersList | (1 << other.gameObject.layer)) && layersCondition == SelectionTypes.Only) || (layersList != (layersList | (1 << other.gameObject.layer)) && layersCondition == SelectionTypes.Not)) {
			return false;
		}
		if ((tagsList.Contains (other.tag) == false && tagsCondition == SelectionTypes.Only) || (tagsList.Contains (other.tag) == true && tagsCondition == SelectionTypes.Not)) {
			return false;
		}
		if ((pMaterialsList.Contains (other.material) == false && pMaterialsCondition == SelectionTypes.Only) || (pMaterialsList.Contains (other.material) == true && pMaterialsCondition == SelectionTypes.Not)) {
			return false;
		}
		return true;
	}
}
