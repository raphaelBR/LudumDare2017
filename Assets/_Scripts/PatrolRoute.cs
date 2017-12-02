using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : MonoBehaviour {

	public enum EndOfPath {Stop, Loop, Reverse}

	public bool beginAtStart = true;
	public EndOfPath endAction = EndOfPath.Stop;

	[Space(20)]
	[Range(0f, 30f)]
	public float speed = 3f;
	[Range(0f, 30f)]
	public float toleranceArea = 3f;
	[Space(20)]
	public List<Transform> passagePoints;

	private bool moving = false;
	private bool goingUp = false;
	private int step = 0;

	void Start () {
		if (beginAtStart == true) {
			Launch ();
		}
	}

	void Update () {
		if (moving == true) {
			if (step < 0 || step >= passagePoints.Count) {
				switch (endAction) {
				case EndOfPath.Stop:
					moving = false;
					break;
				case EndOfPath.Loop:
					step = 0;
					break;
				case EndOfPath.Reverse:
					goingUp = !goingUp;
					if (goingUp == true) {
						step = passagePoints.Count - 1;
					} else {
						step = 0;
					}
					break;
				default:
					break;
				}
			} else {
				if (passagePoints [step] == null) {
					passagePoints.Remove (passagePoints [step]);
				} else {
					if (Vector3.Distance (transform.position, passagePoints [step].transform.position) > toleranceArea) {
						transform.position = transform.position + speed * (passagePoints [step].transform.position - transform.position).normalized / 100;
					} else {
						if (goingUp == false) {
							step = step + 1;
						} else {
							step = step - 1;
						}
					}
				}
			}
		}
	}

	public void Launch () {
		moving = true;
	}
}
