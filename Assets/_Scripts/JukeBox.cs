﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JukeBox : MonoBehaviour {

	public float disco1 = 1f;
	public float disco2 = 0.7f;
	public float disco3 = 0.5f;
	public float disco4 = 0.35f;
	public float disco5 = 0.2f;
	public float disco6 = 0.1f;
	[Space(10f)]
	public float shake1 = 0.1f;
	public float shake2 = 0.2f;
	public float shake3 = 0.5f;
	[Space(10f)]
	public List<AudioClip> levels;

	private Light disco;
	private float discoRatio;

	private bool isShaking = false;
	private float shakeAmount = 0.7f;
	private Vector3 originalPos;

	private AudioSource source;

	// Temporary
	private int i = 1;

	void Awake () {
		source = GetComponent<AudioSource> ();
		disco = FindObjectOfType<Light> ().GetComponent<Light> ();
		ChangeSong (1);
		StartCoroutine (SoundUp ());
		originalPos = transform.localPosition;
	}

	void Update () {
		if (isShaking == true) {
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
		} else {
			transform.localPosition = originalPos;
		}
		if (source.isPlaying == false) {
			ChangeSong (i);
		}
	}

	public void ChangeSong(int level) {
		source.clip = levels [level - 1];

		if (level <= 3) {
			disco.color = Color.white;
		} else {
			if (disco.color == Color.white) {
				StartCoroutine (Disco ());
			}
		}
		if (level <= 6) {
			isShaking = false;
		} else {
			isShaking = true;
		}
		switch (level) {
		case 4:
			discoRatio = disco1;
			break;
		case 5:
			discoRatio = disco2;
			break;
		case 6:
			discoRatio = disco3;
			break;
		case 7:
			discoRatio = disco4;
			shakeAmount = shake1;
			break;
		case 8:
			discoRatio = disco5;
			shakeAmount = shake2;
			break;
		case 9:
			discoRatio = disco6;
			shakeAmount = shake3;
			break;
		default:
			break;
		}
		source.Play ();
	}

	IEnumerator Disco () {
		disco.color = Random.ColorHSV ();
		while (disco.color == Color.white) {
			disco.color = Random.ColorHSV ();
		}
		yield return new WaitForSeconds (discoRatio);
		if (disco.color != Color.white) {
			StartCoroutine (Disco ());
		}
	}

	IEnumerator SoundUp () {
		yield return new WaitForSeconds (5f);
		if (i < 9) {
			i = i + 1;
		} else {
			i = 1;
		}
		ChangeSong (i);
		//Debug.Log (i);
		StartCoroutine (SoundUp ());
	}
}
