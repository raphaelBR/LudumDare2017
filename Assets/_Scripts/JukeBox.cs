using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JukeBox : MonoBehaviour {

	public float disco1 = 1f;
	public float disco2 = 0.5f;
	public float disco3 = 0.25f;
	[Space(10f)]
	public float shake1 = 0.1f;
	public float shake2 = 0.2f;
	public float shake3 = 0.5f;
	[Space(10f)]
	public List<AudioClip> level1;
	public List<AudioClip> level2;
	public List<AudioClip> level3;
	public List<AudioClip> level4;
	public List<AudioClip> level5;
	public List<AudioClip> level6;
	public List<AudioClip> level7;
	public List<AudioClip> level8;
	public List<AudioClip> level9;

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
		switch (level) {
		case 1:
			source.clip = level1 [Random.Range (0, level1.Count - 1)];
			RestoreLight ();
			break;
		case 2:
			source.clip = level2 [Random.Range (0, level2.Count - 1)];
			RestoreLight ();
			break;
		case 3:
			source.clip = level3 [Random.Range (0, level3.Count - 1)];
			RestoreLight ();
			break;
		case 4:
			source.clip = level4 [Random.Range (0, level4.Count - 1)];
			RestoreLight ();
			break;
		case 5:
			source.clip = level5 [Random.Range (0, level5.Count - 1)];
			RestoreLight ();
			break;
		case 6:
			source.clip = level6 [Random.Range (0, level6.Count - 1)];
			RestoreLight ();
			break;
		case 7:
			source.clip = level7 [Random.Range (0, level7.Count - 1)];
			ChangeLight ();
			discoRatio = disco1;
			shakeAmount = shake1;
			break;
		case 8:
			source.clip = level8 [Random.Range (0, level8.Count - 1)];
			ChangeLight ();
			discoRatio = disco2;
			shakeAmount = shake2;
			break;
		case 9:
			source.clip = level9 [Random.Range (0, level9.Count - 1)];
			ChangeLight ();
			discoRatio = disco3;
			shakeAmount = shake3;
			break;
		default:
			break;
		}
		source.Play ();
	}

	void ChangeLight () {
		if (disco.color == Color.white) {
			StartCoroutine (Disco ());
		}
		isShaking = true;
	}

	void RestoreLight () {
		disco.color = Color.white;
		isShaking = false;
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
		yield return new WaitForSeconds (10f);
		if (i < 9) {
			i = i + 1;
		} else {
			i = 1;
		}
		ChangeSong (i);
		Debug.Log (i);
		StartCoroutine (SoundUp ());
	}
}
