using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFromTree : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > 0.6f) {
			// transform.Translate (0, -2.5f * Time.deltaTime, 0, Space.World);
			transform.position = new Vector3 (0, 5 + Mathf.Sin (Time.time * 5.0f), 0);
		}
	}
}
