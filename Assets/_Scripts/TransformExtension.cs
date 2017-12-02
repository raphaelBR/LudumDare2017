using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class TransformExtension : MonoBehaviour {

	public enum Axis {All, XY, YZ, XZ, X, Y, Z, None}
	public enum ActionMode {Freeze, Follow, Copy, Apply, Focus, None}

	public ActionMode mode = ActionMode.None;

	public Axis positionAxis = Axis.None;
	public Axis rotationAxis = Axis.None;
	public Axis scaleAxis = Axis.None;

	public bool positionWorld = false;
	public bool rotationWorld = false;
	public bool mirror = false;

	public Axis moveAxis = Axis.None;
	public float moveSpeedX = 0f;
	public float moveSpeedY = 0f;
	public float moveSpeedZ = 0f;

	public Axis spinAxis = Axis.None;
	public float spinSpeedX = 0f;
	public float spinSpeedY = 0f;
	public float spinSpeedZ = 0f;

	public Axis growAxis = Axis.None;
	public float growSpeedX = 0f;
	public float growSpeedY = 0f;
	public float growSpeedZ = 0f;

	public Transform target = null;

	public bool moveToward = false;
	public float speed = 0f;
	public bool lookAt = false;

	public Vector3 rawPosition;
	public Vector3 rawRotation;
	public Vector3 rawScale;

	private Vector3 oldPosition;
	private Vector3 oldRotation;
	private Vector3 oldScale;

	private Vector3 differencePosition;
	private Vector3 differenceRotation;
	private Vector3 differenceScale;

	private Vector3 mirrorPosition;
	private Vector3 mirrorRotation;
	private Vector3 mirrorScale;

	void OnEnable () {
		InitializeFreeze ();
		if (target != null) {
			InitializeFollow (target);
		}
	}

	void Update () {
		switch (mode) {
		case ActionMode.Freeze:
			Freeze ();
			break;
		case ActionMode.Follow:
			Follow ();
			break;
		case ActionMode.Copy:
			Copy ();
			break;
		case ActionMode.Apply:
			Move ();
			Spin ();
			Grow ();
			break;
		case ActionMode.Focus:
			Focus ();
			break;
		case ActionMode.None:
			break;
		default:
			Debug.Log ("An error occurred");
			break;
		}
	}

	public void InitializeFreeze () {
		oldPosition = transform.position;
		oldRotation = transform.rotation.eulerAngles;
		oldScale = transform.localScale;
	}

	public void InitializeFollow (Transform following) {
		differencePosition = transform.position - following.position;
		differenceRotation = transform.rotation.eulerAngles - following.rotation.eulerAngles;
		differenceScale = transform.localScale - following.localScale;

		mirrorPosition = transform.position + following.position;
		mirrorRotation = transform.rotation.eulerAngles + following.rotation.eulerAngles;
		mirrorScale = transform.localScale + following.localScale;
	}

	void Freeze () {
		TransformOverride (transform.position, transform.rotation, transform.localScale, oldPosition, Quaternion.Euler (oldRotation), oldScale);
	}

	void Follow () {
		if (target != null) {
			if (mirror == false) {
				TransformOverride (transform.position, transform.rotation, transform.localScale, target.position + differencePosition, target.rotation * Quaternion.Euler (differenceRotation), target.localScale + differenceScale);
			} else {
				TransformOverride (transform.position, transform.rotation, transform.localScale, mirrorPosition - target.position, Quaternion.Euler (mirrorRotation) * Quaternion.Inverse (target.rotation), mirrorScale - target.localScale);
			}
		}
	}

	void Copy () {
		if (target != null) {
			TransformOverride (transform.position, transform.rotation, transform.localScale, target.position, target.rotation, target.localScale);
		}
	}

	void Move () {
		if (positionWorld == true) {
			transform.Translate (Vectorize (moveAxis, moveSpeedX, moveSpeedY, moveSpeedZ), Space.World);
		} else {
			transform.Translate (Vectorize (moveAxis, moveSpeedX, moveSpeedY, moveSpeedZ), Space.Self);
		}
	}

	void Spin () {
		if (rotationWorld == true) {
			transform.Rotate (Vectorize (spinAxis, spinSpeedX, spinSpeedY, spinSpeedZ), Space.World);
		} else {
			transform.Rotate (Vectorize (spinAxis, spinSpeedX, spinSpeedY, spinSpeedZ), Space.Self);
		}
	}

	void Grow () {
		transform.localScale += Vectorize (growAxis, growSpeedX, growSpeedY, growSpeedZ);
	}

	void Focus () {
		if (target != null) {
			if (moveToward == true ) {
				transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			}
			if (lookAt == true) {
				transform.LookAt (target);
			}
		}
	}

	Vector3 Vectorize (Axis axis, float x, float y, float z) {
		switch (axis) {
		case Axis.All:
			return (new Vector3(x , y, z) * Time.deltaTime);
		case Axis.X:
			return (new Vector3 (x, 0f, 0f) * Time.deltaTime);
		case Axis.Y:
			return (new Vector3 (0f, y, 0f) * Time.deltaTime);
		case Axis.Z:
			return (new Vector3 (0f, 0f, z) * Time.deltaTime);
		case Axis.XY:
			return (new Vector3 (x, y, 0f) * Time.deltaTime);
		case Axis.YZ:
			return (new Vector3 (0f, y, z) * Time.deltaTime);
		case Axis.XZ:
			return (new Vector3 (x, 0f, z) * Time.deltaTime);
		case Axis.None:
			return Vector3.zero;
		default:
			Debug.Log ("An error occurred");
			return Vector3.zero;
		}
	}

	void TransformOverride (Vector3 positionOriginal, Quaternion rotationOriginal, Vector3 scaleOriginal, Vector3 positionModifier, Quaternion rotationModifier, Vector3 scaleModifier) {
		switch (positionAxis) {
		case Axis.All:
			transform.position = positionModifier;
			break;
		case Axis.XY:
			transform.position = new Vector3 (positionModifier.x, positionModifier.y, positionOriginal.z);
			break;
		case Axis.YZ:
			transform.position = new Vector3 (positionOriginal.x, positionModifier.y, positionModifier.z);
			break;
		case Axis.XZ:
			transform.position = new Vector3 (positionModifier.x, positionOriginal.y, positionModifier.z);
			break;
		case Axis.X:
			transform.position = new Vector3 (positionModifier.x, positionOriginal.y, positionOriginal.z);
			break;
		case Axis.Y:
			transform.position = new Vector3 (positionOriginal.x, positionModifier.y, positionOriginal.z);
			break;
		case Axis.Z:
			transform.position = new Vector3 (positionOriginal.x, positionOriginal.y, positionModifier.z);
			break;
		case Axis.None:
			break;
		default:
			Debug.Log ("An error occurred");
			break;
		}
		switch (rotationAxis) {
		case Axis.All:
			transform.rotation = rotationModifier;
			break;
		case Axis.XY:
			transform.rotation = Quaternion.Euler (new Vector3 (rotationModifier.eulerAngles.x, rotationModifier.eulerAngles.y, rotationOriginal.eulerAngles.z));
			break;
		case Axis.YZ:
			transform.rotation = Quaternion.Euler (new Vector3 (rotationOriginal.eulerAngles.x, rotationModifier.eulerAngles.y, rotationModifier.eulerAngles.z));
			break;
		case Axis.XZ:
			transform.rotation = Quaternion.Euler (new Vector3 (rotationModifier.eulerAngles.x, rotationOriginal.eulerAngles.y, rotationModifier.eulerAngles.z));
			break;
		case Axis.X:
			transform.rotation = Quaternion.Euler (new Vector3 (rotationModifier.eulerAngles.x, rotationOriginal.eulerAngles.y, rotationOriginal.eulerAngles.z));
			break;
		case Axis.Y:
			transform.rotation = Quaternion.Euler (new Vector3 (rotationOriginal.eulerAngles.x, rotationModifier.eulerAngles.y, rotationOriginal.eulerAngles.z));
			break;
		case Axis.Z:
			transform.rotation = Quaternion.Euler (new Vector3 (rotationOriginal.eulerAngles.x, rotationOriginal.eulerAngles.y, rotationModifier.eulerAngles.z));
			break;
		case Axis.None:
			break;
		default:
			Debug.Log ("An error occurred");
			break;
		}
		switch (scaleAxis) {
		case Axis.All:
			transform.localScale = scaleModifier;
			break;
		case Axis.XY:
			transform.localScale = new Vector3 (scaleModifier.x, scaleModifier.y, scaleOriginal.z);
			break;
		case Axis.YZ:
			transform.localScale = new Vector3 (scaleOriginal.x, scaleModifier.y, scaleModifier.z);
			break;
		case Axis.XZ:
			transform.localScale = new Vector3 (scaleModifier.x, scaleOriginal.y, scaleModifier.z);
			break;
		case Axis.X:
			transform.localScale = new Vector3 (scaleModifier.x, scaleOriginal.y, scaleOriginal.z);
			break;
		case Axis.Y:
			transform.localScale = new Vector3 (scaleOriginal.x, scaleModifier.y, scaleOriginal.z);
			break;
		case Axis.Z:
			transform.localScale = new Vector3 (scaleOriginal.x, scaleOriginal.y, scaleModifier.z);
			break;
		case Axis.None:
			break;
		default:
			Debug.Log ("An error occurred");
			break;
		}
	}
}
