using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconuts : MonoBehaviour {
	
	public bool canMakeNoise = true;
	public AudioSource audio;

	void Start() {
		audio = gameObject.GetComponent<AudioSource>();
	}



	void OnCollisionEnter(Collision col) {

		// When colliding with beach, coconuts now only make their sound once.
		// To make sound again, they have to have gained a parent (as in, they're grabbed with controller).
		// Apart from initial impact, sound only plays through player interaction, and only once.
		if (canMakeNoise) {
			if (col.gameObject.CompareTag("MainIsland")) {
				audio.enabled = true;
				canMakeNoise = false;
				transform.parent = null;

				audio.Play();
				Invoke("AudioOff", audio.clip.length + 0.15f);
			}
		}


		// Hanging coconut can be knocked off tree with a grabbable
		if (gameObject.name == "HangingCoconut") {
			if (col.gameObject.tag == "Grabbable") {
				gameObject.GetComponent<Rigidbody>().useGravity = true;
				gameObject.GetComponent<Rigidbody>().isKinematic = false;
			}
		}


		if (col.gameObject.name == "TreasureChest") {
			col.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
		}

	}



	// If a Coconut gains a Controller parent, it can make noise.
	void LateUpdate() {		
		if (transform.parent != null) {
			canMakeNoise = true;
		}
	}


	void AudioOff() {
		audio.enabled = false;
		Invoke("PhysicsOff", 3f);
	}

	void PhysicsOff() {
		gameObject.GetComponent<Rigidbody>().isKinematic = true;
		CancelInvoke("PhysicsOff");
	}
}
