using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFromEye : MonoBehaviour {


	int layer_mask = 1 << 13;		// 13 = sun

	public VoiceRecognition checkSunHit;

	void LateUpdate () {
		RaycastHit hit;

		// Tell VoiceRecognition if Camera is being casted at Sun.
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layer_mask)) {
			//Debug.Log("CASTING");
			checkSunHit.sunHit = true;
			Debug.Log("hitting sun");
		}


		// If not casting at Sun, return Sun to normal
		else {
			if (checkSunHit.sunHit) {
				Debug.Log("not hitting sun");
				checkSunHit.sun.GetComponent<Renderer>().material = checkSunHit.sunNormalMaterial;
				CancelInvoke("checkSunHit.ClipReset");
				checkSunHit.sunHit = false;
			}
		}
	}
}
