using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

	public Transform target;

	void Start() {
		if (gameObject.name == "Achievement Menu") {
			target = GameObject.FindWithTag("MainCamera").transform;
		}
	}

	void FixedUpdate () {		
		transform.rotation = Quaternion.LookRotation(transform.position - target.position);
	}
}
