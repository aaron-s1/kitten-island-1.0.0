using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

	public Animator anim;	// set publically

	public void Fading() {
		anim.enabled = true;
		System.GC.Collect();
		anim.SetTrigger("fadeIn");
	}

	public void EndGame() {
		anim.enabled = true;
		anim.SetTrigger("fadeTiger");
		Invoke("FullStop", 3f);
	}

	void FullStop() {
		anim.enabled = false;
		Time.timeScale = 0.0f;
	}
}
