using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour {

	// Manages voice, and checks if Sun is being yelled at.

	// all set publically

	[Space(10)]
	public Tiger tiger;

	[Space(10)]
	public GameObject sun;
	public AudioSource sunScream;
	public Material sunNormalMaterial;
	public Material sunScreamingMaterial;

	[Space(10)]
	public AchievementsUnlock achievementMenuUnlock;
	public GameObject achievementOverlay;

	[Space(10)]
	public string[] keyword;
	public KeywordRecognizer recognizer;

	[Space(5)]
	public bool sunHit = false;		// changed via RaycastfromEye.cs

	private bool achievementAlreadyTriggered = false;



	void Start() {
		
		keyword = new string[2];
		keyword[0] = "ahhh";
		keyword[1] = "leave";

		recognizer = new KeywordRecognizer(keyword);
		recognizer.OnPhraseRecognized += OnPhraseRecognized;
		recognizer.Start();

	}



	// try public if it doesn't work?
	private void OnPhraseRecognized(PhraseRecognizedEventArgs args) {


		if (args.text == keyword[0]) {
			if (sunHit) {				
				SunScream();
			}
		}
			
		// "Leave"
		if (args.text == keyword[1]) {
			if (tiger.mountable) {
				tiger.Invoke("MountTiger", 2f);
			}
		}

		Debug.Log("phrase recognized");
	}


	// If Sun is being yelled at, while being casted at, change mat, and scream
	public void SunScream() {
		sun.GetComponent<Renderer>().material = sunScreamingMaterial;
		sunScream.Play();
		InvokeRepeating("ClipReset", 0f, 0.98f);	// see ClipReset

		Invoke("StopSunScreaming", 3.05f);

		Debug.Log("phrase said and sun hit");


		if (!achievementAlreadyTriggered) {
			UnlockVoice();
		}			
	}


	public void ClipReset() {
		// Clip now plays from 0.98s to 1.98s, on repeat, for 3 instances (until StopScreamingSun triggers)
		sunScream.time = 1f;	
	}

	public void StopStunScreaming() {
		sun.GetComponent<Renderer>().material = sunNormalMaterial;
		CancelInvoke("ClipReset");
	}



	public void UnlockVoice() {
		achievementAlreadyTriggered = true;
		achievementMenuUnlock.SunAchievementUnlocked();

		achievementOverlay.SetActive(true);
		Invoke("HideOverlay", 3f);
	}

	public void HideOverlay() {
		achievementOverlay.SetActive(false);
	}





}
