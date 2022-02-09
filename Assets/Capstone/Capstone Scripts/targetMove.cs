using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMove : MonoBehaviour {


	// Changes the locations of the targets that each kitten moves to.

	void Start () {				
		InvokeRepeating("ChangePosition", 2f, 18f);	
	}

	void ChangePosition() {
		transform.position = new Vector3 ( Random.Range(-18f, 20f) , 8.1f , Random.Range(-9f, 25f) );
	}
}
