using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScream : MonoBehaviour {

	// Makes Sun play only a specific part of its clip

	public AudioSource clip;

	public float cliptime;
	public float repeattime;

	void Start () {
		clip = gameObject.GetComponent<AudioSource>();
		clip.Play();

		InvokeRepeating("ClipReset", 0f, 0.98f);
	}

	void ClipReset() {
		clip.time = 1f;
	}
}
