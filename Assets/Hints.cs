using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour {

	// Handles particle behavior for certain objects to give the player hints.


	// set publically

	[Space(5)]
	public GameObject rod;
	public GameObject rodParticles;

	[Space(5)]
	public GameObject hook;
	public GameObject hookParticles;

	[Space(5)]
	public GameObject oceanParticles;

	[Space(5)]
	public EatFish checkIfFished;
	[Space(2)]
	public float timeBeforeHintsTrigger = 120f;


	void Start () {
		Invoke("TimeForHints", timeBeforeHintsTrigger);
	}


	void TimeForHints() {
		// Continuously check if particles should change or stop
		InvokeRepeating("ToggleParticles", 0f, 3f);
	}


	public void ToggleParticles() {

		// If hook is not yet attached to rod...
		if (hook.transform.parent != rod) {
			hookParticles.SetActive(true);
			rodParticles.SetActive(true);
		}

		// Then until a fish is fished up (player figures out what they need to do), this continues to roll true and Else isn't triggered.
		// Once hook/rod are connected, they stop glowing, and ocean starts glowing instead
		else if (checkIfFished.fishFed == 0) {
			if (oceanParticles.activeInHierarchy == false) {				
				hookParticles.SetActive(false);
				rodParticles.SetActive(false);
			}
		}

		else {			
			Invoke("DisableAllParticles", 2f);
		}
	}

	public void DisableAllParticles() {
		CancelInvoke("ToggleParticles");
		this.enabled = false;
	}
}
