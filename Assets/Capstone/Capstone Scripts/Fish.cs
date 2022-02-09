using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fish : MonoBehaviour {

	// Script to make fish unparent from end of rod, rescale, and start dying.

	// set publically
	public GameObject fishingPole;
	public GameObject selfParent;

	[Space(10)]
	public bool dying = false;


	void OnCollisionEnter(Collision col) {
		if (!dying) {
			if (col.gameObject.CompareTag("MainIsland")) {
				dying = true;
				InvokeRepeating("DyingFish", 0, 2);
			}
		}
	}

	// For when Fish exit the water {or technically, the boundary area}
	void OnTriggerExit(Collider other) {
		if (other.tag == "boundary") {
			Invoke("UnparentFish", 1.5f);
		}
	}


	// Unbind from edge of rod, push towards beach, apply physics, rescale
	void UnparentFish() {		
		selfParent.transform.parent = null;
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().useGravity = true;
		transform.localScale = new Vector3 (1f, 1f, 1f);
	}


	void DyingFish() {
		if (GetComponent<Animator>().speed > 0.3f) {
			GetComponent<Animator>().speed *= 0.9f;
		}

		else { 
			GetComponent<Animator>().speed = 0.3f;
			CancelInvoke("DyingFish");
		}
	}

}
