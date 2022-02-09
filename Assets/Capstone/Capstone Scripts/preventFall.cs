using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preventFall : MonoBehaviour {

	private float yPos;

	void Start() {
		yPos = gameObject.transform.position.y;
	}

	void Update () {
		if (yPos < 0.3f) {
			yPos = 0.2f;
		}

		if (yPos < 0.2f) {
			yPos = 0.15f;
		}
	}
}
